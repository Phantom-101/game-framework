using UnityEngine;

namespace Framework.Variables {
    [CreateAssetMenu(menuName = "Variable/Bool", fileName = "NewBool")]
    public class BoolVariable : ScriptableVariable<bool> {
        [field: SerializeField] public override bool Value { get; set; }
    }
}