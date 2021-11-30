using FastFrame.Entity;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FastFrame.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            /*所有服务类*/
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

            /*注册自动编码事件*/
            //interfaceType = typeof(IHaveNumber);
            //types = interfaceType.Assembly.GetTypes()
            //    .Where(x =>
            //        interfaceType.IsAssignableFrom(x) &&
            //        x.IsClass && !x.IsAbstract);

            //foreach (var type in types)
            //{
            //    var eventHandleType = typeof(IEventHandle<>).MakeGenericType(typeof(EntityAdding<>).MakeGenericType(type));
            //    var implementedType = typeof(HandleHaveNumberService<>).MakeGenericType(type);

            //    services.AddScoped(eventHandleType, implementedType);
            //}        

            /*注册审批事件处理*/
            interfaceType = typeof(IHaveCheck);
            types = interfaceType.Assembly.GetTypes()
                .Where(x =>
                    interfaceType.IsAssignableFrom(x) &&
                    x.IsClass && !x.IsAbstract);

            foreach (var type in types)
            {
                var eventHandleType = typeof(IEventHandle<>).MakeGenericType(typeof(Flow.FlowOperated<>).MakeGenericType(type));
                var implementedType = typeof(Flow.HandleFlowOperatedService<>).MakeGenericType(type);

                services.AddScoped(eventHandleType, implementedType);
            }

            services.AddScoped(typeof(HandleOne2ManyService<,>));
            return services;
        }
    }
}
