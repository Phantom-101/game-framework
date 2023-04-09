using System;
using UnityEngine;

namespace Framework.Persistence {
    [CreateAssetMenu(menuName = "ScriptablePrefab", fileName = "NewScriptablePrefab")]
    public class ScriptablePrefab : ScriptableObject {
        public string id = Guid.NewGuid().ToString();
        public GameObject obj;
    }
}