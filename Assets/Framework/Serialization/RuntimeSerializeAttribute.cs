using System;

namespace Framework.Serialization {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RuntimeSerializeAttribute : Attribute { }
}