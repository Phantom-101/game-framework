#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework.Persistence;
using Framework.Persistence.Direct.Converters;
using Framework.Persistence.Intermediate;
using Framework.Persistence.Intermediate.Converters;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Tree {
    public class TreeDeserializer : MonoBehaviour {
        public List<ScriptablePrefab> prefabs = new();
        public ScriptablePrefabInstance? target;

        private ScriptablePrefabInstance? _capturedTarget;

        private void Update() {
            if (target != null) {
                _capturedTarget = target;
                target = null;
                /*
                StartCoroutine(UniTask.ToCoroutine(async () => {
                    foreach (Transform child in transform) {
                        Destroy(child.gameObject);
                    }

                    var serializer = new PersistentSerializer(new IPersistentConverter[] { new ScriptablePrefabConverter(prefabs), new DefaultConverter() });

                    var data = serializer.Save(_capturedTarget);

                    var serialized = JsonConvert.SerializeObject(data);

                    data = JsonConvert.DeserializeObject<PersistentData>(serialized)!;
                    var loaded = (ScriptablePrefabInstance?)await serializer.Load(data);
                    if (loaded != null) {
                        loaded.transform.SetParent(transform, false);
                    }
                }));
                */
                foreach (Transform child in transform) {
                    Destroy(child.gameObject);
                }

                var serializer = new PersistentSerializer(new IPersistentConverter[] { new Persistence.Intermediate.Converters.PersistentGameObjectConverter(prefabs), new DefaultConverter() });
                var data = serializer.Save(_capturedTarget);
                var dataSerialized = JsonConvert.SerializeObject(data);
                
                Debug.Log(dataSerialized);
                
                var settings = new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.All,
                    Converters = {
                        new Persistence.Direct.Converters.PersistentGameObjectConverter(prefabs),
                        new TreeNodeConverter()
                    }
                };

                var serialized = JsonConvert.SerializeObject(_capturedTarget, settings);
                
                Debug.Log(serialized);

                var obj = JsonConvert.DeserializeObject<PersistentGameObject>(serialized, settings);
                if (obj != null) {
                    obj.transform.SetParent(transform, false);
                }
            }
        }
    }
}