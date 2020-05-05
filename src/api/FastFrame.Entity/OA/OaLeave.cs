﻿using FastFrame.Entity.Basis;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure.Attrs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFrame.Entity.OA
{
    /// <summary>
    /// 请假单
    /// </summary>
    [Export]
    public class OaLeave : BaseEntity, IHaveDept, IHaveCheck, IHaveNumber,IHaveMultiFile
    {
        /// <summary>
        /// 请假单号
        /// </summary>
        [StringLength(20)]
        [ReadOnly]
        public string Number { get; set; } = "自动生成";

        /// <summary>
        /// 申请时间
        /// </summary>
        [ReadOnly]
        public override DateTime CreateTime { get => base.CreateTime; set => base.CreateTime = value; }

        /// <summary>
        /// 申请人
        /// </summary>
        [RelatedTo(typeof(User))]
        [Required]
        [ReadOnly]
        public override string Create_User_Id { get => base.Create_User_Id; set => base.Create_User_Id = value; }

        /// <summary>
        /// 岗位
        /// </summary>
        [Required]
        [EnumItem(nameof(EnumName.Job))]
        public string Job_Id { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [Required]
        [RelatedTo(typeof(Dept))]
        public string Dept_Id { get; set; }

        /// <summary>
        /// 请假类型
        /// </summary>
        [Required]
        public LeaveCategoryEnum? LeaveCategory { get; set; }

        /// <summary>
        /// 工作代理人
        /// </summary>
        [Required]
        [RelatedTo(typeof(User))]
        public string Agent_Id { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        public decimal? Days { get; set; }

        /// <summary>
        /// 申请事由
        /// </summary>
        [StringLength(500)]
        public string Reasons { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        [ReadOnly]
        public FlowStatusEnum FlowStatus { get; set; }

        public string[] GetBeDeptIds()
        {
            return new string[] { Dept_Id };
        }
    }
}
