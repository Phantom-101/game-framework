#nullable enable
using System;
using Cysharp.Threading.Tasks;
using Framework.Persistence;
using Framework.Persistence.Intermediate;
using UnityEngine;

namespace Framework.Gameplay.Effects {
    [Serializable]
    public class GameplayModifier : IPersistent {
        [field: SerializeField]
        public int Priority { get; private set; }
        
        public event EventHandler? OnChanged;

        protected GameplayModifier(int priority = 0) {
            Priority = priority;
        }

        public virtual float Evaluate(float value) {
            return value;
        }
        
        public virtual PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            data.Add("priority", Priority);
            return data;
        }

        public virtual async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) {
            Priority = data.Get<int>("priority");
        }

        protected void NotifyChange(object sender, EventArgs args) {
            OnChanged?.Invoke(sender, args);
        }
    }
}