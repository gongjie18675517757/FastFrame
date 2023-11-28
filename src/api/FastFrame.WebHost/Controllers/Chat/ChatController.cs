using FastFrame.Application.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Chat
{
    /// <summary>
    /// 聊天
    /// </summary>
    public class ChatController(ChatService chatService) : BaseController
    {

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
