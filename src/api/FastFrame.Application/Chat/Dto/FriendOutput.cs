using System.Collections.Generic;

namespace FastFrame.Application.Chat
{
    /// <summary>
    /// 好友
    /// </summary>
    public class FriendOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadIcon_Id { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public IEnumerable<FriendMessageDto> Messages { get; set; }
    }
}
