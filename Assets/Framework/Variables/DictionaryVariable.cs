using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Variables {
    public class DictionaryVariable<TKey, TValue> : ScriptableVariable<Dictionary<TKey, TValue>>, ISerializationCallbackReceiver {
        public override Dictionary<TKey, TValue> Value { get; set; } = new();
        
        [SerializeField] private List<TKey> keys = new();
        [SerializeField] private List<TValue> values = new();

        public void OnBeforeSerialize() {
            keys.Clear();
            keys.AddRange(Value.Keys);
            values.Clear();
            values.AddRange(Value.Values);
        }

        public void OnAfterDeserialize() {
            Value.Clear();
            for (var i = 0; i < Math.Min(keys.Count, values.Count); i++) {
                Value.Add(keys[i], values[i]);
            }
        }
    }
}