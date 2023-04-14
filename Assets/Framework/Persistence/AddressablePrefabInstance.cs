#nullable enable
using Newtonsoft.Json;

namespace Framework.Persistence {
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class AddressablePrefabInstance : PersistentGameObject {
        [JsonProperty]
        public string prefabKey = string.Empty;
    }
}