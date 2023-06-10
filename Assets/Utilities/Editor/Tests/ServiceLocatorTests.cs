using NUnit.Framework;
using Utilities.Runtime;

public class ServiceLocatorTests
{
    private interface ITestService { }
    private class TestService : ITestService { }

    [TearDown]
    public void TearDown()
    {
        ServiceLocator.RemoveAll();
    }
    
    [Test]
    public void WhenBind_AndGenericNotInterface_ThenException()
    {
        var testService = new TestService();
        
        var e = Assert.Catch(() => ServiceLocator.Bind<TestService>(testService));
        
        Assert.IsTrue(e.Message.Contains("is not interface!"));
    }
    
    [Test]
    public void WhenBind_AndResolve()
    {
        var testService = new TestService();
        ServiceLocator.Bind<ITestService>(testService);
        var testServiceResolved = ServiceLocator.Resolve<ITestService>();
        Assert.IsTrue(testService == testServiceResolved);
    }

    [Test]
    public void WhenBind_AndAlreadyBind_ThenException()
    {
        var testService = new TestService();
        ServiceLocator.Bind<ITestService>(testService);
        
        var e = Assert.Catch(() => ServiceLocator.Bind<ITestService>(testService));
        
        Assert.IsTrue(e.Message.Contains("This service was already bind!"));
    }
    
    [Test]
    public void WhenRemove_AndResolve()
    {
        var testService = new TestService();
        ServiceLocator.Bind<ITestService>(testService);

        Assert.IsTrue(ServiceLocator.Remove<ITestService>());
        
        Assert.IsTrue(ServiceLocator.Resolve<ITestService>() == null);
    }
    
    [Test]
    public void WhenRemove_ThenFalse()
    {
        Assert.IsFalse(ServiceLocator.Remove<ITestService>());
    }
}
