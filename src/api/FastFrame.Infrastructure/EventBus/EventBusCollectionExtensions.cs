using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace FastFrame.Infrastructure.EventBus
{
    public static class EventBusCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services,Assembly assembly)
        { 
            services.AddScoped<IEventBus, EventBus>();
            return services;
        }
    }

   
}
