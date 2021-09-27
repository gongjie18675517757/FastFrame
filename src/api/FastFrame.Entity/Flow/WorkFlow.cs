using FastFrame.Entity.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 工作流    
    /// </summary>
    [Export]
    [RelatedField(nameof(BeModule), nameof(Version))]
    public class WorkFlow : BaseEntity
    {
        /// <summary>
        /// 适用模块
        /// </summary>
        [StringLength(100)]
        [ReadOnly(ReadOnlyMark.Edit)]
        public string BeModule { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [ReadOnly]
        [StringLength(150)]
        public string BeModuleName { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [ReadOnly]
        public int Version { get; set; } = 1;

        /// <summary>
        /// 状态
        /// </summary>
        [ReadOnly]
        public EnabledMark Enabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 流程实例
    /// </summary> 
    public class FlowInstance : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 归属模块
        /// </summary>
        [Required]
        [StringLength(50)]
        public string BeModuleName { get; set; }

        /// <summary>
        /// 单据名称
        /// </summary> 
        [StringLength(50)]
        public string BeModuleText { get; set; }

        /// <summary>
        /// 单据ID
        /// </summary>
        [Required]
        public string Bill_Id { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary> 
        [StringLength(50)]
        public string BillNumber { get; set; }

        /// <summary>
        /// 单据摘要
        /// </summary>
        [StringLength(500)]
        public string BillDes { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        public FlowStatusEnum Status { get; set; }

        /// <summary>
        /// 关联流程
        /// </summary>
        [Required]
        public string WorkFlow_Id { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        public string CurrNode_Id { get; set; }

        /// <summary>
        /// 流程发起人
        /// </summary>    
        public string Sponsor_Id { get; set; }

        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 是否已完结
        /// </summary>
        public bool IsComlete { get; set; }

        /// <summary>
        /// 完结时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 最后审批人
        /// </summary>
        public string LastChecker_Id { get; set; }

        /// <summary>
        /// 最后审批时间
        /// </summary>
        public DateTime? LastCheckTime { get; set; }
    }

    /// <summary>
    /// 流程实例归属科室
    /// </summary>
    public class FlowBeDept : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：FlowInstance
        /// </summary>
        public string FlowInstance_Id { get; set; }

        /// <summary>
        /// 归属部门
        /// </summary> 
        public string BeDept_Id { get; set; }
    }

    /// <summary>
    /// 流程快照(将已提交的流程审核顺序存起来)
    /// </summary> 
    public class FlowSnapshot : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：FlowInstance
        /// </summary>
        public string FlowInstance_Id { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderVal { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string FlowNode_Id { get; set; }
    }

    /// <summary>
    /// 流程步骤审核人
    /// </summary> 
    public class FlowInstanceUser : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：FlowInstance
        /// </summary>
        public string FlowInstance_Id { get; set; }

        /// <summary>
        /// 关联：User
        /// </summary>
        public string User_Id { get; set; }
    }

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
        /// 步骤名称
        /// </summary>
        [StringLength(200)]
        public string FlowNodeName { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        public FlowActionEnum? Action { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operater_Id { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public string Desc { get; set; }
    }

    /// <summary>
    /// 指定的下一步审核人
    /// </summary>
    public class FlowNextChecker : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:FlowStep
        /// </summary>
        public string FlowStep_Id { get; set; }

        /// <summary>
        /// 关联:User
        /// </summary>
        public string Checker_Id { get; set; }
    }
}
