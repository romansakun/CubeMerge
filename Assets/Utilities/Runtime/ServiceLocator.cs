using System;
using System.Collections.Generic;

namespace Utilities.Runtime
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Bind<T>(T service) where T : class
        {
            if (_services.ContainsKey(typeof(T)))
                throw new Exception("This service was already bind!");
            
            _services.Add(typeof(T), service);
        }

        public static T Resolve<T>() where T : class
        {
            return _services.ContainsKey(typeof(T)) 
                ? _services[typeof(T)] as T 
                : null;
        }

        public static bool Remove<T>()
        {
            return _services.Remove(typeof(T));
        }

        public static void RemoveAll()
        {
            _services.Clear();
        }
    }
}