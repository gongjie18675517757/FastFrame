using FastFrame.Entity.Enums;
using FastFrame.Entity.Flow;
using System.Collections.Generic;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 审批操作结果
    /// </summary>
    public class FlowOperateOutput
    {
        /// <summary>
        /// 最终状态
        /// </summary>
        public FlowStatusEnum FlowStatus { get; set; }

        /// <summary>
        /// 审批过程
        /// </summary>
        public IEnumerable<FlowStep>  FlowProcesses { get; set; }
    }
}
