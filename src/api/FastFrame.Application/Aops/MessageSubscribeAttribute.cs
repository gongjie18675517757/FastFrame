using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace FastFrame.Application
{
    /// <summary>
    /// 认阅消息
    /// 方法签名:(string msg_id,T msg),第一个参数为消息ID,订阅方自行判断重复消息的处理
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class MessageSubscribeAttribute : AbstractInterceptorAttribute
    {
        public MessageSubscribeAttribute(string channel)
        {
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentException($"“{nameof(channel)}”不能为 null 或空白。", nameof(channel));

            Channel = channel.ToLower();
        }

        /// <summary>
        /// 消息管道
        /// </summary>
        public string Channel { get; }



        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            /*此处判断避免重复执行*/
            await next(context);
        }
    }
}
