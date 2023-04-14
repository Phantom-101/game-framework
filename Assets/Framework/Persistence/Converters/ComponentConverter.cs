#nullable enable
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Framework.Persistence.Converters {
    public class ComponentConverter : JsonConverter<Component> {
        private readonly GameObjectContext _context;

        public ComponentConverter(GameObjectContext context) {
            _context = context;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, Component? value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override Component? ReadJson(JsonReader reader, Type objectType, Component? existingValue, bool hasExistingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }
            
            var jObject = JObject.Load(reader);
            var type = Type.GetType(jObject.Value<string>("$type")!)!;
            var obj = _context.GetComponent(type);
            serializer.Populate(jObject.CreateReader(), obj);
            return obj;
        }
    }
}