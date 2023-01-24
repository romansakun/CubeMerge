using System;

namespace MVVM.Runtime.ReactiveProperties
{
    public interface IReactiveProperty<T>
    {
        T Value { get; }
        
        void AddListener(Action action);
        void AddOnce(Action action);
        void RemoveListener(Action action);
    }
}