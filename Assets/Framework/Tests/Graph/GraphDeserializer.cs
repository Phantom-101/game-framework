#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework.Persistence;
using Framework.Persistence.Intermediate;
using Framework.Persistence.Intermediate.Converters;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Graph {
    public class GraphDeserializer : MonoBehaviour {
        public List<ScriptablePrefab> prefabs = new();
        public GraphNodeInstanceSet nodeSet = null!;
        public bool deserialize;

        private void Update() {
            if (deserialize) {
                deserialize = false;
                StartCoroutine(UniTask.ToCoroutine(async () => {
                    foreach (Transform child in transform) {
                        Destroy(child.gameObject);
                    }

                    var serializer = new PersistentSerializer(new IPersistentConverter[] { new PersistentGameObjectConverter(prefabs), new DefaultConverter() });

                    var data = new List<PersistentData>();
                    foreach (var node in nodeSet.Value) {
                        var saved = serializer.Save(node);
                        if (saved != null) {
                            data.Add(saved);
                        }
                    }

                    var serialized = JsonConvert.SerializeObject(data);
                    Debug.Log(serialized);

                    data = JsonConvert.DeserializeObject<List<PersistentData>>(serialized)!;
                    foreach (var node in data) {
                        var loaded = (ScriptablePrefabInstance?)await serializer.Load(node);
                        if (loaded != null) {
                            loaded.transform.SetParent(transform, false);
                        }
                    }
                }));
            }
        }
    }
}