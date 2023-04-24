#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Collections {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class SerializableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, ISerializationCallbackReceiver {
        [JsonProperty]
        public Dictionary<TKey, TValue> value;

        [SerializeReference]
        private List<TKey> serializedKeys = new();

        [SerializeReference]
        private List<TValue> serializedValues = new();

        public SerializableDictionary() {
            value = new Dictionary<TKey, TValue>();
        }

        public SerializableDictionary(Dictionary<TKey, TValue> value) {
            this.value = value;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
            return value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void OnBeforeSerialize() {
            serializedKeys.Clear();
            serializedKeys.AddRange(value.Keys);
            serializedValues.Clear();
            serializedValues.AddRange(value.Values);
        }

        public void OnAfterDeserialize() {
            value.Clear();
            for (var i = 0; i < Mathf.Min(serializedKeys.Count, serializedValues.Count); i++) {
                value[serializedKeys.ElementAt(i)] = serializedValues.ElementAt(i);
            }
        }
    }
}