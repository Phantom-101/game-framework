#nullable enable
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Framework.Persistence {
    public class PersistentDataConverter : JsonConverter<PersistentData> {
        public override PersistentData? ReadJson(JsonReader reader, Type objectType, PersistentData? existingValue,
            bool hasExistingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }

            var jObject = JObject.Load(reader);
            var ret = new PersistentData();
            foreach (var property in jObject.Properties()) {
                ret.data[property.Name] = property.Value;
            }

            return ret;
        }

        public override void WriteJson(JsonWriter writer, PersistentData? value, JsonSerializer serializer) {
            if (value == null) {
                writer.WriteNull();
                return;
            }

            var jObject = new JObject();
            foreach (var key in value.data.Keys) {
                var keyValue = value.data[key];
                jObject.Add(key, keyValue == null ? JValue.CreateNull() : JToken.FromObject(keyValue));
            }

            jObject.WriteTo(writer);
        }
    }
}