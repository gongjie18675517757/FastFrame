using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
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

        public async Task TriggerEventAsync<T>(T @event) where T : IEventData
        {
            var servers = serviceProvider.GetServices<IEventHandle<T>>().OrderByDescending(v => v.Weights);
            foreach (var server in servers)
            {
                await server.HandleEventAsync(@event);
            }
        }

        public async Task<TResult> RequestAsync<TResult, TRequest>(TRequest request)
        {
            var requestHandle = serviceProvider.GetService<IRequestHandle<TResult, TRequest>>();
            if (requestHandle != null)
            {
                return await requestHandle.HandleRequestAsync(request);
            }
            else
            {
                throw new Exception($"未匹配到请求处理服务！{typeof(TRequest)}=>${typeof(TResult)}");
            }
        }
    }
}
