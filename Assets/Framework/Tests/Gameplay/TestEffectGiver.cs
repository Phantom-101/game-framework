#nullable enable
using Framework.Gameplay;
using UnityEngine;

namespace Framework.Tests.Gameplay {
    public class TestEffectGiver : MonoBehaviour {
        public GameplayObject obj = null!;

        private void Start() {
            obj.ApplyEffect(new TestEffect(2));
        }
    }
}