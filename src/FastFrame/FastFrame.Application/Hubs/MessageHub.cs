using CSRedis;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Application.Hubs
{
    public class MessageHub : Hub
    { 
        private readonly CSRedisClient redisClient;
        private const string CacheUserMapKey = "CacheUserMapKey";

        public MessageHub(CSRedisClient redisClient)
        { 
            this.redisClient = redisClient; 
        } 
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        } 
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var connectionId = Context.ConnectionId;
            var request = Context.GetHttpContext().Request;
            var token = request.Cookies["_code"];

            var user = await redisClient.GetAsync<CurrUser>(token);
            if (user == null)
                return;
            Context.GetHttpContext().Items.Add("currentUser", user);
            await Clients.Caller.SendAsync("welcom", user);
            await UpdateUserState(user.Id, values => values.Add(connectionId));
        } 
     
        private async Task UpdateUserState(string userId, Action<List<string>> action)
        {
            var values = await redisClient.HGetAsync<List<string>>(CacheUserMapKey, userId);
            if (values == null)
                values = new List<string>();
            action(values);
            await redisClient.HSetAsync(CacheUserMapKey, userId, values);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            var connectionId = Context.ConnectionId;
            if (Context.GetHttpContext().Items.TryGetValue("currentUser", out var value) && value is CurrUser user)
            {
                await UpdateUserState(user.Id, values => values.Remove(connectionId));
            }
        }
    } 
}
