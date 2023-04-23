#nullable enable
using System;
using System.Runtime.Serialization;
using Framework.Gameplay.Effects;
using Framework.Variables;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Gameplay.Attributes {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class GameplayAttribute {
        [field: SerializeField]
        [JsonProperty]
        public ValueReference<float> BaseValue { get; private set; } = new();

        [field: SerializeField]
        [JsonProperty]
        public AggregateModifier Aggregator { get; private set; } = new();

        [SerializeField]
        private bool dirty = true;
        
        [SerializeField]
        private float value;
        
        public event EventHandler? OnChanged;

        public GameplayAttribute() {
            Aggregator.OnChanged += NotifyChange;
        }

        public float GetValue() {
            if (dirty) {
                value = Aggregator.Evaluate(BaseValue);
                dirty = false;
            }

            return value;
        }
        
        private void NotifyChange(object sender, EventArgs e) {
            dirty = true;
            OnChanged?.Invoke(this, EventArgs.Empty);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            Aggregator.OnChanged += NotifyChange;
        }
    }
}