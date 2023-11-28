using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Cache;
using FastFrame.Infrastructure.Client;
using FastFrame.WebHost.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    /// <summary>
    /// 客户端连接抽象实现
    /// </summary>
    public class ClientConnection(IHubContext<MessageHub> hubContext,
                                  ICacheProvider cacheProvider) : IClientConnection
    {
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
            return clientIds.Count != 0;
        }
    }
}