#nullable enable
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Serialization {
    [JsonObject(MemberSerialization.OptIn)]
    public class A {
        [JsonProperty]
        public string name = string.Empty;

        [JsonProperty]
        public A? child;

        protected A() { }

        public A(string name) {
            this.name = name;
        }

        public A(string name, A child) {
            this.name = name;
            this.child = child;
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context) {
            Debug.Log($"A {name} OnSerializing");
        }

        [OnSerialized]
        private void OnSerialized(StreamingContext context) {
            Debug.Log($"A {name} OnSerialized");
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context) {
            Debug.Log($"A {name} OnDeserializing");
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            Debug.Log($"A {name} OnDeserialized");
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class B : A {
        protected B() { }

        public B(string name) : base(name) { }

        public B(string name, A child) : base(name, child) { }

        [OnSerializing]
        private void OnSerializing(StreamingContext context) {
            Debug.Log($"B {name} OnSerializing");
        }

        [OnSerialized]
        private void OnSerialized(StreamingContext context) {
            Debug.Log($"B {name} OnSerialized");
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context) {
            Debug.Log($"B {name} OnDeserializing");
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            Debug.Log($"B {name} OnDeserialized");
        }
    }
}