using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Framework.Inspector {
    public abstract class SerializableMonoBehaviour : MonoBehaviour, ISerializable {
        public virtual IEnumerable<FieldInfo> GetSerializableFields(SerializationMode mode) {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var members = GetType().GetFields(flags);
            var filtered = members.Where(e => e.DeclaringType?.IsSubclassOf(typeof(MonoBehaviour)) ?? false);
            switch (mode) {
                case SerializationMode.Editor:
                    return filtered.Where(e =>
                        e.GetCustomAttributes(true)
                            .Any(a => a is EditorSerializeAttribute or AlwaysSerializeAttribute));
                case SerializationMode.Runtime:
                    return filtered.Where(e =>
                        e.GetCustomAttributes(true)
                            .Any(a => a is RuntimeSerializeAttribute or AlwaysSerializeAttribute));
                default:
                    throw new ArgumentException($"Unknown serialization mode {mode}");
            }
        }
    }
}