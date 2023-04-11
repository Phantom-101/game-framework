#nullable enable
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Framework.Persistence;
using Framework.Persistence.Intermediate;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Tests.Tree {
    [JsonObject(MemberSerialization.OptIn)]
    public class TreeNode : PersistentObject {
        [JsonProperty]
        public bool isIncreasing;
        
        [JsonProperty]
        public int number;

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

        public override PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            base.WritePersistentData(data, serializer);
            data.Add("isIncreasing", isIncreasing);
            data.Add("number", number);
            data.Add("children", GetChildren().ConvertAll(serializer.Save).ToList());
            return data;
        }

        public override async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) {
            await base.ReadPersistentData(data, serializer);
            isIncreasing = data.Get<bool>("isIncreasing");
            number = data.Get<int>("number");
            var children = data.Get<List<PersistentData>>("children");
            foreach (var child in children) {
                var obj = (ScriptablePrefabInstance?)await serializer.Load(child);
                if (obj != null) {
                    obj.transform.SetParent(transform, false);
                }
            }
        }
    }
}