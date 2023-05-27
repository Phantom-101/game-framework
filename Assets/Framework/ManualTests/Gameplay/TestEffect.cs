#nullable enable
using System;
using Framework.Gameplay;
using Framework.Gameplay.Effects;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Gameplay {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class TestEffect : GameplayEffect {
        [SerializeField]
        [JsonProperty]
        private float strength;
        
        [SerializeReference]
        [JsonProperty]
        private GameplayModifier? modifier;

        public TestEffect(float strength) {
            this.strength = strength;
        }
        
        public override void OnApply(GameplayObject target) {
            modifier = new MultiplyModifier(1 / strength);
            if (target is TestGameplayObject testGameplayObject) {
                testGameplayObject.randomAttribute.Aggregator.Add(modifier);
            }
        }

        public override void OnRemove(GameplayObject target) {
            if (target is TestGameplayObject testGameplayObject) {
                testGameplayObject.randomAttribute.Aggregator.Remove(modifier!);
            }

            modifier = null;
        }
    }
}