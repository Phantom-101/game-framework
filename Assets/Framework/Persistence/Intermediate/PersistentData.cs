#nullable enable
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Framework.Persistence.Intermediate {
    [JsonConverter(typeof(PersistentDataConverter))]
    public class PersistentData {
        public readonly Dictionary<string, object> data = new();

        public bool Contains(string key) {
            return data.ContainsKey(key);
        }

        public object Get(string key) {
            return data[key];
        }

        public bool Is<T>(string key) {
            return ((JToken)data[key]).ToObject<T>() != null;
        }

        public bool Has<T>(string key) {
            return Contains(key) && Is<T>(key);
        }
        
        public T Get<T>(string key) {
            return ((JToken)data[key]).ToObject<T>()!;
        }

        public void Add(string key, object value) {
            data.Add(key, value);
        }

        public void Set(string key, object value) {
            data[key] = value;
        }

        public void Remove(string key) {
            data.Remove(key);
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}