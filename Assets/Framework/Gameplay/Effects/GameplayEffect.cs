#nullable enable
using System;

namespace Framework.Gameplay.Effects {
    [Serializable]
    public abstract class GameplayEffect {
        /// <summary>
        /// Called when the effect is first applied.
        /// </summary>
        /// <param name="target">The gameplay object that this effect was applied on</param>
        public virtual void OnApply(GameplayObject target) { }

        /// <summary>
        /// Called every tick this effect is active.
        /// </summary>
        /// <param name="target">The gameplay object that this effect was applied on</param>
        /// <param name="deltaSeconds">The time in seconds since the previous tick</param>
        public virtual void OnTick(GameplayObject target, float deltaSeconds) { }

        /// <summary>
        /// Called when the effect is removed.
        /// </summary>
        /// <param name="target">The gameplay object that this effect was applied on</param>
        public virtual void OnRemove(GameplayObject target) { }
    }
}