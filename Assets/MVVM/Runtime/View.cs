using System;
using System.Collections.Generic;
using UnityEngine;
using MVVM.Runtime.ReactiveProperties;

namespace MVVM.Runtime
{
    public abstract class View<T> : MonoBehaviour, IView<T> where T: IViewModel
    {
        private List<Action> _listenerRemovingActions = new List<Action>(10);
        
        protected T _viewModel;

        public abstract void Init(T viewModel);

        protected void UpdateViewModel(T viewModel)
        {
            if (viewModel == null)
                throw new Exception($"In '{GetType().Name}' -is view type, '{gameObject.name}' -is name of gameObject : view model is missing!");

            RemoveAllListeners();

            _viewModel = viewModel;
        }

        protected void AddListener<TRP>(IReactiveProperty<TRP> reactiveProperty, Action listener, bool invokeImmediately = true)
        {
            if (_viewModel == null)
                throw new Exception($"In '{GetType().Name}' -is view type, '{gameObject.name}' -is name of gameObject : view model is missing!");
            
            if (reactiveProperty == null)
                throw new Exception($"In '{_viewModel.GetType().Name}' - is ViewModel : reactive property is missing!");

            var wrapListenerInvoking = new Action(() =>
            {
                if (this)
                    listener();
            });
            reactiveProperty.AddListener(wrapListenerInvoking);
            
            var wrapListenerRemoving =  new Action(() =>
            {
                reactiveProperty.RemoveListener(wrapListenerInvoking);
            });
            _listenerRemovingActions.Add(wrapListenerRemoving);

            if (invokeImmediately)
                listener();
        }

        private void RemoveAllListeners()
        {
            if (_viewModel == null)
                return;
            
            _listenerRemovingActions.ForEach((removeListener) => removeListener());
            _listenerRemovingActions.Clear();
        }

        private void OnDestroy()
        {
            RemoveAllListeners();
        }
    }
}