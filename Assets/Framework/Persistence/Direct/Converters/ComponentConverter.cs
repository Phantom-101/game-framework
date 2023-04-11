#nullable enable
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Framework.Persistence.Direct.Converters {
    public abstract class ComponentConverter<T> : JsonConverter<T> where T : Component {
        public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer) {
            if (value == null) {
                writer.WriteNull();
            } else {
                WriteJson(value, serializer).WriteTo(writer);
            }
        }

        protected abstract JObject WriteJson(T value, JsonSerializer serializer);

        public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer) {
            if (!hasExistingValue || existingValue == null) {
                return null;
            }
            
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }
            
            return ReadJson(existingValue, JObject.Load(reader), serializer);
        }

        protected abstract T ReadJson(T value, JObject jObject, JsonSerializer serializer);
    }
}