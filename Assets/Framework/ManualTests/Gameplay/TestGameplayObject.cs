using Framework.Gameplay;
using Framework.Gameplay.Attributes;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Gameplay {
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class TestGameplayObject : GameplayObject {
        [SerializeReference]
        [JsonProperty]
        public GameplayAttribute randomAttribute = new();

        protected override void Update() {
            base.Update();
            Debug.Log(randomAttribute.GetValue());
        }
    }
}