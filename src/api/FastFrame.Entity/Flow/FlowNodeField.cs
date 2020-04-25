using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 节点动态审核人
    /// </summary> 
    public class FlowNodeField : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [StringLength(100)]
        [Required]
        public string FieldName { get; set; }
    }
}
