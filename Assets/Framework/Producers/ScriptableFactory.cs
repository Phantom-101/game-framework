#nullable enable
using System;
using UnityEngine;

namespace Framework.Producers {
    public abstract class ScriptableFactory<T> : ScriptableObject, IProducer<T> {
        object IProducer.Produce() => Produce()!;
        
        public virtual T Produce() => Activator.CreateInstance<T>();
    }
}