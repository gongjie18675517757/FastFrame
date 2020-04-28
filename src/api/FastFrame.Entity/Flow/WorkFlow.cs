using FastFrame.Infrastructure.Attrs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 工作流
    /// </summary>
    [Export]
    [RelatedField(nameof(Name))]
    public class WorkFlow : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [Unique]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 适用模块
        /// </summary>
        [StringLength(100)]
        [Unique]
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
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 标识需要审核
    /// </summary>
    public interface IHaveCheck : IEntity
    {
        /// <summary>
        /// 流程状态
        /// </summary>
        FlowStatusEnum FlowStatus { get; }
    }

    /// <summary>
    /// 标记有科室
    /// </summary>
    public interface IHaveDept : IEntity
    {
        /// <summary>
        /// 关联：Dept
        /// </summary>
        string Dept_Id { get; }
    }

    /// <summary>
    /// 流程实例
    /// </summary> 
    public class FlowExample : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 归属模块
        /// </summary>
        [Required]
        [StringLength(50)]
        public string BeModuleName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary> 
        [StringLength(50)]
        public string BeModuleText { get; set; }

        /// <summary>
        /// 单据
        /// </summary>
        [Required]
        public string Bill_Id { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary> 
        [StringLength(50)]
        public string BillNumber { get; set; }

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
        /// 当前步骤
        /// </summary>
        public string CurrStep_Id { get; set; }

        /// <summary>
        /// 归属科室
        /// </summary>
        [Required]
        public string BeDept_Id { get; set; }

        /// <summary>
        /// 流程发起人
        /// </summary>
        [Required]
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
        /// 最后操作人
        /// </summary>
        public string LastOperater_Id { get; set; }

        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime? LastOperateTime { get; set; }
    }

    /// <summary>
    /// 流程快照
    /// </summary> 
    public class FlowSnapshot : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：FlowExample
        /// </summary>
        public string FlowExample_Id { get; set; }

        /// <summary>
        /// 快照内容
        /// </summary>
        public string SnapshotContent { get; set; }
    }

    /// <summary>
    /// 流程步骤
    /// </summary> 
    public class FlowStep : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：FlowExample
        /// </summary>
        public string FlowExample_Id { get; set; }

        /// <summary>
        /// 关联：FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 步骤序号
        /// </summary>
        public int StepOrder { get; set; }

        /// <summary>
        /// 关联：FlowStep，上一步
        /// </summary>
        public string PrevStep_Id { get; set; }
    }

    /// <summary>
    /// 流程步骤审核人
    /// </summary> 
    public class FlowStepUser : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：FlowStep
        /// </summary>
        public string FlowStep_Id { get; set; }

        /// <summary>
        /// 关联：User
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// 是否科室审核人
        /// </summary>
        public string IsBeDept { get; set; }

        /// <summary>
        /// 是否角色审核人
        /// </summary>
        public string IsBeRole { get; set; }
    }

    /// <summary>
    /// 审批过程
    /// </summary> 
    public class FlowProcess : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：FlowStep
        /// </summary>
        public string FlowStep_Id { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        public FlowActionEnum Action { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operater_Id { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(500)]
        public string Direction { get; set; }
    }

    /// <summary>
    /// 流程状态
    /// </summary>
    public enum FlowStatusEnum
    {
        /// <summary>
        /// 未提交
        /// </summary>
        unsubmitted,

        /// <summary>
        /// 已提交
        /// </summary>
        submitted,

        /// <summary>
        /// 进行中
        /// </summary>
        processing,

        /// <summary>
        /// 已完结
        /// </summary>
        finished,

        /// <summary>
        /// 已退回
        /// </summary>
        returned,

        /// <summary>
        /// 已拒绝
        /// </summary>
        refuse
    }

    /// <summary>
    /// 流程动作
    /// </summary>
    public enum FlowActionEnum
    {
        /// <summary>
        /// 提交
        /// </summary>
        submit,

        /// <summary>
        /// 撤回
        /// </summary>
        unsubmit,

        /// <summary>
        /// 通过
        /// </summary>
        pass,

        /// <summary>
        /// 退回
        /// </summary>
        withdraw,

        /// <summary>
        /// 拒绝
        /// </summary>
        refuse,

        /// <summary>
        /// 反审核
        /// </summary>
        uncheck
    }
}
