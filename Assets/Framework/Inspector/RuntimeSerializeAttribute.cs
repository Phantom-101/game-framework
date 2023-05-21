using System;

namespace Framework.Inspector {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RuntimeSerializeAttribute : Attribute { }
}