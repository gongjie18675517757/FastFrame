using System.Collections.Generic;
using FastFrame.Entity.Flow;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 流程节点
    /// </summary>
    public class FlowNodeModel : FlowNode
    {
        /// <summary>
        /// 下级节点
        /// </summary>
        public IEnumerable<FlowNodeModel> Nodes { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public IEnumerable<FlowNodeCond> Conds { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        public IEnumerable<FlowNodeEvent> Events { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public IEnumerable<FlowNodeChecker> Checkers { get; set; }
    }
}
