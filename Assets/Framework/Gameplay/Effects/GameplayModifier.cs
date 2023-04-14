#nullable enable
using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Gameplay.Effects {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class GameplayModifier {
        [field: SerializeField]
        [JsonProperty]
        public int Priority { get; private set; }
        
        public event EventHandler? OnChanged;

        protected GameplayModifier(int priority = 0) {
            Priority = priority;
        }

        public virtual float Evaluate(float value) {
            return value;
        }
        
        protected void NotifyChange(object sender, EventArgs args) {
            OnChanged?.Invoke(sender, args);
        }
    }
}