#nullable enable
namespace Framework.Producers {
    public interface IProducer {
        object Produce();
    }
    
    public interface IProducer<out T> : IProducer {
        new T Produce();
    }
}