#nullable enable
using System;

namespace Framework.Gameplay.Effects {
    [Serializable]
    public abstract class ActiveGameplayEffect {
        public virtual void OnActivate(GameplayObject obj) { }

        public virtual void OnTick(GameplayObject obj, float deltaSeconds) { }

        public virtual void OnDeactivate(GameplayObject obj) { }
    }
}