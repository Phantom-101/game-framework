#nullable enable
using System;

namespace Framework.Gameplay.Effects {
    [Serializable]
    public abstract class GameplayEffect {
        public virtual void OnActivate(GameplayObject source, GameplayObject target) { }

        public virtual void OnTick(GameplayObject target, float deltaSeconds) { }

        public virtual void OnDeactivate(GameplayObject target) { }
    }
}