using UnityEngine;

namespace Framework.Gameplay.Effects {
    public abstract class GameplayEffectDefinition : ScriptableObject {
        public virtual bool CanApply(GameplayObject target) {
            return true;
        }

        public virtual void Apply(GameplayObject target) { }
    }
}