using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Framework.Variables {
    public class SetVariable<T> : ScriptableVariable<HashSet<T>>, ISerializationCallbackReceiver {
        public override HashSet<T> Value { get; set; } = new();

        [SerializeField]
        private List<T> items = new();

        public void OnBeforeSerialize() {
            items.Clear();
            items.AddRange(Value);
        }

        public void OnAfterDeserialize() {
            Value.Clear();
            Value.AddRange(items);
        }
    }
}