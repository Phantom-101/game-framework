using UnityEngine;

namespace Framework.Variables {
    [CreateAssetMenu(menuName = "Variable/Float", fileName = "NewFloat")]
    public class FloatVariable : ScriptableVariable<float> {
        [field: SerializeField] public override float Value { get; set; }
    }
}