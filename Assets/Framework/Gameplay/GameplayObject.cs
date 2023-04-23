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
    }
}