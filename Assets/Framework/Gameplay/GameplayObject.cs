#nullable enable
using Framework.Collections;
using Framework.Gameplay.Effects;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Gameplay {
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class GameplayObject : MonoBehaviour {
        [SerializeField]
        [JsonProperty]
        private SerializableHashSet<GameplayEffect> effects = new();

        protected virtual void Update() {
            foreach (var effect in effects) {
                effect.OnTick(this, Time.deltaTime);
            }
        }

        public void ApplyEffect(GameplayEffect effect) {
            if (!effects.value.Contains(effect)) {
                effect.OnApply(this);
                effects.value.Add(effect);
            }
        }

        public void RemoveEffect(GameplayEffect effect) {
            if (effects.value.Contains(effect)) {
                effects.value.Remove(effect);
                effect.OnRemove(this);
            }
        }
    }
}