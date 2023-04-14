#nullable enable
using System;

namespace Framework.Gameplay.Abilities {
    [Serializable]
    public abstract class GameplayAbility {
        public virtual bool CanActivate(GameplayObject obj) {
            return true;
        }

        public virtual void OnActivate(GameplayObject obj) { }
    }
}