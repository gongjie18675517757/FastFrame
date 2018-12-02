using FastFrame.Entity.Chat;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Dto.Dtos.Chat
{
    /// <summary>
    /// 发送新消息
    /// </summary>
    public class FriendMsgInput
    {
        /// <summary>
        /// 内容
        /// </summary>      
        [StringLength(500)]
        public string Content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType Category { get; set; }

        /// <summary>
        /// 图片?附件ID
        /// </summary>
        public string Resource_Id { get; set; }

        /// <summary>
        /// 目标用户
        /// </summary>
        [Required]
        public string Target_User_Id { get; set; }
    } 
}
