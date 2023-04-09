using UnityEngine;

namespace Framework.Variables {
    [CreateAssetMenu(menuName = "Variable/Camera", fileName = "NewCamera")]
    public class CameraVariable : ScriptableVariable<Camera> {
        [field: SerializeField] public override Camera Value { get; set; }
    }
}