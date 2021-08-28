using FastFrame.Application;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.WebHost.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    /// <summary>
    /// 客户端连接管理
    /// </summary>
    public class ClientMamager : IClientManage, IMessageSubscribeHost
    {
        private readonly IHubContext<MessageHub> hubContext;
        private readonly ICacheProvider cacheProvider;
        private readonly IMessageQueue messageQueue;

        private static readonly IDictionary<string, TaskCompletionSource<bool>> TaskCompletionSourceDic
            = new Dictionary<string, TaskCompletionSource<bool>>(100);

        public ClientMamager(IHubContext<MessageHub> hubContext, ICacheProvider cacheProvider, IMessageQueue messageQueue)
        {
            this.hubContext = hubContext;
            this.cacheProvider = cacheProvider;
            this.messageQueue = messageQueue;
        }

        public Task<bool> PublishChooseAsync(string userId, string title, string text, KeyValuePair<string, string> values, int timeout)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> PublishConfirmAsync(ClientConfirm clientConfirm, string userId)
        { 
            var taskCompletionSource = new TaskCompletionSource<bool>();
            TaskCompletionSourceDic.Add(clientConfirm.Id, taskCompletionSource);

            await PublishSendAsync(IClientManage.ClientConfirm, clientConfirm.ToJson(), new string[] { userId });

            var task = await Task.WhenAny(taskCompletionSource.Task, ExistsConfirmTimeOut(clientConfirm.Timeout));

            lock (TaskCompletionSourceDic)
            {
                if (TaskCompletionSourceDic.TryGetValue(clientConfirm.Id, out _))
                    TaskCompletionSourceDic.Remove(clientConfirm.Id);
            }


            if (task == taskCompletionSource.Task)
                return await taskCompletionSource.Task;
            else
                throw new MsgException("客户端确认超时");
        }

        /// <summary>
        /// 检查客户端确认是否超时
        /// </summary> 
        /// <param name="timeout"></param>
        /// <returns></returns>
        private async Task<bool> ExistsConfirmTimeOut(int timeout)
        {
            await Task.Delay(timeout * 1000 + 500);
            return false;
        }


        public async Task PublishNotifyAsync(ClientNotify notify, string[] userIds)
        {
            await PublishSendAsync(IClientManage.ClientNotify, notify.ToJson(), userIds);
        }

        public async Task<string> PublishSendAsync(string msgType, string msgContent, string[] userIds)
        {
            return await messageQueue.PublishAsync(IMessageQueue.Service2ClientMessage, new Service2ClientMessage
            {
                MsgType = msgType,
                MsgContent = msgContent,
                ToUser = userIds
            }.ToJson());
        }

        /// <summary>
        /// 订阅通知(客户端发送给服务端的消息)
        /// </summary> 
        [MessageSubscribe(IMessageQueue.Client2ServiceMessage)]
        public async Task HandleSubscribeServerMessage(string msg_id, Client2ServiceMessage msg)
        {
            await Task.CompletedTask;

            switch (msg.MsgType)
            {
                /*客户端的确认响应*/
                case IClientManage.ClientConfirm:
                    var clientConfirmResult = msg.MsgContent.ToObject<ClientConfirmResult>();
                    lock (TaskCompletionSourceDic)
                    {
                        if (TaskCompletionSourceDic.TryGetValue(clientConfirmResult.Id, out var taskCompletionSource))
                        {
                            taskCompletionSource.SetResult(clientConfirmResult.Result);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 订阅通知(服务端发送给客户端的消息)
        /// </summary> 
        [MessageSubscribe(IMessageQueue.Service2ClientMessage)]
        public async Task HandleSubscribeClientMessage(string msg_id, Service2ClientMessage msg)
        {
            await SendAsync(msg.ToUser, msg.MsgType, msg.MsgContent);
        }


        private async Task SendAsync(string[] userIds, string msg_type, params object[] arguments)
        {
            if (userIds == null)
                await hubContext.Clients.All.SendAsync(msg_type, arguments);

            foreach (var toId in userIds)
            {
                var clientIds = await cacheProvider.HGetAsync<List<string>>(ConstValuePool.CacheUserMapKey, toId);
                if (clientIds == null)
                    continue;

                foreach (var clientId in clientIds)
                    await hubContext.Clients.Client(clientId).SendAsync(msg_type, arguments);
            }
        }
    }
}