using System;
using Utilities.Runtime;

public class PoolObject : IPoolObject
{
    public static int NumberCounter;
    public int Number;
    public string Value = String.Empty;
    public IPoolObject Create()
    {
        NumberCounter++;
        Number = NumberCounter;
        return new PoolObject();
    }

    public void Reset()
    {
        Value = String.Empty;
    }

    public void Destroy()
    {
            
    }
}