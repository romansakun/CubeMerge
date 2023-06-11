using System;
using System.Collections.Generic;

namespace Core.Runtime
{
    public class Pool<T> : IDisposable where T: IPoolObject
    {
        private readonly List<T> _objects = new List<T>();
        private readonly Queue<T> _freeObjects = new Queue<T>();
        private IFactory<T> _factory;

        public Pool(IFactory<T> factory, int objectsCount = 0) 
        {
            _factory = factory;
            for (int i = 0; i < objectsCount; i++)
            {
                CreatePoolObject();
            }
        }

        private void CreatePoolObject()
        {
            var obj = _factory.Create();
            _objects.Add(obj);
            _freeObjects.Enqueue(obj);
        }

        public T Get()
        {
            if (_freeObjects.Count <= 0)
            {
                CreatePoolObject();
            }
            return _freeObjects.Dequeue();
        }

        public void Return(T poolObject)
        {
            if (!_objects.Contains(poolObject))
                throw new ArgumentException($"There is no this object in pool!");
            
            poolObject.Reset();
            _freeObjects.Enqueue(poolObject);
        }

        public void Dispose()
        {
            _factory = null;
            _freeObjects.Clear();
            foreach (var poolObject in _objects)
            {
                poolObject.Destroy();
            }
        }
    }
}