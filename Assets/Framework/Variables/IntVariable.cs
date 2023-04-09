using UnityEngine;

namespace Framework.Variables {
    [CreateAssetMenu(menuName = "Variable/Int", fileName = "NewInt")]
    public class IntVariable : ScriptableVariable<int> {
        [field: SerializeField] public override int Value { get; set; }
    }
}