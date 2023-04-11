using System;
using UnityEngine;

namespace Framework.Utilities {
    public static class UnityUtilities {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component {
            var comp = gameObject.GetComponent<T>();
            return comp == null ? gameObject.AddComponent<T>() : comp;
        }

        public static TRet GetOrAddComponent<TRet, TComp>(this GameObject gameObject) where TComp : Component, TRet {
            var comp = gameObject.GetComponent<TRet>();
            return comp == null ? gameObject.AddComponent<TComp>() : comp;
        }

        public static Component GetOrAddComponent(this GameObject gameObject, Type type) {
            var comp = gameObject.GetComponent(type);
            return comp == null ? gameObject.AddComponent(type) : comp;
        }
    }
}