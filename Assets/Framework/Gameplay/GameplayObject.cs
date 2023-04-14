#nullable enable
using System.Collections.Generic;
using Framework.Gameplay.Effects;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Gameplay {
    public class GameplayObject : MonoBehaviour {
        [SerializeReference]
        [JsonProperty]
        private List<GameplayEffect> effects = new();

        private void Update() {
            foreach (var effect in effects) {
                effect.OnTick(this, Time.deltaTime);
            }
        }
        
        public void AddEffect(GameplayEffect effect) {
            if (effect.CanApply(this)) {
                effects.Add(effect);
                effect.OnActivate(this);
            }
        }

        public void RemoveEffect(GameplayEffect effect) {
            if (effects.Remove(effect)) {
                effect.OnDeactivate(this);
            }
        }
    }
}