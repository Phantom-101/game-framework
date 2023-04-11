#nullable enable
using System;
using Cysharp.Threading.Tasks;
using Framework.Gameplay.Effects;
using Framework.Persistence;
using Framework.Persistence.Intermediate;
using Framework.Variables;
using UnityEngine;

namespace Framework.Gameplay.Attributes {
    [Serializable]
    public class GameplayAttribute : IPersistent {
        [field: SerializeField]
        public ValueReference<float> BaseValue { get; private set; } = new();

        [field: SerializeField]
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
        
        public PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            data.Add("baseValue", BaseValue.Value);
            data.Add("aggregator", serializer.Save(Aggregator)!);
            return data;
        }

        public async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) {
            BaseValue.SetValue(data.Get<float>("baseValue"));
            Aggregator = (AggregateModifier)(await serializer.Load(data.Get<PersistentData>("aggregator")))!;
        }

        private void NotifyChange(object sender, EventArgs e) {
            dirty = true;
            OnChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}