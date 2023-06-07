using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Utilities.Runtime;


public class PoolTests
{
    //get object from pool
    //return object in pool
    
    
    // A Test behaves as an ordinary method
    [Test]
    public void PoolTestsSimplePasses()
    {
        try
        {
            var pool = new Pool<PoolObject>(new PoolObjectFactory());
            pool.Get();
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    // A Test behaves as an ordinary method
    [Test]
    public void WhenGetAndReturn100Objects()
    {
        var pool = new Pool<PoolObject>(new PoolObjectFactory(), 100);
        for (int i = 0; i < 100; i++)
        {
            var obj = pool.Get();
            obj.Value = "value";
            
            pool.Return(obj);
            
            Assert.IsTrue(obj.Value == String.Empty);
        }
    }
}
