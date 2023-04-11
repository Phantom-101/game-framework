#nullable enable
using Newtonsoft.Json;

namespace Framework.Persistence {
    [JsonObject(MemberSerialization.OptIn)]
    public class AddressablePrefabInstance : PersistentGameObject {
        [JsonProperty]
        public string key = string.Empty;
    }
}