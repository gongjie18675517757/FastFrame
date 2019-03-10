using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 通知目标
    /// </summary>
    public class NotifyTarget : TargetInfo
    {
        /// <summary>
        /// 通知ID
        /// </summary>
        [Required]
        public string Notify_Id { get; set; }
    }
}
