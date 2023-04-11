#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Framework.Persistence.Direct.Converters {
    public class PersistentGameObjectConverter : JsonConverter<PersistentGameObject> {
        private readonly List<ScriptablePrefab> _prefabs = new();

        public PersistentGameObjectConverter() { }
        
        public PersistentGameObjectConverter(IEnumerable<ScriptablePrefab> prefabs) {
            _prefabs.AddRange(prefabs);
        }

        public override void WriteJson(JsonWriter writer, PersistentGameObject? value, JsonSerializer serializer) {
            if (value == null) {
                writer.WriteNull();
            } else {
                var jObject = new JObject {
                    new JProperty("name", value.gameObject.name),
                    new JProperty("position", new JArray(Vector3ToFloatArray(value.transform.localPosition))),
                    new JProperty("rotation", new JArray(Vector3ToFloatArray(value.transform.localEulerAngles)))
                };
                switch (value) {
                    case AddressablePrefabInstance addressable:
                        jObject.Add("$prefabKey", addressable.key);
                        break;
                    case ResourcePrefabInstance resource:
                        jObject.Add("$prefabPath", resource.path);
                        break;
                    case ScriptablePrefabInstance scriptable:
                        jObject.Add("$prefabId", scriptable.prefab.id);
                        break;
                }

                var components = new List<JObject>();
                foreach (var component in value.GetComponents<Component>()) {
                    if (value != component && component.GetType().GetCustomAttributes(true).Any(e => e is JsonObjectAttribute)) {
                        var componentData = JObject.FromObject(component, serializer);
                        componentData.Add("$componentType", component.GetType().FullName);
                        components.Add(componentData);
                    }
                }
                jObject.Add("$components", new JArray(components));
                
                jObject.WriteTo(writer);
            }
        }

        public override PersistentGameObject? ReadJson(JsonReader reader, Type objectType, PersistentGameObject? existingValue, bool hasExistingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }
            var jObject = JObject.Load(reader);
            GameObject? obj;
            if (jObject.ContainsKey("$prefabKey")) {
                obj = PersistentGameObjectFactory.InstantiateFromAddressableKey(jObject.Value<string>("$prefabKey")!);
            } else if (jObject.ContainsKey("$prefabPath")) {
                obj = PersistentGameObjectFactory.InstantiateFromResourcePath(jObject.Value<string>("$prefabPath")!);
            } else if (jObject.ContainsKey("$prefabId")) {
                obj = PersistentGameObjectFactory.InstantiateFromScriptablePrefab(_prefabs.First(e => e.id == jObject.Value<string>("$prefabId")));
            } else {
                obj = PersistentGameObjectFactory.NewEmpty();
            }

            if (obj == null) {
                return null;
            }

            obj.gameObject.name = jObject.Value<string>("name");
            obj.transform.localPosition = FloatArrayToVector3(jObject.Value<JArray>("position")!.ToObject<float[]>()!);
            obj.transform.localEulerAngles = FloatArrayToVector3(jObject.Value<JArray>("rotation")!.ToObject<float[]>()!);

            var components = jObject.Value<JArray>("$components")!;
            var loaded = new List<Component>();
            foreach (var componentData in components) {
                var type = Type.GetType(componentData.Value<string>("$componentType")!)!;
                // Verify component type inherits Component and is marked with a JsonObject attribute
                if (typeof(Component).IsAssignableFrom(type) && type.GetCustomAttributes(true).Any(e => e is JsonObjectAttribute)) {
                    var isDataLoaded = false;
                    // Prevent loading to the same component twice if multiple of the same component type needs to be loaded
                    foreach (var target in obj.GetComponents(type)) {
                        if (!loaded.Contains(target)) {
                            // Check if there is an appropriate converter
                            var converter = serializer.Converters.FirstOrDefault(e => e.CanConvert(type) && e.CanRead);
                            if (converter == null) {
                                serializer.Populate(componentData.CreateReader(), target);
                            } else {
                                converter.ReadJson(componentData.CreateReader(), type, target, serializer);
                            }
                            loaded.Add(target);
                            isDataLoaded = true;
                            break;
                        }
                    }
                    // If isDataLoaded is still false by this point, no suitable load targets have been found and one needs to be created
                    if (!isDataLoaded) {
                        var target = obj.AddComponent(type);
                        // Check if there is an appropriate converter
                        var converter = serializer.Converters.FirstOrDefault(e => e.CanConvert(type) && e.CanRead);
                        if (converter == null) {
                            serializer.Populate(componentData.CreateReader(), target);
                        } else {
                            converter.ReadJson(componentData.CreateReader(), type, target, serializer);
                        }
                        loaded.Add(target);
                    }
                }
            }

            return obj.GetComponent<PersistentGameObject>();
        }
        
        private static float[] Vector3ToFloatArray(Vector3 v) {
            return new[] { v.x, v.y, v.z };
        }

        private static Vector3 FloatArrayToVector3(IReadOnlyList<float> a) {
            return new Vector3(a[0], a[1], a[2]);
        }
    }
}