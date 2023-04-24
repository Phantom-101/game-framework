#nullable enable
using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Gameplay.Effects {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public abstract class GameplayModifier {
        public int Priority => priority;

        [SerializeField]
        [JsonProperty]
        private int priority;
        
        public event EventHandler? OnChanged;

        protected GameplayModifier(int priority = 0) {
            this.priority = priority;
        }

        public abstract float Evaluate(float value);

        public abstract float Evaluate(VersionedValue value);

        protected void NotifyChange() {
            OnChanged?.Invoke(this, EventArgs.Empty);
        }
        
        protected void OnDependencyChange(object sender, EventArgs args) {
            OnChanged?.Invoke(sender, args);
        }
    }
}