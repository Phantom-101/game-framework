#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Framework.Persistence;
using UnityEngine;

namespace Framework.Gameplay.Effects {
    [Serializable]
    public class AggregateModifier : GameplayModifier, IEnumerable<GameplayModifier> {
        [SerializeReference]
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

        public override PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            base.WritePersistentData(data, serializer);
            data.Add("modifiers", modifiers.ConvertAll(serializer.Save));
            return data;
        }

        public override async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) {
            await base.ReadPersistentData(data, serializer);
            var modifierData = data.Get<List<PersistentData>>("modifiers");
            foreach (var modifier in modifierData) {
                modifiers.Add((GameplayModifier)(await serializer.Load(modifier))!);
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