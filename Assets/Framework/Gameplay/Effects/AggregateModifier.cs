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
        // Not a serializable hash set because this needs to be sorted
        [SerializeReference]
        [JsonProperty]
        private List<GameplayModifier> modifiers = new();

        public AggregateModifier(int priority = 0) : base(priority) { }
        
        /// <summary>
        /// Adds a gameplay modifier to the aggregator. Modifiers are sorted firstly by increasing priority, then by earliest to latest insertion.
        /// </summary>
        /// <param name="modifier">Modifier to add</param>
        public void Add(GameplayModifier modifier) {
            if (modifiers.Contains(modifier)) {
                return;
            }
            var index = 0;
            while (index < modifiers.Count && modifiers[index].Priority <= modifier.Priority) {
                index++;
            }
            modifiers.Insert(index, modifier);
            modifier.OnChanged += OnDependencyChange;
            NotifyChange();
        }

        public void Remove(GameplayModifier modifier) {
            if (modifiers.Remove(modifier)) {
                modifier.OnChanged -= OnDependencyChange;
                NotifyChange();
            }
        }

        public override float Evaluate(float value) {
            return modifiers.Aggregate(value, (current, modifier) => modifier.Evaluate(current));
        }

        public override float Evaluate(VersionedValue value) {
            return Evaluate(value.GetLatestValue());
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            foreach (var modifier in modifiers) {
                modifier.OnChanged += OnDependencyChange;
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