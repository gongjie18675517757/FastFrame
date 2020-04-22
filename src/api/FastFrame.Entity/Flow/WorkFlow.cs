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
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 流程节点
    /// </summary>
    [Exclude]
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

    /// <summary>
    /// 流程线
    /// </summary>
    [Exclude]
    public class FlowLink : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:WorkFlow
        /// </summary>
        public string WorkFlow_Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(100)]
        public string Text { get; set; }

        /// <summary>
        /// 从
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// 到
        /// </summary>
        public int To { get; set; }
    }

    /// <summary>
    /// 流程线条件
    /// </summary>
    [Exclude]
    public class FlowLinkCond : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:FlowLink
        /// </summary>
        public string FlowLink_Id { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 字段文本
        /// </summary> 
        [StringLength(100)]
        public string FieldText { get; set; }

        /// <summary>
        /// 比较方式
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Compare { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Value { get; set; }

        /// <summary>
        /// 值文本
        /// </summary>
        [StringLength(500)]
        public string ValueText { get; set; }
    }

    /// <summary>
    /// 节点审核角色
    /// </summary>
    [Exclude]
    public class FlowNodeRole : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 关联:Role
        /// </summary>
        public string Role_Id { get; set; }
    }

    /// <summary>
    /// 节点审核人
    /// </summary>
    [Exclude]
    public class FlowNodeUser : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 关联:User
        /// </summary>
        public string User_Id { get; set; }
    }

    /// <summary>
    /// 节点动态审核人
    /// </summary>
    [Exclude]
    public class FlowNodeField : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [StringLength(100)]
        public string FieldName { get; set; }
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
    [Exclude]
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
    [Exclude]
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
    [Exclude]
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
    [Exclude]
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
    /// 流程过程
    /// </summary>
    [Exclude]
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
