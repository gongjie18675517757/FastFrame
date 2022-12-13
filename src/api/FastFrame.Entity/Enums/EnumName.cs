using FastFrame.Entity.Flow;

namespace FastFrame.Entity.Enums
{
    /// <summary>
    /// 字典键
    /// </summary>
    public enum EnumName
    {
        /// <summary>
        /// 数据字典类型
        /// </summary>
        [EnumNameForAttribute<EnumName>]
        EnumNames = 0,

        /// <summary>
        /// 启用状态
        /// </summary>
        [EnumNameForAttribute<EnabledMark>]
        EnabledMark = 10,

        /// <summary>
        /// 审批状态
        /// </summary>
        [EnumNameForAttribute<FlowStatusEnum>]
        FlowStatusEnum = 20,

        /// <summary>
        /// 性别
        /// </summary>
        [EnumNameForAttribute<GenderMark>]
        GenderMark = 30,

        /// <summary>
        /// 编码方式
        /// </summary>
        [EnumNameForAttribute<FmtDateEnum>]
        FmtDateEnum = 40,


        /// <summary>
        /// 流程审批动作
        /// </summary>
        [EnumNameForAttribute<FlowActionEnum>]
        FlowActionEnum = 50,

        /// <summary>
        /// 流程审批方式
        /// </summary>
        [EnumNameForAttribute<FlowNodeCheckEnum>]
        FlowNodeCheckEnum = 60,

        /// <summary>
        /// 流程审核人枚举
        /// </summary>
        [EnumNameForAttribute<FlowNodeCheckerEnum>]
        FlowNodeCheckerEnum = 70,

        /// <summary>
        /// 流程条件比较方式
        /// </summary>
        [EnumNameForAttribute<FlowNodeCondCompareEnum>]
        FlowNodeCondCompareEnum = 80,

        /// <summary>
        /// 流程条件比较对象
        /// </summary>
        [EnumNameForAttribute<FlowNodeCondValueEnum>]
        FlowNodeCondValueEnum = 90,

        /// <summary>
        /// 流程节点类型
        /// </summary>
        [EnumNameForAttribute<FlowNodeEnum>]
        FlowNodeEnum = 100,

        /// <summary>
        /// 审批时事件通知方式
        /// </summary>
        [EnumNameForAttribute<FlowNodeEnum>]
        FlowNodeEventNotifyEnum = 110,

        /// <summary>
        /// 审批时事件通知目标
        /// </summary>
        [EnumNameForAttribute<FlowNodeEventTargetEnum>]
        FlowNodeEventTargetEnum = 120,

        /// <summary>
        /// 审批时事件触发方式
        /// </summary>
        [EnumNameForAttribute<FlowNodeEventTriggerEnum>]
        FlowNodeEventTriggerEnum = 130,

 

        /// <summary>
        /// 通知类型
        /// </summary>
        NotifyType = 100010,

        /// <summary>
        /// 岗位
        /// </summary>
        Job = 100020,

        /// <summary>
        /// 请假原因
        /// </summary>
        LeaveCategoryEnum = 100030,
    }
}
