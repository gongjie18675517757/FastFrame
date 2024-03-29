﻿using FastFrame.Entity;
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
        public FlowOperateOutput(string key, bool isSuccess)
        {
            Key = key;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// 单据主键
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; }
    }

    /// <summary>
    /// 审批成功
    /// </summary>
    public class FlowOperateSuccessOutput : FlowOperateOutput
    {
        public FlowOperateSuccessOutput(string key, int flowStatus, string billNo, IEnumerable<FlowStep> flowProcesses) : base(key, true)
        {
            FlowStatus = flowStatus;
            BillNumber = billNo;
            FlowProcesses = flowProcesses;
        }

        /// <summary>
        /// 最终状态
        /// </summary>
        [EnumItem(EnumName.FlowStatusEnum)]
        public int FlowStatus { get; set; }

        /// <summary>
        /// 单据号码
        /// </summary>
        public string BillNumber { get; }

        /// <summary>
        /// 最终的审批过程
        /// </summary>
        public IEnumerable<FlowStep> FlowProcesses { get; set; }
    }

    /// <summary>
    /// 审批失败
    /// </summary>
    public class FlowOperateFailOutput : FlowOperateOutput
    {
        public FlowOperateFailOutput(string key, string errMessage) : base(key, false)
        {
            ErrMessage = errMessage; 
        }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string ErrMessage { get; set; }
    }
}
