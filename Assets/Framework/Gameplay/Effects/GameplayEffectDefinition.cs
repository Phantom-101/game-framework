#nullable enable
using UnityEngine;

namespace Framework.Gameplay.Effects {
    public abstract class GameplayEffectDefinition : ScriptableObject {
        /// <summary>
        /// Determines if this effect can be applied to the target.
        /// </summary>
        /// <param name="target">The gameplay object that this effect is being attempted to be applied on</param>
        /// <returns></returns>
        public abstract bool CanApply(GameplayObject target);

        /// <summary>
        /// Applies this effect to the target.
        /// </summary>
        /// <param name="target">The gameplay object that this effect is being applied on</param>
        public abstract void OnApply(GameplayObject target);
    }
}