using Framework.Gameplay.Effects;

namespace Framework.Tests.Gameplay {
    public class TestModifier : GameplayModifier {
        public int strength = 1;
        
        public override float Evaluate(float value) {
            return value / strength;
        }
    }
}