using System.Collections.Generic;
using UnityEngine;

namespace Framework.Variables {
    public class ListVariable<T> : ScriptableVariable<List<T>> {
        [field: SerializeField] public override List<T> Value { get; set; } = new();
    }
}