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
    public class ClientMamager : IClientManage
    {
       
        private readonly IHubContext<MessageHub> hubContext;
        private readonly ICacheProvider cacheProvider;

        public ClientMamager(IHubContext<MessageHub> hubContext, ICacheProvider cacheProvider)
        {
            this.hubContext = hubContext;
            this.cacheProvider = cacheProvider;
        }

        public Task<bool> ChooseAsync(string userId, string title, string text, KeyValuePair<string, string> values, int timeout)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ConfirmAsync(string userId, string title, string content, int timeout)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendAllAsync(string msgType, string msgContent)
        {
            await hubContext.Clients.All.SendAsync(msgType, msgContent);
        }

        public async Task SendAsync(string msgType, string msgContent, params string[] userIds)
        {
            foreach (var toId in userIds)
            {
                var clientIds = await cacheProvider.HGetAsync<List<string>>(ConstValuePool.CacheUserMapKey, toId);
                if (clientIds == null)
                    continue;

                foreach (var clientId in clientIds)
                    await hubContext.Clients.Client(clientId).SendAsync(msgType, msgContent);
            }
        }
    }
}