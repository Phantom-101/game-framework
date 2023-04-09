#nullable enable
using System;
using UnityEngine;

namespace Framework.Variables {
    [Serializable]
    public class ValueReference<T> {
        [SerializeField] private bool useConstant = true;
        [SerializeField] private T? constant;
        [SerializeField] private ScriptableVariable<T>? variable;

        public event EventHandler<T>? OnValueChanged; 

        public ValueReference() { }

        public ValueReference(T value) {
            constant = value;
        }

        public T Value => useConstant ? constant! : variable!.Value;

        public void SetValue(T value) {
            useConstant = true;
            constant = value;
            OnValueChanged?.Invoke(this, Value);
        }

        public void SetValue(ScriptableVariable<T> value) {
            useConstant = false;
            variable = value;
            OnValueChanged?.Invoke(this, Value);
        }
        
        public void SetValueWithoutNotify(T value) {
            useConstant = true;
            constant = value;
        }

        public void SetValueWithoutNotify(ScriptableVariable<T> value) {
            useConstant = false;
            variable = value;
        }
        
        public static implicit operator T(ValueReference<T> reference) {
            return reference.Value;
        }
    }
}