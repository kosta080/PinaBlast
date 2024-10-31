using System.Collections.Generic;

namespace Kor.Infra
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<string, object> Services = new();

        public static T Register<T>(T service, bool asSingle = false)
        {
            var key = typeof(T).Name;

            if (asSingle)
            {
                if (!Services.ContainsKey(key))
                {
                    Services[key] = service;
                }
            }
            else
            {
                Services[key] = service;
            }

            return service;
        }

        public static T Resolve<T>()
        {
            var key = typeof(T).Name;
            return Services.ContainsKey(key) ? (T)Services[key] : default;
        }
    }
}