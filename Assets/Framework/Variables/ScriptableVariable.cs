using Framework.Producers;
using UnityEngine;

namespace Framework.Variables {
    public abstract class ScriptableVariable<T> : ScriptableObject, IProducer<T> {
        public abstract T Value { get; set; }
        
        object IProducer.Produce() => Produce();
        
        public T Produce() => Value;
    }
}