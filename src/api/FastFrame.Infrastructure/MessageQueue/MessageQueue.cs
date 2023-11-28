using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.MessageQueue;
using System.Linq;
using System.Reflection;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.MessageQueue
{
    /// <summary>
    /// 消息队列
    /// </summary>
    public class MessageQueue(StackExchange.Redis.ConnectionMultiplexer redisClient, IBackgroundJob backgroundJob) 
        : IMessageQueue, IApplicationInitialLifetime, IApplicationUnInitialLifetime
    {
        private readonly StackExchange.Redis.ConnectionMultiplexer redisClient = redisClient;
        private readonly IBackgroundJob backgroundJob = backgroundJob;

        public async Task InitialAsync()
        {
            /*收集所有订阅的*/
            foreach (var item in MessageQueueServiceCollectionExtensions.SubscribeMethodList)
            {
                var k = new StackExchange.Redis.RedisChannel(item.Key, StackExchange.Redis.RedisChannel.PatternMode.Auto);
                await redisClient.GetSubscriber().SubscribeAsync(k, OnSubscribe);
            }

        }

        public async Task UnInitialAsync()
        {
            foreach (var item in MessageQueueServiceCollectionExtensions.SubscribeMethodList)
            {
                var k = new StackExchange.Redis.RedisChannel(item.Key, StackExchange.Redis.RedisChannel.PatternMode.Auto);

                await redisClient.GetSubscriber().UnsubscribeAsync(k, OnSubscribe);
            }
        }

        /// <summary>
        /// 触发订阅事件
        /// </summary> 
        private void OnSubscribe(StackExchange.Redis.RedisChannel channel, StackExchange.Redis.RedisValue e)
        {
            if (MessageQueueServiceCollectionExtensions.SubscribeMethodList.TryGetValue(channel, out var list))
            {
                var settimeOutMethod = typeof(IBackgroundJob)
                    .GetMethod(nameof(IBackgroundJob.SetTimeoutByMethod), BindingFlags.Instance | BindingFlags.Public);

                foreach (var g in list.GroupBy(v => v.type))
                {
                    /*创建延时方法*/
                    var methodInfo = settimeOutMethod.MakeGenericMethod(g.Key);

                    foreach (var (_, method) in g)
                    {
                        /*消息参数类型*/
                        var msgType = method.GetParameters()[1].ParameterType;

                        /*尝试序列化为强类型*/
                        if (!e.ToString().TryToObject(msgType, out object msgData))
                            msgData = e.ToString();


                        /*执行延时方法（将订阅的方法放入队列）*/
                        methodInfo.Invoke(backgroundJob, new object[] {
                            method,
                            new object[]{
                                channel.ToString(),
                                msgData
                            },
                            null
                        });
                    }
                }
            }
        }

        public async Task<string> PublishAsync(string channel, string msg)
        {
            var k = new StackExchange.Redis.RedisChannel(channel, StackExchange.Redis.RedisChannel.PatternMode.Auto);
            var msgId = await redisClient.GetSubscriber().PublishAsync(k, msg);

            return msgId.ToString();
        }
    }
}