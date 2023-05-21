using System.Collections.Generic;
using System.Reflection;

namespace Framework.Inspector {
    public interface ISerializable {
        IEnumerable<FieldInfo> GetSerializableFields(SerializationMode mode);
    }
}