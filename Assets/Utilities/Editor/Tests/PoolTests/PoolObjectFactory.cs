using Utilities.Runtime;

public class PoolObjectFactory : IFactory<PoolObject>
{
    public PoolObject Create()
    {
        return new PoolObject();
    }
}