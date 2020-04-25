using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Dto.Flow
{
    public partial class WorkFlowDto
    {
        /// <summary>
        /// 流程节点
        /// </summary>
        public IEnumerable<FlowNodeDto> Nodes { get; set; }

        /// <summary>
        /// 流程链
        /// </summary>
        public IEnumerable<FlowLineDto> Lines { get; set; } 
    }
}
