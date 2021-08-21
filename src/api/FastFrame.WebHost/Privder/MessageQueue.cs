using CSRedis;
using FastFrame.Infrastructure.Interface;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    /// <summary>
    /// 消息队列
    /// </summary>
    public class MessageQueue : IMessageQueue,IApplicationInitialLifetime
    {
        private readonly CSRedisClient redisClient;

        public MessageQueue(CSRedisClient redisClient)
        {
            this.redisClient = redisClient;
        }

        public async Task InitialAsync()
        {
            await Task.Yield();

            /*收集所有订阅的*/


            redisClient.Subscribe(("xx", OnSubscribe));
        }

        private void OnSubscribe(CSRedisClient.SubscribeMessageEventArgs e)
        {
             
        }

        public async Task PublishAsync(string qname, string msg)
        { 
            await redisClient.PublishAsync(qname, msg);
        } 
    } 
}