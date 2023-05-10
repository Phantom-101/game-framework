#nullable enable
using UnityEngine;

namespace Framework.Providers {
    public class ComponentFactory<T> : ScriptableFactory<T> where T : Component {
        [field: SerializeField]
        public T PrefabComponent { get; private set; } = null!;

        public override T Provide() => Instantiate(PrefabComponent);
    }
}