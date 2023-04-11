#nullable enable
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Framework.Persistence.Intermediate.Converters {
    public class PersistentGameObjectConverter : IPersistentConverter {
        private readonly List<ScriptablePrefab> _prefabs = new();
        
        public PersistentGameObjectConverter() { }

        public PersistentGameObjectConverter(IEnumerable<ScriptablePrefab> prefabs) {
            _prefabs.AddRange(prefabs);
        }

        public bool CanSave(IPersistent obj) {
            return obj is PersistentGameObject;
        }

        public bool CanLoad(PersistentData data) {
            return true;
        }

        public PersistentData Save(IPersistent obj, PersistentSerializer serializer) {
            var data = new PersistentData();
            if (obj is AddressablePrefabInstance addressable) {
                data.Add(Keys.AddressableKey, addressable.key);
            } else if (obj is ResourcePrefabInstance resource) {
                data.Add(Keys.ResourcePath, resource.path);
            } else if (obj is ScriptablePrefabInstance scriptable) {
                data.Add(Keys.PrefabId, scriptable.prefab.id);
            }
            return obj.WritePersistentData(data, serializer);
        }

        public async UniTask<IPersistent?> Load(PersistentData data, PersistentSerializer serializer) {
            GameObject? obj;
            if (data.Has<string>(Keys.AddressableKey)) {
                obj = await PersistentGameObjectFactory.InstantiateFromAddressableKeyAsync(data.Get<string>(Keys.AddressableKey));
            } else if (data.Has<string>(Keys.ResourcePath)) {
                obj = await PersistentGameObjectFactory.InstantiateFromResourcePathAsync(data.Get<string>(Keys.ResourcePath));
            } else if (data.Has<string>(Keys.PrefabId)) {
                obj = PersistentGameObjectFactory.InstantiateFromScriptablePrefab(_prefabs.First(e => e.id == data.Get<string>(Keys.PrefabId)));
            } else {
                obj = PersistentGameObjectFactory.NewEmpty();
            }
            
            if (obj != null) {
                var metadata = obj.GetComponent<ScriptablePrefabInstance>();
                await metadata.ReadPersistentData(data, serializer);
                return metadata;
            }

            return null;
        }
    }
}