using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace FastFrame.Infrastructure.EventBus
{
    public static class EventBusCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services,Assembly assembly)
        {
            var interfaceType = typeof(IEventHandleService);
            var types = assembly.GetTypes()
                .Where(x => interfaceType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
            foreach (var type in types)
            {
                foreach (var interfaceItem in type.GetInterfaces().Where(x => x != interfaceType))
                {
                    services.AddScoped(interfaceItem, type);
                }
                services.AddScoped(type);
            }

            services.AddScoped<IEventBus, EventBus>();
            return services;
        }
    }
}
