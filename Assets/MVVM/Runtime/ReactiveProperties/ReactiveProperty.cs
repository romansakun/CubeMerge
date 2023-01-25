using System;
using System.Collections.Generic;

namespace MVVM.Runtime.ReactiveProperties
{
    public sealed class ReactiveProperty<T> : IReactiveProperty<T>, IDisposable
    {
        private static readonly IEqualityComparer<T> EqualityComparer = EqualityComparer<T>.Default;
        
        private List<Delegate> _addOnceListeners = new List<Delegate>(10);
        private Action _onChanged = () => {};
        private T _value;
        private bool _isDisposed;
        
        
        public T Value
        {
            get => _value;
            set
            {
                if (_isDisposed)
                    return;
                
                if (EqualityComparer.Equals(value, _value))
                    return;
                
                _value = value;
                InvokeChanging();
            }
        }

        public ReactiveProperty()
        {
            _value = default;
        }

        public ReactiveProperty(T value)
        {
            _value = value;
        }
        
        public ReactiveProperty(ReactiveProperty<T> fromOther)
        {
            _value = fromOther._value;
        }

        public void InvokeChanging()
        {
            if (_isDisposed)
                return;
            
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
            if (_isDisposed)
                return;
            
            _onChanged += action;
        }

        public void AddOnce(Action action)
        {
            if (_isDisposed)
                return;
            
            _onChanged += action;
            _addOnceListeners.Add(action);
        }

        public void RemoveListener(Action action)
        {
            if (_isDisposed)
                return;
            
            _onChanged -= action;
            if (_addOnceListeners.Contains(action))
                _addOnceListeners.Remove(action);
        }

        public void Dispose()
        {
            _isDisposed = true;
            _onChanged = null;
            _addOnceListeners = null;
            _value = default;
        }
    }
}