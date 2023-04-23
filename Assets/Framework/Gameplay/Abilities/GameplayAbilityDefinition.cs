#nullable enable
using UnityEngine;

namespace Framework.Gameplay.Abilities {
    public abstract class GameplayAbilityDefinition : ScriptableObject {
        public abstract bool CanActivate(GameplayObject source, GameplayObject target);
        
        public abstract void OnActivate(GameplayObject source, GameplayObject target);
    }
}