using System.Collections.Generic;
using System.Reflection;

namespace Framework.Serialization {
    public interface ISerializable {
        IEnumerable<FieldInfo> GetSerializableFields(SerializationMode mode);
    }
}