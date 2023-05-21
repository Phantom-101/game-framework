using UnityEngine;

namespace Framework.Variables {
    public class VariableSetter<T> : MonoBehaviour {
        public ScriptableVariable<T> variable;
        
        [field: SerializeField]
        public virtual T Value { get; set; }

        private void Awake() {
            variable.Value = Value;
        }
    }
}