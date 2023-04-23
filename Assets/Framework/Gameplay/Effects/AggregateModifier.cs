#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Gameplay.Effects {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class AggregateModifier : GameplayModifier, IEnumerable<GameplayModifier> {
        [SerializeReference]
        [JsonProperty]
        private List<GameplayModifier> modifiers = new();

        public AggregateModifier(int priority = 0) : base(priority) { }
        
        public void Add(GameplayModifier modifier) {
            if (modifiers.Contains(modifier)) {
                return;
            }
            var index = 0;
            while (index < modifiers.Count && modifiers[index].Priority <= modifier.Priority) {
                index++;
            }
            modifiers.Insert(index, modifier);
            modifier.OnChanged += NotifyChange;
            NotifyChange(this, EventArgs.Empty);
        }

        public void Remove(GameplayModifier modifier) {
            if (modifiers.Remove(modifier)) {
                modifier.OnChanged -= NotifyChange;
                NotifyChange(this, EventArgs.Empty);
            }
        }

        public override float Evaluate(float value) {
            return modifiers.Aggregate(value, (current, modifier) => modifier.Evaluate(current));
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            foreach (var modifier in modifiers) {
                modifier.OnChanged += NotifyChange;
            }
        }
        
        public IEnumerator<GameplayModifier> GetEnumerator() {
            return modifiers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}