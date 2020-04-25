using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程连接线
    /// </summary>
    [Export(ExportMark.DTO)]
    public class FlowLine : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:WorkFlow
        /// </summary>
        public string WorkFlow_Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(100)]
        public string Text { get; set; }

        /// <summary>
        /// 从
        /// </summary>
        [Required]
        public int From { get; set; }

        /// <summary>
        /// 到
        /// </summary>
        [Required]
        public int To { get; set; }
    }
}
