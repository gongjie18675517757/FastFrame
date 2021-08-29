using FastFrame.Application;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.WebHost.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    /// <summary>
    /// 客户端连接管理
    /// </summary>
    public class ClientMamager : IClientManage, IMessageSubscribeHost
    {
        private readonly IEnumerable<IClientConnection> clientConnections;
        private readonly IMessageQueue messageQueue;

        private static readonly IDictionary<string, TaskCompletionSource<bool>> ConfirmTaskDic
            = new Dictionary<string, TaskCompletionSource<bool>>(100);

        private static readonly IDictionary<string, TaskCompletionSource<string[]>> ChooseTaskDic
            = new Dictionary<string, TaskCompletionSource<string[]>>(100);

        public ClientMamager(IEnumerable<IClientConnection> clientConnections, IMessageQueue messageQueue)
        {
            this.clientConnections = clientConnections;
            this.messageQueue = messageQueue;
        }

        /// <summary>
        /// 发布客户端选择
        /// </summary>
        /// <param name="clientChoose"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string[]> PublishChooseAsync(ClientChoose clientChoose, string userId)
        {
            var taskCompletionSource = new TaskCompletionSource<string[]>();
            ChooseTaskDic.Add(clientChoose.Id, taskCompletionSource);

            await PublishSendAsync(IClientManage.ClientChoose, clientChoose.ToJson(), new string[] { userId });

            var task = await Task.WhenAny(taskCompletionSource.Task, Task.Delay(clientChoose.Timeout * 1000 + 500));

            lock (ChooseTaskDic)
            {
                if (ChooseTaskDic.TryGetValue(clientChoose.Id, out _))
                    ChooseTaskDic.Remove(clientChoose.Id);
            }


            if (task == taskCompletionSource.Task)
                return await taskCompletionSource.Task;
            else
                throw new MsgException("客户端确认超时");
        }

        /// <summary>
        /// 发布客户端确定
        /// </summary>
        /// <param name="clientConfirm"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> PublishConfirmAsync(ClientConfirm clientConfirm, string userId)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            ConfirmTaskDic.Add(clientConfirm.Id, taskCompletionSource);

            await PublishSendAsync(IClientManage.ClientConfirm, clientConfirm.ToJson(), new string[] { userId });

            var task = await Task.WhenAny(taskCompletionSource.Task, Task.Delay(clientConfirm.Timeout * 1000 + 500));

            lock (ConfirmTaskDic)
            {
                if (ConfirmTaskDic.TryGetValue(clientConfirm.Id, out _))
                    ConfirmTaskDic.Remove(clientConfirm.Id);
            }


            if (task == taskCompletionSource.Task)
                return await taskCompletionSource.Task;
            else
                throw new MsgException("客户端确认超时");
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
                    lock (ConfirmTaskDic)
                    {
                        if (ConfirmTaskDic.TryGetValue(clientConfirmResult.Id, out var taskCompletionSource))
                        {
                            taskCompletionSource.SetResult(clientConfirmResult.Result);
                        }
                    }
                    break;
                /*客户端的选择响应*/
                case IClientManage.ClientChoose:
                    var clientChooseResult = msg.MsgContent.ToObject<ClientChooseResult>();
                    lock (ChooseTaskDic)
                    {
                        if (ChooseTaskDic.TryGetValue(clientChooseResult.Id, out var taskCompletionSource))
                        {
                            taskCompletionSource.SetResult(clientChooseResult.Result);
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

    /// <summary>
    /// 异步任务帮忙类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public static class TaskCompletionSourceCenter<TResult>
    {
        private static readonly IDictionary<string, TaskCompletionSource<TResult>> dic
           = new Dictionary<string, TaskCompletionSource<TResult>>(100); 

        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>
        public static void MakeTaskCompletionSource(string id)
        {
            var taskCompletionSource = new TaskCompletionSource<TResult>();
            lock (dic)
            {
                dic.Add(id, taskCompletionSource);
            }
        }

        /// <summary>
        /// 等待任务完成
        /// </summary>
        /// <param name="id"></param>
        /// <param name="millisecondsDelay"></param>
        /// <returns></returns>
        public static async Task<TResult> DelayTaskCompletionSource(string id, int? millisecondsDelay)
        {
            Task<TResult> task = null;
            lock (dic)
            {
                if (dic.TryGetValue(id, out var taskCompletionSource))
                    task = taskCompletionSource.Task;
            }

            if (task == null)
                throw new MsgException("任务丢失");

            /*判断超时*/
            var any_task = millisecondsDelay == null ?
                            task :
                            await Task.WhenAny(task, Task.Delay(millisecondsDelay.Value));

            lock (dic)
            {
                if (dic.TryGetValue(id, out _))
                    dic.Remove(id);
            }

            if (any_task == task)
                return await task;
            else
                throw new MsgException("客户端确认超时");
        }

        /// <summary>
        /// 设置任务完成
        /// </summary>
        /// <param name="id"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static void SetTaskCompletionSource(string id, Action<TaskCompletionSource<TResult>> action)
        {
            lock (dic)
            {
                if (dic.TryGetValue(id, out var task))
                {
                    action(task);
                    dic.Remove(id);
                }
            }
        }
    }

    /// <summary>
    /// 客户端连接抽象
    /// </summary>
    public class ClientConnection : IClientConnection
    {
        private readonly IHubContext<MessageHub> hubContext;
        private readonly ICacheProvider cacheProvider;

        public ClientConnection(IHubContext<MessageHub> hubContext,
                                ICacheProvider cacheProvider)
        {
            this.hubContext = hubContext;
            this.cacheProvider = cacheProvider;
        }

        public string Name => "后台网页";

        /// <summary>
        /// 发送消息(直接发送)
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="msg_type"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public async Task SendAsync(string[] userIds, string msg_type, params object[] arguments)
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

        /// <summary>
        /// 检查是否在线
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> ExistsIsOnLine(string userId)
        {
            var clientIds = await cacheProvider.HGetAsync<List<string>>(ConstValuePool.CacheUserMapKey, userId);
            return clientIds.Any();
        }
    }
}