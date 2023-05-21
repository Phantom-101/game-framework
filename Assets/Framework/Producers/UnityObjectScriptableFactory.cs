#nullable enable
using UnityEngine;

namespace Framework.Producers {
    [CreateAssetMenu(menuName = "Provider/Factory/UnityObject", fileName = "NewUnityObjectFactory")]
    public class UnityObjectScriptableFactory : UnityObjectScriptableFactory<Object> { }

    public abstract class UnityObjectScriptableFactory<T> : ScriptableFactory<T> where T : Object {
        public T prefab = null!;

        public override T Produce() => Instantiate(prefab);
    }
}