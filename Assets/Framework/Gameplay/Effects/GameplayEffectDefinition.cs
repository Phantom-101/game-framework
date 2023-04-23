using UnityEngine;

namespace Framework.Gameplay.Effects {
    public abstract class GameplayEffectDefinition : ScriptableObject {
        public abstract bool CanApply(GameplayObject target);

        public abstract void OnApply(GameplayObject target);
    }
}