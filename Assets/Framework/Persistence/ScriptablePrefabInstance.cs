#nullable enable
using Newtonsoft.Json;

namespace Framework.Persistence {
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ScriptablePrefabInstance : PersistentGameObject {
        [JsonProperty]
        public string prefabId = string.Empty;
    }
}