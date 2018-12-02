using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Chat
{
    /// <summary>
    /// 群组 
    /// </summary>
    public class Group : BaseEntity
    {
        /// <summary>
        /// 群组名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 群主
        /// </summary>
        public string LordUser_Id { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string HandIcon_Id { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [StringLength(200)]
        public string Summary { get; set; }
    }
}
