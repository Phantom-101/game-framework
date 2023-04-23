#nullable enable
using Framework.Gameplay;
using Framework.Gameplay.Effects;

namespace Framework.Tests.Gameplay {
    public class TestEffect : GameplayEffect {
        private TestModifier? _modifier;
        
        public void OnActivate(GameplayObject obj) {
            /*
            _modifier = (TestModifier)((TestEffectType)Type).modifier.NewInstance();
            _modifier.strength = ((TestEffectType)Type).strength;
            foreach (var set in obj.attributeSets) {
                if (set is TestAttributeSet casted) {
                    casted.health.Aggregator.Add(_modifier);
                }
            }
            */
        }

        public override void OnDeactivate(GameplayObject obj) {
            /*
            foreach (var set in obj.attributeSets) {
                if (set is TestAttributeSet casted) {
                    casted.health.Aggregator.Remove(_modifier!);
                }
            }
            */
        }
    }
}