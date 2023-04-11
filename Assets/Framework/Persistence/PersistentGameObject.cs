#nullable enable
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework.Persistence.Intermediate;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Persistence {
    [JsonObject(MemberSerialization.OptIn)]
    public class PersistentGameObject : MonoBehaviour, IPersistent {
        public virtual PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            var components = new List<PersistentData>();
            data.Add("components", components);
            foreach (var component in GetComponents<IPersistent>()) {
                // Do not save this component to prevent infinite loop
                if (!ReferenceEquals(component, this)) {
                    var componentData = new PersistentData();
                    componentData.Add(Keys.ComponentType, component.GetType().FullName);
                    component.WritePersistentData(componentData, serializer);
                    components.Add(componentData);
                }
            }

            return data;
        }

        public virtual async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) {
            var components = data.Get<List<PersistentData>>("components");
            var loaded = new List<IPersistent>();
            foreach (var componentData in components) {
                var type = Type.GetType(componentData.Get<string>(Keys.ComponentType));
                // Verify component type inherits both Component and IPersistent
                if (typeof(IPersistent).IsAssignableFrom(type) && typeof(Component).IsAssignableFrom(type)) {
                    var isDataLoaded = false;
                    // Prevent loading to the same component twice if multiple of the same component type needs to be loaded
                    foreach (var target in GetComponents(type)) {
                        var persistentTarget = (IPersistent)target;
                        if (!loaded.Contains(persistentTarget)) {
                            await persistentTarget.ReadPersistentData(componentData, serializer);
                            loaded.Add(persistentTarget);
                            isDataLoaded = true;
                            break;
                        }
                    }
                    // If isDataLoaded is still false by this point, no suitable load targets have been found and one needs to be created
                    if (!isDataLoaded) {
                        var target = (IPersistent)gameObject.AddComponent(type);
                        await target.ReadPersistentData(componentData, serializer);
                        loaded.Add(target);
                    }
                }
            }
        }
    }
}