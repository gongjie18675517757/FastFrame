using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 查询方案
    /// </summary>
    [Export]
    public class QueryProgram:IEntity
    {
        /// <summary>
        /// 方案名称
        /// </summary>
        [StringLength(50), Required]
        public string Name { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [StringLength(50), Required]
        public string ModuleName { get; set; } 

        /// <summary>
        /// 是否公共方案
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
    }
}
