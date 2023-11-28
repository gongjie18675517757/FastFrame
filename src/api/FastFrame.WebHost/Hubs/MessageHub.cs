using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Cache;
using FastFrame.Infrastructure.Identity;
using FastFrame.Infrastructure.MessageQueue;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Hubs
{
    public class MessageHub(ICacheProvider cacheProvider, IMessageQueue messageQueue, IIdentityManager identityManager) : Hub
    {

        /// <summary>
        /// 客户端发送给服务端的消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task ClientResponse(string msg)
        { 
            await messageQueue.PublishAsync(IMessageQueue.Client2ServiceMessage, msg); 
        }


        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var connectionId = Context.ConnectionId;
            var request = Context.GetHttpContext().Request;
            var token = request.Cookies[ConstValuePool.Token_Name];
            if (token.IsNullOrWhiteSpace())
                return;

            var user = token.FromBase64().ToObject<CurrUser>();
            if(!await identityManager.ExistsTokenAsync(user.ToKen, Context.GetHttpContext().Connection.RemoteIpAddress))
            {
                await Clients.Caller.SendAsync("client.identity.expiration", "身份认证失败[IP地址不对]！!");
                Context.Abort();
                return;
            }

            if (user == null)
            {
                await Clients.Caller.SendAsync("client.identity.expiration", "身份认证失败！!");
                Context.Abort();
                return;
            }

            Context.GetHttpContext().Items.Add(ConstValuePool.Token_Name, user);
            await UpdateUserState(user.Id, values => values.Add(connectionId));
            await Clients.Caller.SendAsync("client.onConnected", "signalR身份认证成功!"); 
        }

        private async Task UpdateUserState(string userId, Action<List<string>> action)
        {
            var values = await cacheProvider.HGetAsync<List<string>>(ConstValuePool.CacheUserMapKey, userId) ?? [];
            action(values);
            await cacheProvider.HSetAsync(ConstValuePool.CacheUserMapKey, userId, values, null);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            var connectionId = Context.ConnectionId;
            if (Context.GetHttpContext().Items.TryGetValue(ConstValuePool.Token_Name, out var value) && value is CurrUser user)
            {
                await UpdateUserState(user.Id, values => values.Remove(connectionId));
            }
        }
    }
}
