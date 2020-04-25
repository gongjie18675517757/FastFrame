using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程节点
    /// </summary>
    [Export(ExportMark.DTO)]
    public class FlowNode : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:WorkFlow
        /// </summary>
        public string WorkFlow_Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 节点键
        /// </summary>
        [ReadOnly] 
        public int Key { get; set; }

        /// <summary>
        /// 主管审核
        /// </summary>
        public bool DeptCheck { get; set; }

        /// <summary>
        /// 触发应用通知
        /// </summary>
        public bool TriggerAppNotify { get; set; } = true;

        /// <summary>
        /// 触发微信通知
        /// </summary>
        public bool TriggerWxNotify { get; set; }

        /// <summary>
        /// 触发短信通知
        /// </summary>
        public bool TriggerSmsNotify { get; set; }
    }
}
