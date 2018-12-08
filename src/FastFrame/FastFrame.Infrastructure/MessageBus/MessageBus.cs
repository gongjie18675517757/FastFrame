using CSRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static CSRedis.CSRedisClient;

namespace FastFrame.Infrastructure.MessageBus
{
    /// <summary>
    /// 消息总线
    /// </summary>
    public class MessageBus : IMessageBus
    {
        private readonly CSRedisClient redisClient;
        private readonly IServiceProvider serviceProvider;

        public MessageBus(CSRedisClient redisClient, IServiceProvider serviceProvider)
        {
            this.redisClient = redisClient;
            this.serviceProvider = serviceProvider;
        }

        public async Task PubLishAsync<T>(Message<T> message) where T : class
        {
            await redisClient.PublishAsync($"Message:{typeof(T).Name}", message.ToJson());
        }

        public void SubscribeAsync<T>() where T : class
        {
            redisClient.Subscribe(($"Message:{typeof(T).Name}", ExecHandle<T>));
        }

        private async void ExecHandle<T>(SubscribeMessageEventArgs msg) where T : class
        {
            var message = msg.Body.ToObject<Message<T>>();
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                var services = serviceProvider.GetServices<IAsyncMessageHandle<T>>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                foreach (var item in services)
                {
                    try
                    {
                        await item.Handle(message);
                    }
                    catch (Exception ex)
                    {
                        loggerFactory.CreateLogger<MessageBus>().LogError(ex, "发生异常!");
                    }
                }
            }
        }
    }
}
