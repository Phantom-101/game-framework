using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Gameplay.Effects {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class PercentModifier : GameplayModifier {
        public float Percent {
            get => percent;
            set {
                percent = value;
                NotifyChange();
            }
        }

        [SerializeField]
        [JsonProperty]
        private float percent;

        public PercentModifier(float percent, int priority = 0) : base(priority) {
            this.percent = percent;
        }
        
        public override float Evaluate(float value) {
            return value * (1 + percent / 100);
        }

        public override float Evaluate(VersionedValue value) {
            return Evaluate(value.GetValueAtVersion(Priority - 1));
        }
    }
}