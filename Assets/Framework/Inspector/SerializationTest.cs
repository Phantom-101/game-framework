namespace Framework.Inspector {
    public class SerializationTest : SerializableMonoBehaviour {
        [EditorSerialize] public float editorOnly;
        [RuntimeSerialize] public float runtimeOnly;
        [AlwaysSerialize] public float both;
        public float neither;
    }
}