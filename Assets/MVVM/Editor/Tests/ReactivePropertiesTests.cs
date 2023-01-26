using System.Collections.Generic;
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
    
    [Test]
    public void WhenDispose_ThenListenerNotInvoked()
    {
        ReactiveProperty<object> reactiveProperty = new ReactiveProperty<object>();
        IReactiveProperty<object> propertyInterface = reactiveProperty;
        int listenerInvokeCount = 0;
        void TestListenerAction() => listenerInvokeCount++;
        propertyInterface.AddListener(TestListenerAction);
        reactiveProperty.Dispose();

        reactiveProperty.Value = "first new value";
        reactiveProperty.Value = "second new value";
        
        Assert.AreEqual(0, listenerInvokeCount);
    }
    
    [Test]
    public void WhenDoAction_ThenListenerWasInvoked()
    {
        ReactiveProperty<Dictionary<int, int>> reactiveProperty = 
            new ReactiveProperty<Dictionary<int, int>>(new Dictionary<int, int>()
            {
                {0, 0}, {1, 1}
            });
        IReactiveProperty<Dictionary<int, int>> propertyInterface = reactiveProperty;
        int listenerInvokeCount = 0;
        void TestListenerAction() => listenerInvokeCount++;
        propertyInterface.AddListener(TestListenerAction);
        
        reactiveProperty.DoAction(dict => dict[0] = 5);
        
        Assert.AreEqual(1, listenerInvokeCount);
    }
}
