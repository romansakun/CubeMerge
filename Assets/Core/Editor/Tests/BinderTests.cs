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
        Binder.RemoveAll();
    }
    
    [Test]
    public void WhenBind_AndResolve()
    {
        var testService = new TestService();
        Binder.Bind<TestService>(testService);
        var testServiceResolved = Binder.Resolve<TestService>();
        Assert.IsTrue(testService == testServiceResolved);
    }

    [Test]
    public void WhenBind_AndAlreadyBind_ThenException()
    {
        var testService = new TestService();
        Binder.Bind<TestService>(testService);
        
        var e = Assert.Catch(() => Binder.Bind<TestService>(testService));
        
        Assert.IsTrue(e.Message.Contains("This service was already bind!"));
    }
    
    [Test]
    public void WhenRemove_AndResolve() 
    {
        var testService = new TestService();
        Binder.Bind<TestService>(testService);

        Assert.IsTrue(Binder.Remove<TestService>());
        var e = Assert.Catch(() => Binder.Resolve<TestService>());
        
        Assert.IsTrue(e.Message.Contains("There is no service"));
    }
    
    [Test]
    public void WhenRemove_ThenFalse()
    {
        Assert.IsFalse(Binder.Remove<TestService>());
    }
}
