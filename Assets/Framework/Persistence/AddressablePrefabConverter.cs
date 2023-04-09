#nullable enable
using Cysharp.Threading.Tasks;

namespace Framework.Persistence {
    public class AddressablePrefabConverter : IPersistentConverter {
        public bool CanSave(IPersistent obj) {
            return obj is AddressablePrefabInstance;
        }

        public bool CanLoad(PersistentData data) {
            return data.Has<string>(Strings.AddressableKey);
        }

        public PersistentData Save(IPersistent obj, PersistentSerializer serializer) {
            var data = new PersistentData();
            data.Add(Strings.AddressableKey, ((AddressablePrefabInstance)obj).key);
            return obj.WritePersistentData(data, serializer);
        }

        public async UniTask<IPersistent?> Load(PersistentData data, PersistentSerializer serializer) {
            var key = data.Get<string>(Strings.AddressableKey);
            var obj = await PersistentUnityObjectFactory.InstantiateFromAddressableKey(key);
            if (obj != null) {
                var metadata = obj.GetComponent<ScriptablePrefabInstance>();
                await metadata.ReadPersistentData(data, serializer);
                return metadata;
            }

            return null;
        }
    }
}