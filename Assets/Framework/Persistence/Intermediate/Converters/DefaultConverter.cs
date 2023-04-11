#nullable enable
using System;
using Cysharp.Threading.Tasks;

namespace Framework.Persistence.Intermediate.Converters {
    public class DefaultConverter : IPersistentConverter {
        public bool CanSave(IPersistent obj) {
            return true;
        }
        
        public bool CanLoad(PersistentData data) {
            return data.Has<string>(Keys.Type) && Type.GetType(data.Get<string>(Keys.Type)) != null;
        }

        public PersistentData Save(IPersistent obj, PersistentSerializer serializer) {
            var data = new PersistentData();
            data.Add(Keys.Type, obj.GetType().FullName);
            return obj.WritePersistentData(data, serializer);
        }
        
        public async UniTask<IPersistent?> Load(PersistentData data, PersistentSerializer serializer) {
            var ret = (IPersistent)Activator.CreateInstance(Type.GetType(data.Get<string>(Keys.Type))!);
            await ret.ReadPersistentData(data, serializer);
            return ret;
        }
    }
}