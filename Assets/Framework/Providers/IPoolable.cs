using System;

namespace Framework.Providers {
    public interface IPoolable {
        event EventHandler OnRequestRelease;

        void OnGet();
        
        void OnRelease();

        void Destroy();
    }
}