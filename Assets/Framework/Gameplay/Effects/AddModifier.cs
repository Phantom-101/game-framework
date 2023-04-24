using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Gameplay.Effects {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class AddModifier : GameplayModifier {
        public float Delta {
            get => delta;
            set {
                delta = value;
                NotifyChange();
            }
        }

        [SerializeField]
        [JsonProperty]
        private float delta;

        public AddModifier(float delta, int priority = 0) : base(priority) {
            this.delta = delta;
        }
        
        public override float Evaluate(float value) {
            return value + delta;
        }
        
        public override float Evaluate(VersionedValue value) {
            return Evaluate(value.GetLatestValue());
        }
    }
}