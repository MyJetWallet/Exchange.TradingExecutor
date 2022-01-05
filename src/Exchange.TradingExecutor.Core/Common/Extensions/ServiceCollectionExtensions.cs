using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Exchange.TradingExecutor.Core.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMarkedBy<T>(this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped, params Assembly[] assemblies)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }
    }
}