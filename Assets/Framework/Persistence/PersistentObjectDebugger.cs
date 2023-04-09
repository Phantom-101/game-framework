#nullable enable
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Persistence {
    public class PersistentObjectDebugger : MonoBehaviour {
        public PersistentObject target = null!;

        [TextArea(5, 10)]
        public string serialized = string.Empty;

        private void Update() {
            var serializer = new PersistentSerializer(new IPersistentConverter[] { new ScriptablePrefabConverter(), new DefaultConverter() });
            serialized = JsonConvert.SerializeObject(serializer.Save(target), Formatting.Indented);
        }
    }
}