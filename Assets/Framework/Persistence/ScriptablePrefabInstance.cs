#nullable enable
using Newtonsoft.Json;

namespace Framework.Persistence {
    [JsonObject(MemberSerialization.OptIn)]
    public class ScriptablePrefabInstance : PersistentGameObject {
        //[JsonProperty]
        public ScriptablePrefab prefab = null!;
    }
}