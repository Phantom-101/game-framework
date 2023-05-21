using UnityEditor;
using UnityEngine.UIElements;

namespace Framework.Inspector.Editor {
    [CustomEditor(typeof(SerializableMonoBehaviour), true)]
    public class SerializableMonoBehaviourEditor : UnityEditor.Editor {
        private SerializableMonoBehaviour _target;

        private void OnEnable() {
            _target = (SerializableMonoBehaviour)target;
        }

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();
            var fields = _target.GetSerializableFields(SerializationMode.Editor);
            foreach (var field in fields) {
                root.Add(new Label($"{field.Name}: {field.GetValue(_target)}"));
            }
            return root;
        }
    }
}