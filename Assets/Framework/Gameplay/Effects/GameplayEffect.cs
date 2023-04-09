#nullable enable
using System;
using Cysharp.Threading.Tasks;
using Framework.Persistence;

namespace Framework.Gameplay.Effects {
    [Serializable]
    public abstract class GameplayEffect : IPersistent {
        public virtual bool CanApply(GameplayObject obj) {
            return true;
        }

        public virtual void OnActivate(GameplayObject obj) { }

        public virtual void OnTick(GameplayObject obj, float deltaSeconds) { }

        public virtual void OnDeactivate(GameplayObject obj) { }

        public virtual PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            return data;
        }

        public virtual async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) { }
    }
}