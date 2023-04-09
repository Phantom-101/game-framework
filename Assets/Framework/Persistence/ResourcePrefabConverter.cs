#nullable enable
using Cysharp.Threading.Tasks;

namespace Framework.Persistence {
    public class ResourcePrefabConverter : IPersistentConverter {
        public bool CanSave(IPersistent obj) {
            return obj is ResourcePrefabInstance;
        }

        public bool CanLoad(PersistentData data) {
            return data.Has<string>(Strings.ResourcePath);
        }

        public PersistentData Save(IPersistent obj, PersistentSerializer serializer) {
            var data = new PersistentData();
            data.Add(Strings.ResourcePath, ((ResourcePrefabInstance)obj).path);
            return obj.WritePersistentData(data, serializer);
        }

        public async UniTask<IPersistent?> Load(PersistentData data, PersistentSerializer serializer) {
            var path = data.Get<string>(Strings.ResourcePath);
            var obj = await PersistentUnityObjectFactory.InstantiateFromResourcePath(path);
            if (obj != null) {
                var metadata = obj.GetComponent<ResourcePrefabInstance>();
                await metadata.ReadPersistentData(data, serializer);
                return metadata;
            }

            return null;
        }
    }
}