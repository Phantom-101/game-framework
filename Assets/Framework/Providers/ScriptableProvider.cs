#nullable enable
using UnityEngine;

namespace Framework.Providers {
    public abstract class ScriptableProvider<T> : ScriptableObject, IProvider<T> {
        public abstract T Provide();
    }
}