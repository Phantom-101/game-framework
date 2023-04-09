#nullable enable
using System;
using Cysharp.Threading.Tasks;
using Framework.Persistence;

namespace Framework.Gameplay.Abilities {
    [Serializable]
    public abstract class GameplayAbility : IPersistent {
        public virtual bool CanActivate(GameplayObject obj) {
            return true;
        }

        public virtual void OnActivate(GameplayObject obj) { }

        public virtual PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            return data;
        }

        public virtual async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) { }
    }
}