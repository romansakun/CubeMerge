using System;
using System.Collections.Generic;

namespace MVVM.Runtime.ReactiveProperties
{
    public sealed class ReactiveProperty<T> : IReactiveProperty<T>
    {
        private static readonly IEqualityComparer<T> EqualityComparer = EqualityComparer<T>.Default;
        private readonly List<Delegate> _addOnceListeners = new List<Delegate>(10);

        private Action _onChanged = () => { };


        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer.Equals(value, _value))
                    return;
                
                _value = value;
                InvokeChange();
            }
        }

        public void InvokeChange()
        {
            _onChanged.Invoke();
            
            if (_addOnceListeners.Count <= 0) 
                return;
            
            var listeners = _onChanged.GetInvocationList();
            for (int i = listeners.Length - 1; i >= 0; i--)
            {
                if (_addOnceListeners.Contains(listeners[i]))
                    _onChanged -= (Action) listeners[i];
            }
            _addOnceListeners.Clear();
        }

        public void AddListener(Action action)
        {
            _onChanged += action;
        }

        public void AddOnce(Action action)
        {
            _onChanged += action;
            _addOnceListeners.Add(action);
        }

        public void RemoveListener(Action action)
        {
            _onChanged -= action;
            if (_addOnceListeners.Contains(action))
                _addOnceListeners.Remove(action);
        }
    }
}