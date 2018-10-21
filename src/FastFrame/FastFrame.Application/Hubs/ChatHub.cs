using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var token = Context.GetHttpContext().Request.Cookies["token"];
            await Clients.Caller.SendAsync("ReceiveMessage", "system", "welcome to SignalR");
            await Clients.Others.SendAsync("ReceiveMessage", connectionId, "connectioned");
            await base.OnConnectedAsync();
        }
    }
}
