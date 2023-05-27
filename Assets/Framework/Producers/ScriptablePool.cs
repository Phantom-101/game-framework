#nullable enable
using System;
using Framework.Variables;
using UnityEngine;
using UnityEngine.Pool;

namespace Framework.Producers {
    [CreateAssetMenu(menuName = "Provider/Pool", fileName = "NewPool")]
    public class ScriptablePool<T> : ScriptableProducer<T> where T : class, IPoolable {
        [SerializeReference]
        public ValueReference<IProducer> creationProducer = new();
        public int defaultCapacity = 10;
        public int maxSize = 50;

        private ObjectPool<T>? _pool;
        
        public override T Produce() {
            _pool ??= new ObjectPool<T>(() => (T)creationProducer.Value.Produce(), OnGet, OnRelease, DestroyInstance, true, defaultCapacity, maxSize);
            return _pool.Get();
        }

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
            _pool ??= new ObjectPool<T>(() => (T)creationProducer.Value.Produce(), OnGet, OnRelease, DestroyInstance, true, defaultCapacity, maxSize);
            _pool.Release((T)sender);
        }
    }
}