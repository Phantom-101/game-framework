using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Serialization {
    public class CallbacksTest : MonoBehaviour {
        private void Start() {
            var obj = new B("Parent", new B("Child"));

            var settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            
            var serialized = JsonConvert.SerializeObject(obj, settings);

            var deserialized = JsonConvert.DeserializeObject<A>(serialized, settings);
        }
    }
}