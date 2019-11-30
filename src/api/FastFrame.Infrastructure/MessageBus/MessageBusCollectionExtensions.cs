using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace FastFrame.Infrastructure.MessageBus
{
    public static class MessageBusCollectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, Assembly assembly)
        {
            //var interfaceType = typeof(IAsyncMessageHandle);
            //var types = assembly.GetTypes()
            //    .Where(x => interfaceType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
            //foreach (var type in types)
            //{
            //    foreach (var interfaceItem in type.GetInterfaces().Where(x => x != interfaceType))
            //    {
            //        services.AddScoped(interfaceItem, type);
            //    }
            //    services.AddScoped(type);
            //}
            services.AddSingleton<IMessageBus, MessageBus>();
            return services;
        }
    }
}
