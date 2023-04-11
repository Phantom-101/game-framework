#nullable enable
using System;
using Cysharp.Threading.Tasks;
using Framework.Persistence;
using Framework.Persistence.Intermediate;

namespace Framework.Gameplay.Effects {
    [Serializable]
    public abstract class ActiveGameplayEffect : IPersistent {
        public virtual void OnActivate(GameplayObject obj) { }

        public virtual void OnTick(GameplayObject obj, float deltaSeconds) { }

        public virtual void OnDeactivate(GameplayObject obj) { }

        public virtual PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            return data;
        }

        public virtual async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) { }
    }
}