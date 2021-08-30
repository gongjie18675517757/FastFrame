using FastFrame.Infrastructure.MessageQueue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Client
{
    /// <summary>
    /// 客户端连接管理
    /// </summary>
    public class ClientMamager : IClientManage, IMessageSubscribeHost
    {
        private readonly IEnumerable<IClientConnection> clientConnections;
        private readonly IMessageQueue messageQueue;

        public ClientMamager(IEnumerable<IClientConnection> clientConnections, IMessageQueue messageQueue)
        {
            this.clientConnections = clientConnections;
            this.messageQueue = messageQueue;
        }

        /// <summary>
        /// 发布客户端选择
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string[]> PublishChooseAsync(ClientChoose input, string userId)
        {
            TaskCompletionSourceCenter<string[]>.MakeTaskCompletionSource(input.Id);
            await PublishSendAsync(IClientManage.ClientChoose, input.ToJson(), new string[] { userId });
            return await TaskCompletionSourceCenter<string[]>.DelayTaskCompletionSource(input.Id, input.Timeout * 1000 + 500, "客户端选择超时");
        }

        /// <summary>
        /// 发布客户端确定
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> PublishConfirmAsync(ClientConfirm input, string userId)
        {
            TaskCompletionSourceCenter<bool>.MakeTaskCompletionSource(input.Id);
            await PublishSendAsync(IClientManage.ClientConfirm, input.ToJson(), new string[] { userId });
            return await TaskCompletionSourceCenter<bool>.DelayTaskCompletionSource(input.Id, input.Timeout * 1000 + 500, "客户端确认超时");
        }

        /// <summary>
        /// 发布客户端通知
        /// </summary>
        /// <param name="notify"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public async Task PublishNotifyAsync(ClientNotify notify, string[] userIds)
        {
            await PublishSendAsync(IClientManage.ClientNotify, notify.ToJson(), userIds);
        }

        /// <summary>
        /// 发布消息给客户端
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msgContent"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
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
                    TaskCompletionSourceCenter<bool>.SetTaskCompletionSource(clientConfirmResult.Id, t => t.SetResult(clientConfirmResult.Result));
                    break;
                /*客户端的选择响应*/
                case IClientManage.ClientChoose:
                    var clientChooseResult = msg.MsgContent.ToObject<ClientChooseResult>();
                    TaskCompletionSourceCenter<string[]>.SetTaskCompletionSource(clientChooseResult.Id, t => t.SetResult(clientChooseResult.Result));
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

        /// <summary>
        /// 检查是否在线
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<string> ExistsIsOnLine(string userId)
        {
            foreach (var clientConnection in clientConnections)
                if (await clientConnection.ExistsIsOnLine(userId))
                    yield return clientConnection.Name;
        }


        private async Task SendAsync(string[] userIds, string msg_type, params object[] arguments)
        {
            foreach (var clientConnection in clientConnections)
                await clientConnection.SendAsync(userIds, msg_type, arguments);
        }
    }
}
