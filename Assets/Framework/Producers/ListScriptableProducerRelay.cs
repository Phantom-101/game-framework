using System;
using System.Collections.Generic;

namespace Framework.Producers {
    [Serializable]
    public class ListScriptableProducerRelay<T> : ScriptableProducerRelay<List<T>> { }
}