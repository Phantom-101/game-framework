#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Framework.Persistence;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Tree {
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class TreeNode : MonoBehaviour {
        [JsonProperty]
        public bool isIncreasing;
        
        [JsonProperty]
        public int number;

        [SerializeField]
        [JsonProperty]
        private List<ScriptablePrefabInstance> children = new();

        public ScriptablePrefabInstance? GetParent() {
            var parent = transform.parent;
            return parent == null ? null : parent.GetComponent<ScriptablePrefabInstance>();
        }

        public List<ScriptablePrefabInstance> GetChildren() {
            return (from Transform child in transform select child.GetComponent<ScriptablePrefabInstance>()).ToList();
        }

        private void Update() {
            if (isIncreasing) {
                number++;
            } else {
                number--;
            }
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context) {
            children = GetChildren();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            foreach (var child in children) {
                child.transform.SetParent(transform, false);
            }
        }
    }
}