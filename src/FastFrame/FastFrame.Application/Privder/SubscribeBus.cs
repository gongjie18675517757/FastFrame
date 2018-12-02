using FastFrame.Application.Hubs;
using FastFrame.Infrastructure.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using static CSRedis.CSRedisClient;
using FastFrame.Infrastructure;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using CSRedis;
using FastFrame.Dto.Dtos.Chat;

namespace FastFrame.Application
{
    internal class SubscribeBus
    {
        private const string CacheUserMapKey = "CacheUserMapKey";
        private readonly CSRedisClient client;
        private readonly IServiceProvider serviceProvider;

        public SubscribeBus(CSRedisClient client, IServiceProvider serviceProvider)
        {
            this.client = client;
            this.serviceProvider = serviceProvider;
        }

        private async Task SendMessage<THub, TMsgContent>(IHubContext<THub> hubContext, Message<TMsgContent> message) where THub : Hub
        {
            foreach (var toId in message.Target_Ids)
            {
                var clientIds = await client.HGetAsync<List<string>>(CacheUserMapKey, toId);
                if (clientIds == null)
                    continue;

                foreach (var clientId in clientIds)
                {
                    await hubContext.Clients.Client(clientId).SendAsync(message.Category.ToString(), message.Content);
                }
            }
        }

        /// <summary>
        /// 接收聊天消息
        /// </summary>
        /// <param name="e"></param>
        private async void ChatMsgHandle(SubscribeMessageEventArgs e)
        {
            var message = e.Body.ToObject<Message<RecMsgOutPut>>();
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var hubContext = serviceScope.ServiceProvider.GetService<IHubContext<MessageHub>>();
                await SendMessage(hubContext, message);
            }
        }

        private void recieveNotifyMsg(SubscribeMessageEventArgs e)
        {
            Console.WriteLine(e.Body);
            //var message = e.Body.ToObject<Message<RecMsgOutPut>>();
            //using (var serviceScope = serviceProvider.CreateScope())
            //{
            //    var hubContext = serviceScope.ServiceProvider.GetService<IHubContext<MessageHub>>(); 
            //}
        }

        public void Start()
        {
            Task.Run(() =>
            {
                client.Subscribe(
                    ($"receive.{MsgType.FriendMsg.ToString().ToLower()}", ChatMsgHandle),
                    ($"receive.{MsgType.Notify.ToString().ToLower()}", recieveNotifyMsg)
                    );
            });
        }
    }
}
