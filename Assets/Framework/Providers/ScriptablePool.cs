#nullable enable
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Framework.Providers {
    public abstract class ScriptablePool<T> : ScriptableProvider<T> where T : class, IPoolable {
        [field: SerializeField]
        public int DefaultCapacity { get; private set; } = 10;

        [field: SerializeField]
        public int MaxSize { get; private set; } = 10000;

        [field: SerializeField]
        public ScriptableFactory<T> Factory { get; private set; } = null!;

        [field: SerializeField]
        public bool Initialized { get; private set; }

        private ObjectPool<T>? _pool;

        public sealed override T Provide() {
            TryInitialize();
            return _pool!.Get();
        }

        private void TryInitialize() {
            if (Initialized) return;

            _pool = new ObjectPool<T>(CreateInstance, OnGet, OnRelease, DestroyInstance, true, DefaultCapacity, MaxSize);

            Initialized = true;
        }

        private T CreateInstance() => Factory.Provide();
        
        private void OnGet(T instance) {
            instance.OnRequestRelease += HandleReleaseRequest;
            instance.OnGet();
        }

        private void OnRelease(T instance) {
            instance.OnRequestRelease -= HandleReleaseRequest;
            instance.OnRelease();
        }

        private void DestroyInstance(T instance) {
            instance.Destroy();
        }

        private void HandleReleaseRequest(object sender, EventArgs args) {
            TryInitialize();
            _pool!.Release((T)sender);
        }
    }
}