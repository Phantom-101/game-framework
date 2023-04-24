#nullable enable
using UnityEngine;

namespace Framework.Gameplay.Abilities {
    public abstract class GameplayAbilityDefinition : ScriptableObject {
        /// <summary>
        /// Creates a new gameplay ability with default values.
        /// </summary>
        /// <returns>New instance of the gameplay ability this scriptable object defines</returns>
        public abstract GameplayAbility CreateGameplayAbility();
    }
}