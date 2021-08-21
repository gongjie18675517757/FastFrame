﻿using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Hubs
{
    public class MessageHub : Hub
    {
        private readonly ICacheProvider cacheProvider;

        public MessageHub(ICacheProvider cacheProvider)
        {
            this.cacheProvider = cacheProvider;
        }

        public async Task SendMessage(string msgType, string user, string message)
        {
            await Clients.All.SendAsync(msgType, user, message);
        }


        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var connectionId = Context.ConnectionId;
            var request = Context.GetHttpContext().Request;
            var token = request.Cookies[ConstValuePool.Token_Name];

            var user = await cacheProvider.GetAsync<CurrUser>(token);
            if (user == null)
                return;

            Context.GetHttpContext().Items.Add(ConstValuePool.Token_Name, user);
            await Clients.Caller.SendAsync("inited", user);
            await UpdateUserState(user.Id, values => values.Add(connectionId));
        }

        private async Task UpdateUserState(string userId, Action<List<string>> action)
        {
            var values = await cacheProvider.HGetAsync<List<string>>(ConstValuePool.CacheUserMapKey, userId);
            if (values == null)
                values = new List<string>();
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
