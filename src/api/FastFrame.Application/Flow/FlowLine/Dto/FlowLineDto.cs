﻿using FastFrame.Entity.Flow;
using System.Collections.Generic;

namespace FastFrame.Application.Flow
{
    public partial class FlowLineDto
    {
        /// <summary>
        /// 流程线条件
        /// </summary>
        public IEnumerable<FlowLineCond> Conds { get; set; }
    }
}
