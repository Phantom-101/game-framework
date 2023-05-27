using System;
using System.Collections.Generic;

namespace Framework.Producers {
    [Serializable]
    public class SetScriptableProducerRelay<T> : ScriptableProducerRelay<HashSet<T>> { }
}