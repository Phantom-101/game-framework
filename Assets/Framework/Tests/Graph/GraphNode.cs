#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework.Persistence;
using Framework.Persistence.Intermediate;

namespace Framework.Tests.Graph {
    public class GraphNode : PersistentObject {
        public GraphNodeInstanceSet nodeSet = null!;
        public List<string> references = new();

        private void OnEnable() {
            nodeSet.Value.Add(GetComponent<ScriptablePrefabInstance>());
        }

        private void OnDisable() {
            nodeSet.Value.Remove(GetComponent<ScriptablePrefabInstance>());
        }

        public override PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            base.WritePersistentData(data, serializer);
            data.Add("references", references);
            return data;
        }

        public override async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) {
            await base.ReadPersistentData(data, serializer);
            references.AddRange(data.Get<List<string>>("references"));
        }
    }
}