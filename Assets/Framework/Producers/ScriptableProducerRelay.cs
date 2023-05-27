#nullable enable
using System;

namespace Framework.Producers {
    [Serializable]
    public class ScriptableProducerRelay<T> : IProducer {
        public ScriptableProducer<T> producer = null!;

        public object Produce() => producer.Produce()!;
    }
}