#nullable enable
using Cysharp.Threading.Tasks;

namespace Framework.Persistence.Intermediate {
    public interface IPersistent {
        public PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer);
        
        public UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer);
    }
}