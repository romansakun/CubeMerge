using System;
using System.Collections.Generic;

namespace Core.Runtime
{
    public static class Binder
    {
        private static readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static void Bind<T>(T service) where T : class, IService
        {
            var serviceType = typeof(T);
            if (_services.ContainsKey(serviceType))
                throw new Exception("This service was already bind!");

            _services.Add(typeof(T), service);
        }

        public static bool IsBound<T>()
        {
            return _services.ContainsKey(typeof(T));
        }

        public static T Resolve<T>() where T : class, IService
        {
            var serviceType = typeof(T);
            return _services.ContainsKey(serviceType) 
                ? _services[serviceType] as T
                : throw new Exception($"There is no service: {serviceType.FullName}");
        }

        public static bool Remove<T>() where T : class, IService
        {
            var serviceType = typeof(T);
            if (!_services.ContainsKey(serviceType))
                return false;

            _services[serviceType].Dispose();
            _services.Remove(serviceType);
            return true;
        }

        public static void RemoveAll()
        {
            foreach (var pair in _services)
            {
                pair.Value.Dispose();   
            }
            _services.Clear();
        }
        
    }
}