using UnityEngine;

namespace Framework.Producers {
    [CreateAssetMenu(menuName = "Provider/Factory/GameObject", fileName = "NewGameObjectFactory")]
    public class GameObjectScriptableFactory : UnityObjectScriptableFactory<GameObject> { }
}