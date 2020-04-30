using FastFrame.Entity.Chat;

namespace FastFrame.Application.Chat
{
    /// <summary>
    /// 接收消息
    /// </summary>    
    public class RecMsgOutPut
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 内容
        /// </summary>            
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
        /// 发送用户
        /// </summary> 
        public string From_User_Id { get; set; }

        /// <summary>
        /// 目标用户
        /// </summary>      
        public string Target_User_Id { get; set; }
    }
}
