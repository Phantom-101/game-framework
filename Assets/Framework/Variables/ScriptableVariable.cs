using UnityEngine;

namespace Framework.Variables {
    public abstract class ScriptableVariable<T> : ScriptableObject {
        public abstract T Value { get; set; }
    }
}