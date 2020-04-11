using Microsoft.Extensions.DependencyInjection;
using System;

namespace djab.proxy.Injection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectableAttribute : Attribute
    {
        public Type Interface { get; set; } = null;
        public ServiceLifetime? Lifetime { get; set; } = ServiceLifetime.Scoped;
    }
}
