#nullable enable
using Framework.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Framework.Persistence {
    public static class PersistentGameObjectFactory {
        public static GameObject NewEmpty() {
            var obj = new GameObject();
            obj.GetOrAddComponent<PersistentGameObject>();
            return obj;
        }
        
        public static GameObject? NewFromAddressableKey(string key) {
            var prefab = Addressables.LoadAssetAsync<GameObject>(key).WaitForCompletion();
            if (prefab != null) {
                var obj = Object.Instantiate(prefab);
                var metadata = obj.GetOrAddComponent<AddressablePrefabInstance>();
                metadata.prefabKey = key;
                return obj;
            }

            return null;
        }
        
        public static GameObject? NewFromResourcePath(string path) {
            var prefab = Resources.Load<GameObject>(path);
            if (prefab != null) {
                var obj = Object.Instantiate(prefab);
                var metadata = obj.GetOrAddComponent<ResourcePrefabInstance>();
                metadata.prefabPath = path;
                return obj;
            }

            return null;
        }

        public static GameObject NewFromScriptablePrefab(ScriptablePrefab prefab) {
            var obj = Object.Instantiate(prefab.obj);
            var metadata = obj.GetOrAddComponent<ScriptablePrefabInstance>();
            metadata.prefabId = prefab.id;
            return obj;
        }
    }
}