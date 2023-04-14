#nullable enable
using System.Collections.Generic;
using Framework.Persistence;
using Framework.Persistence.Converters;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Graph {
    public class GraphDeserializer : MonoBehaviour {
        public List<ScriptablePrefab> prefabs = new();
        public GraphNodeInstanceSet nodeSet = null!;
        public bool deserialize;

        private void Update() {
            if (deserialize) {
                foreach (Transform child in transform) {
                    Destroy(child.gameObject);
                }

                var context = new GameObjectContext();
                var settings = new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Converters = {
                        new GameObjectConverter(context, prefabs),
                        new ComponentConverter(context)
                    }
                };
                
                var serialized = JsonConvert.SerializeObject(nodeSet.Value, settings);
                Debug.Log(serialized);

                var objs = JsonConvert.DeserializeObject<HashSet<PersistentGameObject>>(serialized, settings)!;
                foreach (var obj in objs) {
                    if (obj != null) {
                        obj.transform.SetParent(transform, false);
                    }
                }
                
                deserialize = false;
            }
        }
    }
}