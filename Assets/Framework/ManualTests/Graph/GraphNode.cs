#nullable enable
using System.Collections.Generic;
using Framework.Persistence;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Graph {
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class GraphNode : MonoBehaviour {
        public GraphNodeInstanceSet nodeSet = null!;
        
        [JsonProperty]
        public List<string> references = new();

        private void OnEnable() {
            nodeSet.Value.Add(GetComponent<ScriptablePrefabInstance>());
        }

        private void OnDisable() {
            nodeSet.Value.Remove(GetComponent<ScriptablePrefabInstance>());
        }
    }
}