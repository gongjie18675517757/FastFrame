using FastFrame.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程实例
    /// </summary> 
    public class FlowInstance : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 归属模块
        /// </summary>
        [Required]
        [StringLength(50)]
        public string BeModuleName { get; set; }

        /// <summary>
        /// 单据名称
        /// </summary> 
        [StringLength(50)]
        public string BeModuleText { get; set; }

        /// <summary>
        /// 单据ID
        /// </summary>
        [Required]
        public string Bill_Id { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary> 
        [StringLength(50)]
        public string BillNumber { get; set; }

        /// <summary>
        /// 单据摘要
        /// </summary>
        [StringLength(500)]
        public string BillDes { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        [EnumItem(EnumName.FlowStatusEnum)]
        public int Status { get; set; }

        /// <summary>
        /// 关联流程
        /// </summary>
        [Required]
        public string WorkFlow_Id { get; set; }

        public string CurrNode_Id { get; set; } 

        /// <summary>
        /// 当前节点
        /// </summary>
        public string CurrNodeName { get; set; }

        /// <summary>
        /// 流程发起人
        /// </summary>    
        public string SponsorName { get; set; }

        public string Sponsor_Id { get; set; }

        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 是否已完结
        /// </summary>
        public bool IsComlete { get; set; }

        /// <summary>
        /// 完结时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 最后审批人
        /// </summary>
        public string LastCheckerName { get; set; }

        public string LastChecker_Id { get; set; }

        /// <summary>
        /// 最后审批时间
        /// </summary>
        public DateTime? LastCheckTime { get; set; }
    }  
}
