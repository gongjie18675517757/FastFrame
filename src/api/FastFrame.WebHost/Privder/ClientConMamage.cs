﻿using CSRedis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.MessageBus;
using FastFrame.WebHost.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
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

        public async Task SendAsync<T>(Message<T> message)
        {
            if (message.Target_Ids.Length > 0)
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

            else
            {
                await hubContext.Clients.All.SendAsync("receiveMessage", message.ToJson());
            }
        }
    }
}