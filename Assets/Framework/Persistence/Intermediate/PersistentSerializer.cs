#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework.Persistence.Intermediate.Converters;

namespace Framework.Persistence.Intermediate {
    public class PersistentSerializer {
        private readonly IEnumerable<IPersistentConverter> _converters;

        public PersistentSerializer(IEnumerable<IPersistentConverter> converters) {
            _converters = converters;
        }

        public PersistentData? Save(IPersistent obj) {
            foreach (var saver in _converters) {
                if (saver.CanSave(obj)) {
                    return saver.Save(obj, this);
                }
            }

            return null;
        }

        public async UniTask<IPersistent?> Load(PersistentData data) {
            foreach (var loader in _converters) {
                if (loader.CanLoad(data)) {
                    return await loader.Load(data, this);
                }
            }

            return null;
        }
    }
}