using System;
using System.Collections.Generic;

namespace Framework.Producers {
    [Serializable]
    public class DictionaryScriptableProducerRelay<TKey, TValue> : ScriptableProducerRelay<Dictionary<TKey, TValue>> { }
}