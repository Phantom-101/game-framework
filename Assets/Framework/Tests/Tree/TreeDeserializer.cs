#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework.Persistence;
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
            }
        }
    }
}