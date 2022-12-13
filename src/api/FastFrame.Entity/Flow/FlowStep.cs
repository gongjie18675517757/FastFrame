using FastFrame.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 审批步骤
    /// </summary> 
    public class FlowStep : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：FlowInstance
        /// </summary>
        public string FlowInstance_Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 关联表单的ID
        /// </summary>
        public string BeForm_Id { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [StringLength(200)]
        public string FlowNodeName { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        [EnumItem(EnumName.FlowActionEnum)]
        public int? Action { get; set; }

        /// <summary>
        /// 是否已办理
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string Operater_Id { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string OperaterName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? OperateTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [StringLength(500)]
        public string Desc { get; set; }
    }
}
