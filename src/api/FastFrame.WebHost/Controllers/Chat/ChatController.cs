using FastFrame.Application.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Chat
{
    /// <summary>
    /// 聊天
    /// </summary>
    public class ChatController:BaseController
    {
        private readonly ChatService chatService;

        public ChatController(ChatService chatService)
        {
            this.chatService = chatService;
        }

        /// <summary>
        /// 发送消息给好友
        /// </summary> 
        [HttpPost]
        public async Task<RecMsgOutPut> SendFriendMsg([FromBody]FriendMsgInput input)
        {
            return await chatService.SendFriendMsg(input);
        }
    }
}
