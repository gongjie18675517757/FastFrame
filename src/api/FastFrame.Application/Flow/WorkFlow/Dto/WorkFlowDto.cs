using System;
using System.Collections.Generic;
using System.Text;
using FastFrame.Repository;

namespace FastFrame.Application.Flow
{
    public partial class WorkFlowDto
    {
        /// <summary>
        /// 流程节点
        /// </summary>
        public IEnumerable<FlowNodeModel> Nodes { get; set; }
    } 
}
