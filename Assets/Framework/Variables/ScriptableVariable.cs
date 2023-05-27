#nullable enable
using Framework.Producers;

namespace Framework.Variables {
    public abstract class ScriptableVariable<T> : ScriptableProducer<T> {
        public abstract T Value { get; set; }
        
        public override T Produce() => Value;
    }
}