using FastFrame.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using static CSRedis.CSRedisClient;

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
            await Clients.Caller.SendAsync("ReceiveMessage", "system", "welcome to ChatHub");
            await Clients.Others.SendAsync("ReceiveMessage", connectionId, "connectioned");
            await base.OnConnectedAsync();
        }
    }
}
