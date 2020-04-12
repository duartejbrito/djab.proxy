using AutoMapper;
using djab.proxy.Injection;
using djab.proxy.Profiles;
using Microsoft.AspNetCore.Http;
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

        public static void AddAutomapperConfiguration(this IServiceCollection services, IHttpContextAccessor httpContextAccessor)
        {
            var automapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new YtsRssProfile(httpContextAccessor));
            });

            var autoMapper = automapperConfig.CreateMapper();

            services.AddSingleton(autoMapper);
        }
    }
}
