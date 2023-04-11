#nullable enable
using Newtonsoft.Json;

namespace Framework.Persistence {
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourcePrefabInstance : PersistentGameObject {
        [JsonProperty]
        public string path = string.Empty;
    }
}