#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Framework.Persistence.Converters {
    public class GameObjectConverter : JsonConverter<PersistentGameObject> {
        private readonly GameObjectContext _context;
        private readonly List<ScriptablePrefab> _prefabs = new();

        public GameObjectConverter(GameObjectContext context, IEnumerable<ScriptablePrefab> prefabs) {
            _context = context;
            _prefabs.AddRange(prefabs);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, PersistentGameObject? value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override PersistentGameObject? ReadJson(JsonReader reader, Type objectType, PersistentGameObject? existingValue, bool hasExistingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }

            var jObject = JObject.Load(reader);
            GameObject? obj;
            if (jObject.ContainsKey("prefabKey")) {
                obj = PersistentGameObjectFactory.NewFromAddressableKey(jObject.Value<string>("prefabKey")!);
            } else if (jObject.ContainsKey("prefabPath")) {
                obj = PersistentGameObjectFactory.NewFromResourcePath(jObject.Value<string>("prefabPath")!);
            } else if (jObject.ContainsKey("prefabId")) {
                obj = PersistentGameObjectFactory.NewFromScriptablePrefab(_prefabs.First(e => e.id == jObject.Value<string>("prefabId")));
            } else {
                // todo maybe we should throw exception here
                obj = PersistentGameObjectFactory.NewEmpty();
            }

            if (obj == null) {
                // todo maybe we should throw exception here
                return null;
            }

            _context.Push(obj);
            var ret = obj.GetComponent<PersistentGameObject>();
            serializer.Populate(jObject.CreateReader(), ret);
            return ret;
        }
    }
}