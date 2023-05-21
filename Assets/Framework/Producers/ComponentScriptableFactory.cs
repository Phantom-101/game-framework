using UnityEngine;

namespace Framework.Producers {
    [CreateAssetMenu(menuName = "Provider/Factory/Component", fileName = "NewComponentFactory")]
    public class ComponentScriptableFactory : ComponentScriptableFactory<Component> { }

    public abstract class ComponentScriptableFactory<T> : UnityObjectScriptableFactory<T> where T : Component { }
}