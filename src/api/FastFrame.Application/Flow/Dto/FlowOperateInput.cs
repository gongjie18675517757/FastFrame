﻿using FastFrame.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 审批操作
    /// </summary>
    public class FlowOperateInput
    {
        private string[] nextCheckerIds;

        /// <summary>
        /// 动作
        /// </summary>
        [Required]
        public FlowActionEnum ActionEnum { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public string Desc { get; set; }

        /// <summary>
        /// 下一步审批人
        /// </summary>
        public string[] NextCheckerIds { get => nextCheckerIds ?? Array.Empty<string>(); set => nextCheckerIds = value; }

        /// <summary>
        /// 附件内容
        /// </summary>
        public Dictionary<string, string> Items { get; set; }
    }
}
