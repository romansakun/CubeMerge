using System;
using System.Collections.Generic;

namespace Utilities.Runtime
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Bind<T>(T service) where T : class
        {
            var serviceType = GetServiceType<T>();
            if (_services.ContainsKey(serviceType))
                throw new Exception("This service was already bind!");

            _services.Add(typeof(T), service);
        }

        public static T Resolve<T>() where T : class
        {
            var serviceType = GetServiceType<T>();
            return _services.ContainsKey(serviceType) 
                ? _services[serviceType] as T
                : null;
        }

        public static bool Remove<T>() where T : class
        {
            var serviceType = GetServiceType<T>();
            return _services.Remove(serviceType);
        }

        public static void RemoveAll()
        {
            _services.Clear();
        }

        private static Type GetServiceType<T>() where T : class
        {
            var serviceType = typeof(T);
            if (!serviceType.IsInterface)
                throw new Exception($"{serviceType.FullName} is not interface!");
            
            return serviceType;
        }
    }
}