#nullable enable
using System;

namespace Framework.Gameplay.Effects {
    [Serializable]
    public abstract class GameplayEffect {
        public virtual bool CanApply(GameplayObject obj) {
            return true;
        }

        public virtual void OnActivate(GameplayObject obj) { }

        public virtual void OnTick(GameplayObject obj, float deltaSeconds) { }

        public virtual void OnDeactivate(GameplayObject obj) { }
    }
}