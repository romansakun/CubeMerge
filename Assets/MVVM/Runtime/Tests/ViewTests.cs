using System.Collections;
using MVVM.Runtime;
using MVVM.Runtime.ReactiveProperties;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ViewTests
{
    private class TestViewModel : IViewModel
    {
        private ReactiveProperty<int> _intProperty;
        public IReactiveProperty<int> IntProperty => _intProperty;

        public TestViewModel() => _intProperty = new ReactiveProperty<int>(5);
        public void IncrementIntProperty() => _intProperty.Value++;
        public void Dispose() => _intProperty.Dispose();
    }

    private class TestView : View<TestViewModel>
    {
        public int ListenerInvokedCount = 0;
        
        public override void Init(TestViewModel viewModel)
        {
            UpdateViewModel(viewModel);
            AddListener(_viewModel.IntProperty, () => ListenerInvokedCount++);
        }
    }

    [UnityTest]
    public IEnumerator WhenInit_ThenListenerInvoked()
    {
        var testViewModel = new TestViewModel();
        var testView = new GameObject().AddComponent<TestView>();
        
        testView.Init(testViewModel);
        yield return null;
        
        Assert.AreEqual(1, testView.ListenerInvokedCount);
    }
    
    [UnityTest]
    public IEnumerator WhenInit_AndViewModelIsNull_ThenViewModelIsMissingException()
    {
        var testView = new GameObject().AddComponent<TestView>();
        yield return null;
        
        var errMsg = Assert.Catch(() => testView.Init(null)).Message;

        StringAssert.Contains("view model is missing", errMsg);
    }
    
    [UnityTest]
    public IEnumerator WhenTwoViewModelWasInit_AndIncrementIntPropertyOnce_ThenListenerInvokedTwoTimes()
    {
        var testViewModelOne = new TestViewModel();
        var testViewModelTwo = new TestViewModel();
        var testView = new GameObject().AddComponent<TestView>();
        
        testView.Init(testViewModelOne);
        testView.Init(testViewModelTwo);
        testViewModelOne.IncrementIntProperty();
        yield return null;
        
        Assert.AreEqual(2, testView.ListenerInvokedCount);
    }
    
    [UnityTest]
    public IEnumerator WhenViewDestroyed_AndIncrementIntPropertyOnce_ThenListenerInvokedOneTime()
    {
        var testViewModel = new TestViewModel();
        var testView = new GameObject().AddComponent<TestView>();
        
        testView.Init(testViewModel);
        Object.Destroy(testView.gameObject);
        yield return null;
        testViewModel.IncrementIntProperty();

        // testView is still not null and we can check ListenerInvokedCount
        Assert.AreEqual(1, testView.ListenerInvokedCount);
    }
    
    [UnityTest]
    public IEnumerator WhenTwoViews_AndIncrementIntPropertyOnce_ThenListenerInvokedTwoTimes()
    {
        var testViewModel = new TestViewModel();
        var testViewOne = new GameObject().AddComponent<TestView>();
        var testViewTwo = new GameObject().AddComponent<TestView>();
        testViewOne.Init(testViewModel);
        testViewTwo.Init(testViewModel);
        
        yield return null;
        testViewModel.IncrementIntProperty();

        Assert.AreEqual(2, testViewOne.ListenerInvokedCount);
        Assert.AreEqual(2, testViewTwo.ListenerInvokedCount);
    }
}
