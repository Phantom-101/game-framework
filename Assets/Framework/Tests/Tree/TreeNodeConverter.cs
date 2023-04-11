#nullable enable
using System.Collections.Generic;
using Framework.Persistence;
using Framework.Persistence.Direct.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Framework.Tests.Tree {
    public class TreeNodeConverter : ComponentConverter<TreeNode> {
        protected override JObject WriteJson(TreeNode value, JsonSerializer serializer) {
            var jObject = JObject.FromObject(value);
            
            var children = new List<JObject>();
            foreach (var child in value.GetChildren()) {
                children.Add(JObject.FromObject(child, serializer));
            }
            jObject.Add("children", new JArray(children));

            return jObject;
        }

        protected override TreeNode ReadJson(TreeNode value, JObject jObject, JsonSerializer serializer) {
            serializer.Populate(jObject.CreateReader(), value);

            var children = jObject.Value<JArray>("children")!;
            foreach (var child in children) {
                var obj = ((JObject)child).ToObject<PersistentGameObject>(serializer);
                if (obj != null) {
                    obj.transform.SetParent(value.transform, false);
                }
            }

            return value;
        }
    }
}