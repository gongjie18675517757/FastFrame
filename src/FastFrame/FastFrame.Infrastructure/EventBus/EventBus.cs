using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IServiceProvider serviceProvider;

        public EventBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task TriggerAsync<T>(IEventData<T> @event)
        {
            var servers = serviceProvider.GetServices<IEventHandle<T>>();
            foreach (var server in servers)
            {
                await server.HandleEventAsync(@event);
            }
        }
    }
}
