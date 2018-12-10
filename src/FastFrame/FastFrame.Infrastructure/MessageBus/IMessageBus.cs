using System;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.MessageBus
{
    /// <summary>
    /// 消息总线
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        ///  发布消息
        /// </summary>      
        Task PubLishAsync<T>(Message<T> message) where T : class, new();

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void SubscribeAsync<T>() where T : class, new();

        /// <summary>
        ///  订阅消息
        /// </summary>
        /// <param name="msgType"></param>
        void SubscribeAsync(Type msgType);
    }
}


