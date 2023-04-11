#nullable enable
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Framework.Persistence {
    public static class PersistentGameObjectFactory {
        public static GameObject NewEmpty() {
            var obj = new GameObject();
            obj.AddComponent<PersistentGameObject>();
            return obj;
        }
        
        public static GameObject? InstantiateFromAddressableKey(string key) {
            var prefab = Addressables.LoadAssetAsync<GameObject>(key).WaitForCompletion();
            if (prefab != null) {
                var obj = Object.Instantiate(prefab);
                var metadata = obj.GetComponent<AddressablePrefabInstance>();
                if (metadata == null) {
                    metadata = obj.AddComponent<AddressablePrefabInstance>();
                }
                metadata.key = key;
                return obj;
            }

            return null;
        }
        
        public static async UniTask<GameObject?> InstantiateFromAddressableKeyAsync(string key) {
            var prefab = await Addressables.LoadAssetAsync<GameObject>(key);
            if (prefab != null) {
                var obj = Object.Instantiate(prefab);
                var metadata = obj.GetComponent<AddressablePrefabInstance>();
                if (metadata == null) {
                    metadata = obj.AddComponent<AddressablePrefabInstance>();
                }
                metadata.key = key;
                return obj;
            }

            return null;
        }

        public static GameObject? InstantiateFromResourcePath(string path) {
            var prefab = Resources.Load<GameObject>(path);
            if (prefab != null) {
                var obj = Object.Instantiate(prefab);
                var metadata = obj.GetComponent<ResourcePrefabInstance>();
                if (metadata == null) {
                    metadata = obj.AddComponent<ResourcePrefabInstance>();
                }
                metadata.path = path;
                return obj;
            }

            return null;
        }

        public static async UniTask<GameObject?> InstantiateFromResourcePathAsync(string path) {
            var resource = await Resources.LoadAsync<GameObject>(path);
            if (resource != null && resource is GameObject prefab) {
                var obj = Object.Instantiate(prefab);
                var metadata = obj.GetComponent<ResourcePrefabInstance>();
                if (metadata == null) {
                    metadata = obj.AddComponent<ResourcePrefabInstance>();
                }
                metadata.path = path;
                return obj;
            }

            return null;
        }

        public static GameObject InstantiateFromScriptablePrefab(ScriptablePrefab prefab) {
            var obj = Object.Instantiate(prefab.obj);
            var metadata = obj.GetComponent<ScriptablePrefabInstance>();
            if (metadata == null) {
                metadata = obj.AddComponent<ScriptablePrefabInstance>();
            }
            metadata.prefab = prefab;
            return obj;
        }
    }
}