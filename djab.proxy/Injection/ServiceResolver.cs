using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace djab.proxy.Injection
{
    public class ServiceResolver
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public ServiceResolver()
        {
            _assemblies = DependencyContext.Default.RuntimeLibraries.SelectMany(lib => lib.GetDefaultAssemblyNames(DependencyContext.Default)
                .Where(n => n.FullName.ToLowerInvariant().StartsWith("djab"))
                .Select(Assembly.Load));
        }
        public IEnumerable<Type> ResolveByAttribute(Type attributeType)
        {
            return _assemblies.SelectMany(assembly => assembly.ExportedTypes.Where(t => t.GetTypeInfo().GetCustomAttribute(attributeType) != null)
                .Where(t => !t.GetTypeInfo().IsAbstract));
        }
    }
}
