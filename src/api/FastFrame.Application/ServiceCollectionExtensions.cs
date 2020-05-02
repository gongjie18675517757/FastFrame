using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FastFrame.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var interfaceType = typeof(IService);
            var types = interfaceType.Assembly.GetTypes()
                .Where(x =>
                    interfaceType.IsAssignableFrom(x) &&
                    x.IsClass && !x.IsAbstract &&
                    !x.Name.StartsWith("BaseService"));

            foreach (var type in types)
            {
                foreach (var interfaceItem in type.GetInterfaces().Where(x => x != interfaceType))
                {
                    services.AddScoped(interfaceItem, type);
                }
                services.AddScoped(type);
            } 
            
            services.AddScoped(typeof(HandleOne2ManyService<,>));
            return services;
        }
    }
}
