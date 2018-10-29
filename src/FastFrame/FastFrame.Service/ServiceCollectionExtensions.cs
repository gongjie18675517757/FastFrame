using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FastFrame.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var interfaceType = typeof(IService);
            var types = interfaceType.Assembly.GetTypes()
                .Where(x => interfaceType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
            foreach (var type in types)
            {
                foreach (var interfaceItem in type.GetInterfaces().Where(x => x != interfaceType))
                {
                    services.AddScoped(interfaceItem, type); 
                }
                services.AddScoped(type);
            }
            return services;
        }
    }
}
