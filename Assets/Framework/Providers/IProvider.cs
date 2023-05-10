#nullable enable
namespace Framework.Providers {
    public interface IProvider<out T> {
        T Provide();
    }
}