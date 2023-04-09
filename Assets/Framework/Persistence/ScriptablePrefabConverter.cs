#nullable enable
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Framework.Persistence {
    public class ScriptablePrefabConverter : IPersistentConverter {
        private readonly List<ScriptablePrefab>? _prefabs;

        public ScriptablePrefabConverter(IEnumerable<ScriptablePrefab>? prefabs = null) {
            if (prefabs != null) {
                _prefabs = new List<ScriptablePrefab>();
                _prefabs.AddRange(prefabs);
            }
        }
        
        public bool CanSave(IPersistent obj) {
            return obj is ScriptablePrefabInstance;
        }

        public bool CanLoad(PersistentData data) {
            return _prefabs != null && data.Has<string>(Strings.PrefabId) && _prefabs.Any(e => e.id == data.Get<string>(Strings.PrefabId));
        }
        
        public PersistentData Save(IPersistent obj, PersistentSerializer serializer) {
            var data = new PersistentData();
            data.Add(Strings.PrefabId, ((ScriptablePrefabInstance)obj).prefab.id);
            return obj.WritePersistentData(data, serializer);
        }

        public async UniTask<IPersistent?> Load(PersistentData data, PersistentSerializer serializer) {
            var prefab = _prefabs!.First(e => e.id == data.Get<string>(Strings.PrefabId));
            var obj = PersistentUnityObjectFactory.InstantiateFromScriptablePrefab(prefab);
            var metadata = obj.GetComponent<ScriptablePrefabInstance>();
            await metadata.ReadPersistentData(data, serializer);
            return metadata;
        }
    }
}