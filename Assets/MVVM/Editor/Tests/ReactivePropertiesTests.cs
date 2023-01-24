using MVVM.Runtime.ReactiveProperties;
using NUnit.Framework;
using UnityEngine;

public class ReactivePropertyTests
{
    [Test]
    public void WhenChangeValue_AndAddListener_ThenListenerInvoke()
    {
        ReactiveProperty<object> reactiveProperty = new ReactiveProperty<object>();
        IReactiveProperty<object> propertyInterface = reactiveProperty;

        bool isListenerWasInvoked = false;
        propertyInterface.AddListener(() =>
        {
            Debug.Log($"property was changed: {reactiveProperty.Value}");
            isListenerWasInvoked = true;
        });
        
        reactiveProperty.Value = "this is new value";
        
        Assert.IsTrue(isListenerWasInvoked);
    }

    [Test]
    public void WhenChangeValueTwice_AndAddOnce_ThenListenerInvokeOnce()
    {
        ReactiveProperty<object> reactiveProperty = new ReactiveProperty<object>();
        IReactiveProperty<object> propertyInterface = reactiveProperty;

        int listenerInvokeCount = 0;
        propertyInterface.AddOnce(() =>
        {
            Debug.Log($"property was changed: {reactiveProperty.Value}");
            listenerInvokeCount++;
        });
        
        reactiveProperty.Value = "first new value";
        reactiveProperty.Value = "second new value";
        
        Assert.AreEqual(1, listenerInvokeCount);
    }
    
    [Test]
    public void WhenChangeValue_AndAddAndRemoveListener_ThenListenerNotInvoked()
    {
        ReactiveProperty<object> reactiveProperty = new ReactiveProperty<object>();
        IReactiveProperty<object> propertyInterface = reactiveProperty;

        int listenerInvokeCount = 0;
        void TestListenerAction() => listenerInvokeCount++;
        propertyInterface.AddListener(TestListenerAction);
        propertyInterface.RemoveListener(TestListenerAction);

        reactiveProperty.Value = "first new value";
        reactiveProperty.Value = "second new value";
        
        Assert.AreEqual(0, listenerInvokeCount);
    }
}
