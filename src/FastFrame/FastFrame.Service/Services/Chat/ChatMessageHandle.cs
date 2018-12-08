﻿using FastFrame.Dto.Dtos.Chat;
using FastFrame.Infrastructure.MessageBus;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Chat
{
    /// <summary>
    /// 处理接收消息
    /// </summary>
    public class ChatMessageHandle : IAsyncMessageHandle<RecMsgOutPut>
    {
        private readonly IClientManage clientManage;

        public ChatMessageHandle(IClientManage clientManage)
        {
            this.clientManage = clientManage;
        }
        public async Task Handle(Message<RecMsgOutPut> message)
        {
            await clientManage.SendAsync(message);
        }
    }
}


