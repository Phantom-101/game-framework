#nullable enable
using Cysharp.Threading.Tasks;

namespace Framework.Persistence {
    public interface IPersistentConverter {
        public bool CanSave(IPersistent obj);
        
        public bool CanLoad(PersistentData data);
        
        public PersistentData Save(IPersistent obj, PersistentSerializer serializer);

        public UniTask<IPersistent?> Load(PersistentData data, PersistentSerializer serializer);
    }
}