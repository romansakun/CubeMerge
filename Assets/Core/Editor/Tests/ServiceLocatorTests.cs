using NUnit.Framework;
using Core.Runtime;

public class ServiceLocatorTests
{
    private class TestService : IService
    {
        public void Dispose()
        {
            
        }
    }

    [TearDown]
    public void TearDown()
    {
        ServiceLocator.RemoveAll();
    }
    
    [Test]
    public void WhenBind_AndResolve()
    {
        var testService = new TestService();
        ServiceLocator.Bind<TestService>(testService);
        var testServiceResolved = ServiceLocator.Resolve<TestService>();
        Assert.IsTrue(testService == testServiceResolved);
    }

    [Test]
    public void WhenBind_AndAlreadyBind_ThenException()
    {
        var testService = new TestService();
        ServiceLocator.Bind<TestService>(testService);
        
        var e = Assert.Catch(() => ServiceLocator.Bind<TestService>(testService));
        
        Assert.IsTrue(e.Message.Contains("This service was already bind!"));
    }
    
    [Test]
    public void WhenRemove_AndResolve() 
    {
        var testService = new TestService();
        ServiceLocator.Bind<TestService>(testService);

        Assert.IsTrue(ServiceLocator.Remove<TestService>());
        
        Assert.IsTrue(ServiceLocator.Resolve<TestService>() == null);
    }
    
    [Test]
    public void WhenRemove_ThenFalse()
    {
        Assert.IsFalse(ServiceLocator.Remove<TestService>());
    }
}
