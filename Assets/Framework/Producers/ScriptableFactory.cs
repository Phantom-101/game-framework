#nullable enable
using System;

namespace Framework.Producers {
    public abstract class ScriptableFactory<T> : ScriptableProducer<T> {
        public override T Produce() => Activator.CreateInstance<T>();
    }
}