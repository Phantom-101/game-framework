#nullable enable
using System.Collections.Generic;
using Framework.Persistence;
using Framework.Persistence.Converters;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Tree {
    public class TreeDeserializer : MonoBehaviour {
        public List<ScriptablePrefab> prefabs = new();
        public ScriptablePrefabInstance? target;
        
        private void Update() {
            if (target != null) {
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

                var serialized = JsonConvert.SerializeObject(target, settings);
                Debug.Log(serialized);

                var obj = JsonConvert.DeserializeObject<PersistentGameObject>(serialized, settings);
                if (obj != null) {
                    obj.transform.SetParent(transform, false);
                }
                
                target = null;
            }
        }
    }
}