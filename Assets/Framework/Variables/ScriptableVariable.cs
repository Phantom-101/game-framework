using Framework.Providers;

namespace Framework.Variables {
    public abstract class ScriptableVariable<T> : ScriptableProvider<T> {
        public abstract T Value { get; set; }

        public override T Provide() => Value;
    }
}