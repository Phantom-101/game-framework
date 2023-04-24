using System;
using Newtonsoft.Json;

namespace Framework.Gameplay.Abilities {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public abstract class GameplayAbility {
        /// <summary>
        /// Determines if the target is valid for this ability to be used on.
        /// </summary>
        /// <param name="source">The gameplay object attempting to use the ability</param>
        /// <param name="target">The gameplay object the ability is attempting to be used on</param>
        /// <returns></returns>
        public abstract bool CanActivate(GameplayObject source, GameplayObject target);
        
        /// <summary>
        /// Activates the ability.
        /// </summary>
        /// <param name="source">The gameplay object using the ability</param>
        /// <param name="target">The gameplay object the ability is being used on</param>
        public abstract void OnActivate(GameplayObject source, GameplayObject target);
    }
}