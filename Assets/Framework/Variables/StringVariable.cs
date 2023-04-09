using UnityEngine;

namespace Framework.Variables {
    [CreateAssetMenu(menuName = "Variable/String", fileName = "NewString")]
    public class StringVariable : ScriptableVariable<string> {
        [field: SerializeField] public override string Value { get; set; } = string.Empty;
    }
}