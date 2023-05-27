using UnityEngine;

namespace Framework.Producers {
    public abstract class ScriptableProducer<T> : ScriptableObject, IProducer<T> {
        object IProducer.Produce() => Produce();

        public abstract T Produce();
    }
}