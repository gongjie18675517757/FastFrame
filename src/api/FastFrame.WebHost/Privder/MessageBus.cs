using CSRedis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using static CSRedis.CSRedisClient;
using FastFrame.Infrastructure.MessageBus;
using FastFrame.Infrastructure;
using System.Reflection;

namespace FastFrame.WebHost
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

        public async Task PubLishAsync<T>(Message<T> message) where T : class, new()
        {
            await redisClient.PublishAsync($"Message:{typeof(T).Name}", message.ToJson());
        }

        public void SubscribeAsync<T>() where T : class, new()
        {
            redisClient.Subscribe(($"Message:{typeof(T).Name}", ExecHandle<T>));
        }

        public void SubscribeAsync(Type msgType)
        {
            var method = GetType().GetMethod(nameof(ExecHandle), BindingFlags.NonPublic | BindingFlags.Instance);

            var action = method.MakeGenericMethod(msgType);


            redisClient.Subscribe(($"Message:{msgType.Name}", new Action<SubscribeMessageEventArgs>(msgType => action.Invoke(this, new object[] { msgType }))));
        }



        private async void ExecHandle<T>(SubscribeMessageEventArgs msg) where T : class, new()
        {
            var message = msg.Body.ToObject<Message<T>>();
            using var serviceScope = serviceProvider.CreateScope();
            var loader = serviceScope.ServiceProvider;
            var services = loader.GetServices<IAsyncMessageHandle<T>>();
            var loggerFactory = loader.GetService<ILoggerFactory>();
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
