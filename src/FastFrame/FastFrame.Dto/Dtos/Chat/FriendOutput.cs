using FastFrame.Dto.Chat;
using FastFrame.Entity.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Dto.Dtos.Chat
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

    public partial class FriendMessageDto
    {
        public MessageTargetDto MessageTarget { get; set; }
    }
}
