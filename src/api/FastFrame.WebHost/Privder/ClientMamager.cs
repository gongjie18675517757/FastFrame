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

        public Task<bool> PublishConfirmAsync(string userId, string title, string content, int timeout)
        {
            throw new System.NotImplementedException();
        } 
 

        public async Task PublishNotifyAsync(ClientNotify notify, string[] userIds)
        {
            await PublishSendAsync(IClientManage.ClientNotify, notify.ToJson(), userIds);
        }  

        public async Task<string> PublishSendAsync(string msgType, string msgContent, string[] userIds)
        {
            return await messageQueue.PublishAsync(IMessageQueue.ClientMessage, new ClientMessage
            {
                MsgType = msgType,
                MsgContent = msgContent,
                ToUser = userIds
            }.ToJson());
        }

        /// <summary>
        /// 订阅通知
        /// </summary> 
        [MessageSubscribe(IMessageQueue.ClientMessage)]
        public async Task HandleSubscribe(string msg_id, ClientMessage msg)
        { 
            switch (msg.MsgType)
            {
                case IClientManage.ClientNotify:
                    await SendAsync(msg.ToUser, msg.MsgType, msg.MsgContent);
                    break;
                default:
                    break;
            }
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