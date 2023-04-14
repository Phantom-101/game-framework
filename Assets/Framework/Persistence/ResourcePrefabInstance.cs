#nullable enable
using Newtonsoft.Json;

namespace Framework.Persistence {
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ResourcePrefabInstance : PersistentGameObject {
        [JsonProperty]
        public string prefabPath = string.Empty;
    }
}