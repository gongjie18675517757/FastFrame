using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程节点
    /// </summary> 
    public class FlowNode : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:WorkFlow
        /// </summary>
        public string WorkFlow_Id { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderVal { get; set; }

        /// <summary>
        /// 标题
        /// </summary> 
        [StringLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public FlowNodeEnum NodeEnum { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public string Super_Id { get; set; }

        /// <summary>
        /// 条件权重(为分支时)
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// 缺省分支(为分支时)
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 审批方式(多人时)
        /// </summary>
        public FlowNodeCheckEnum? CheckEnum { get; set; }

        /// <summary>
        /// 通过比例(多人审批且审批方式为投票时)
        /// </summary>
        public decimal? VoteScale { get; set; }
    }
}
