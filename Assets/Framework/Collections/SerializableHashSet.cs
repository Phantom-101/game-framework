#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

namespace Framework.Collections {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class SerializableHashSet<T> : IEnumerable<T>, ISerializationCallbackReceiver {
        [JsonProperty]
        public HashSet<T> value;

        [SerializeField]
        private List<T> serializedValue = new();

        public SerializableHashSet() {
            value = new HashSet<T>();
        }

        public SerializableHashSet(HashSet<T> value) {
            this.value = value;
        }

        public IEnumerator<T> GetEnumerator() {
            return value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void OnBeforeSerialize() {
            serializedValue.Clear();
            serializedValue.AddRange(value);
        }

        public void OnAfterDeserialize() {
            value.Clear();
            value.AddRange(serializedValue);
        }
    }
}