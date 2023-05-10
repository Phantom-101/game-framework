using System;

namespace Framework.Providers {
    public abstract class ScriptableFactory<T> : ScriptableProvider<T> {
        public override T Provide() => Activator.CreateInstance<T>();
    }
}