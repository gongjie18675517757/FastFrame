using CSRedis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.MessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    /// <summary>
    /// 消息队列
    /// </summary>
    public class MessageQueue : IMessageQueue, IApplicationInitialLifetime, IApplicationUnInitialLifetime
    {
        private readonly CSRedisClient redisClient;
        private readonly IBackgroundJob backgroundJob;
        private static List<CSRedisClient.SubscribeObject> list;

        public MessageQueue(CSRedisClient redisClient, IBackgroundJob backgroundJob)
        {
            this.redisClient = redisClient;
            this.backgroundJob = backgroundJob;
        }

        public async Task InitialAsync()
        {
            await Task.Yield();

            list = new List<CSRedisClient.SubscribeObject>(MessageQueueServiceCollectionExtensions.SubscribeMethodList.Count);

            /*收集所有订阅的*/
            foreach (var item in MessageQueueServiceCollectionExtensions.SubscribeMethodList)
                list.Add(redisClient.Subscribe((item.Key, OnSubscribe)));
            //redisClient.Subscribe((item.Key, e => OnSubscribe(e)));
        }

        public async Task UnInitialAsync()
        {
            await Task.Yield();

            foreach (var item in list)
            {
                item.Unsubscribe();
                item.Dispose();
            }

            list.Clear();
        }

        /// <summary>
        /// 触发订阅事件
        /// </summary>
        /// <param name="e"></param>
        private void OnSubscribe(CSRedisClient.SubscribeMessageEventArgs e)
        {
            if (MessageQueueServiceCollectionExtensions.SubscribeMethodList.TryGetValue(e.Channel, out var list))
            {
                var settimeOutMethod = typeof(IBackgroundJob).GetMethod(nameof(IBackgroundJob.SetTimeoutByMethod), BindingFlags.Instance | BindingFlags.Public);
                foreach (var g in list.GroupBy(v => v.type))
                {
                    /*创建延时方法*/
                    var methodInfo = settimeOutMethod.MakeGenericMethod(g.Key);

                    foreach (var (_, method) in g)
                    {
                        /*消息参数类型*/
                        var msgType = method.GetParameters()[1].ParameterType;

                        /*尝试序列化为强类型*/
                        if (!e.Body.TryToObject(msgType, out object msgData))
                            msgData = e.Body;


                        /*执行延时方法（将订阅的方法放入队列）*/
                        methodInfo.Invoke(backgroundJob, new object[] {
                            method,
                            new object[]{
                                e.MessageId.ToString(),
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
            var msgId = await redisClient.PublishAsync(channel, msg);

            return msgId.ToString();
        } 
    }
}