using NUnit.Framework;
using Core.Runtime;

public class BinderTests
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
        ServiceRegistry.RemoveAll();
    }
    
    [Test]
    public void WhenBind_AndResolve()
    {
        var testService = new TestService();
        ServiceRegistry.Add<TestService>(testService);
        var testServiceResolved = ServiceRegistry.Get<TestService>();
        Assert.IsTrue(testService == testServiceResolved);
    }

    [Test]
    public void WhenBind_AndAlreadyBind_ThenException()
    {
        var testService = new TestService();
        ServiceRegistry.Add<TestService>(testService);
        
        var e = Assert.Catch(() => ServiceRegistry.Add<TestService>(testService));
        
        Assert.IsTrue(e.Message.Contains("This service was already bind!"));
    }
    
    [Test]
    public void WhenRemove_AndResolve() 
    {
        var testService = new TestService();
        ServiceRegistry.Add<TestService>(testService);

        Assert.IsTrue(ServiceRegistry.Remove<TestService>());
        var e = Assert.Catch(() => ServiceRegistry.Get<TestService>());
        
        Assert.IsTrue(e.Message.Contains("There is no service"));
    }
    
    [Test]
    public void WhenRemove_ThenFalse()
    {
        Assert.IsFalse(ServiceRegistry.Remove<TestService>());
    }
}
