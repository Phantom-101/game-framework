using Framework.Inspector;
using Framework.Variables;
using UnityEngine;

namespace Framework.Producers {
    public class ProducerSetter : VariableSetter<IProducer> {
        [field: SerializeReference]
        [field: SelectImplementation(typeof(IProducer))]
        public override IProducer Value { get; set; }
    }
}