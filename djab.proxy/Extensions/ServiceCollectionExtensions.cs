using djab.proxy.Injection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;
using System.Reflection;

namespace djab.proxy.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInjectableServices(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var resolver = new ServiceResolver();
            foreach (var serviceType in resolver.ResolveByAttribute(typeof(InjectableAttribute)))
            {
                var attribute = serviceType.GetTypeInfo().GetCustomAttribute<InjectableAttribute>();
                services.TryAdd(new ServiceDescriptor(attribute.Interface ?? serviceType, serviceType, attribute.Lifetime ?? serviceLifetime));
            }
            return services;
        }
    }
}
