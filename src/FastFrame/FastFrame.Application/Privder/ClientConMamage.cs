using CSRedis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using System;
using System.Threading.Tasks;
using static CSRedis.CSRedisClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FastFrame.Dto.Dtos.Chat;
using Microsoft.AspNetCore.SignalR;
using FastFrame.Application.Hubs;
using System.Collections.Generic;
using FastFrame.Infrastructure.MessageBus;

namespace FastFrame.Application.Privder
{ 
    /// <summary>
    /// 客户端连接管理
    /// </summary>
    public class ClientConMamage : IClientManage
    {
        private const string CacheUserMapKey = "CacheUserMapKey";
        private readonly CSRedisClient client;
        private readonly IHubContext<MessageHub> hubContext;

        public ClientConMamage(CSRedisClient client, IHubContext<MessageHub> hubContext)
        {
            this.client = client;
            this.hubContext = hubContext;
        }
        public async Task SendAsync<T>(Message<T> message) where T : class
        {
            foreach (var toId in message.Target_Ids)
            {
                var clientIds = await client.HGetAsync<List<string>>(CacheUserMapKey, toId);
                if (clientIds == null)
                    continue;

                foreach (var clientId in clientIds)
                {
                    await hubContext.Clients.Client(clientId).SendAsync("receiveMessage", message);
                }
            }
        }
    } 
}