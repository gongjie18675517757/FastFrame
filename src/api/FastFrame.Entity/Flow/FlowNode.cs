using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程节点
    /// </summary> 
    public class FlowNode : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:WorkFlow
        /// </summary>
        public string WorkFlow_Id { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderVal { get; set; }

        /// <summary>
        /// 标题
        /// </summary> 
        [StringLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public FlowNodeEnum NodeEnum { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public string Super_Id { get; set; }

        /// <summary>
        /// 条件权重(为分支时)
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// 缺省分支(为分支时)
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 审批方式(多人时)
        /// </summary>
        public FlowNodeCheckEnum? CheckEnum { get; set; }
    }

    /// <summary>
    /// 审批方式
    /// </summary>
    public enum FlowNodeCheckEnum
    {
        /// <summary>
        /// 或签
        /// </summary>
        or,

        /// <summary>
        /// 会签
        /// </summary>
        and
    }

    /// <summary>
    /// 流程节点条件
    /// </summary>
    public class FlowNodeCond : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:WorkFlow
        /// </summary>
        public string WorkFlow_Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 条件组号
        /// </summary>
        public int GroupIndex { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public FlowNodeCondCompareEnum CompareEnum { get; set; }

        /// <summary>
        /// 值类型
        /// </summary>
        public FlowNodeCondValueEnum ValueEnum { get; set; }

        /// <summary>
        /// 值的ID
        /// </summary>
        public string Value_Id { get; set; }

        /// <summary>
        /// 值的文本
        /// </summary>
        public string ValueText { get; set; }
    }

    public enum FlowNodeCondValueEnum
    {
        /// <summary>
        /// 指定内容
        /// </summary>
        input_value,

        /// <summary>
        /// 表单字段
        /// </summary>
        form_field
    }

    public enum FlowNodeCondCompareEnum
    {
        /// <summary>
        /// 大于等于
        /// </summary>
        gte,

        /// <summary>
        /// 小于等于
        /// </summary>
        lte,

        /// <summary>
        /// 等于
        /// </summary>
        eq,

        /// <summary>
        /// 不等于
        /// </summary>
        not_eq,

        /// <summary>
        /// 包含
        /// </summary>
        like,

        /// <summary>
        /// 不包含
        /// </summary>
        not_like
    }

    /// <summary>
    /// 节点类型
    /// </summary>
    public enum FlowNodeEnum
    {
        /// <summary>
        /// 开始
        /// </summary>
        start,

        /// <summary>
        /// 分支
        /// </summary>
        branch,

        /// <summary>
        /// 子分支
        /// </summary>
        branch_child,

        /// <summary>
        /// 审核 
        /// </summary>
        check,

        /// <summary>
        /// 抄送
        /// </summary>
        cc,

        /// <summary>
        /// 条件
        /// </summary>
        cond,

        /// <summary>
        /// 结束
        /// </summary>
        end
    }

    /// <summary>
    /// 事件类型
    /// </summary>
    public enum FlowNodeEventTriggerEnum
    {
        /// <summary>
        /// 进入时
        /// </summary>
        enter,

        /// <summary>
        /// 离开时
        /// </summary>
        leave
    }

    /// <summary>
    /// 事件通知
    /// </summary>
    public enum FlowNodeEventNotifyEnum
    {
        /// <summary>
        /// 微信通知
        /// </summary>
        wx_notify,

        /// <summary>
        /// 应用内通知
        /// </summary>
        app_notify,

        /// <summary>
        /// 短信通知
        /// </summary>
        sms_notify,

        /// <summary>
        /// 邮箱通知
        /// </summary>
        email_notify,
    }

    /// <summary>
    /// 事件通知目标
    /// </summary>
    public enum FlowNodeEventTargetEnum
    {
        /// <summary>
        /// 待审核人
        /// </summary>
        checker,

        /// <summary>
        /// 申请人
        /// </summary>
        applyer
    }

    /// <summary>
    /// 节点事件
    /// </summary>
    public class FlowNodeEvent : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:WorkFlow
        /// </summary>
        public string WorkFlow_Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 触发方式
        /// </summary>
        public FlowNodeEventTriggerEnum EventTrigger { get; set; }

        /// <summary>
        /// 通知方式
        /// </summary>
        public FlowNodeEventNotifyEnum EventNotify { get; set; }

        /// <summary>
        /// 通知目标
        /// </summary>
        public FlowNodeEventTargetEnum EventTarget { get; set; }
    }

    /// <summary>
    /// 流程节点审核人/抄送人
    /// </summary>
    public class FlowNodeChecker : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:WorkFlow
        /// </summary>
        public string WorkFlow_Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 审核人类型
        /// </summary>
        public FlowNodeCheckerEnum CheckerEnum { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Checker_Id { get; set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        public string CheckerName { get; set; }
    }

    /// <summary>
    /// 审核人枚举
    /// </summary>
    public enum FlowNodeCheckerEnum
    {
        /// <summary>
        /// 指定人
        /// </summary>
        user,

        /// <summary>
        /// 指定角色
        /// </summary>
        role,

        /// <summary>
        /// 指定字段
        /// </summary>
        field,

        /// <summary>
        /// 指定部门人员
        /// </summary>
        dept,

        /// <summary>
        /// 上级部门人员
        /// </summary>
        parent_dept,

        /// <summary>
        /// 上级部门主管
        /// </summary>
        parent_dept_manage,

        /// <summary>
        /// 当前部门主管
        /// </summary>
        dept_manage,

        /// <summary>
        /// 上个节点指定
        /// </summary>
        prev_appoint
    }
}
