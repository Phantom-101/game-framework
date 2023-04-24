using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Gameplay.Effects {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class MultiplyModifier : GameplayModifier {
        public float Factor {
            get => factor;
            set {
                factor = value;
                NotifyChange();
            }
        }

        [SerializeField]
        [JsonProperty]
        private float factor;

        public MultiplyModifier(float factor, int priority = 0) : base(priority) {
            this.factor = factor;
        }
        
        public override float Evaluate(float value) {
            return value * factor;
        }

        public override float Evaluate(VersionedValue value) {
            return Evaluate(value.GetLatestValue());
        }
    }
}