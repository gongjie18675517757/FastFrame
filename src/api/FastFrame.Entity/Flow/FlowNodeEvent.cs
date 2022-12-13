using FastFrame.Entity.Enums;

namespace FastFrame.Entity.Flow
{
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
        [EnumItem(EnumName.FlowNodeEventTriggerEnum)]
        public FlowNodeEventTriggerEnum EventTrigger { get; set; }

        /// <summary>
        /// 通知方式
        /// </summary>
        [EnumItem(EnumName.FlowNodeEventNotifyEnum)]
        public FlowNodeEventNotifyEnum EventNotify { get; set; }

        /// <summary>
        /// 通知目标
        /// </summary> 
        [EnumItem(EnumName.FlowNodeEventTargetEnum)]
        public FlowNodeEventTargetEnum EventTarget { get; set; }
    }
}
