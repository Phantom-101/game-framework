using System;

namespace Framework.Producers {
    public interface IPoolable {
        event EventHandler OnRequestRelease;

        void OnGet();
        
        void OnRelease();

        void Destroy();
    }
}