using FastFrame.Infrastructure.MessageBus;
using System.Threading.Tasks;

namespace FastFrame.Application.Chat
{
    /// <summary>
    /// 处理接收消息
    /// </summary>
    public class ChatMessageHandle : IAsyncMessageHandle<RecMsgOutPut>,IService
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


