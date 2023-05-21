#nullable enable
using Framework.Inspector;
using Framework.Variables;
using UnityEngine;

namespace Framework.Producers {
    [CreateAssetMenu(menuName = "Variable/Producer", fileName = "NewProducer")]
    public class ProducerVariable : ScriptableVariable<IProducer> {
        [field: SerializeReference]
        [field: SelectImplementation(typeof(IProducer))]
        public override IProducer Value { get; set; } = null!;
    }
}