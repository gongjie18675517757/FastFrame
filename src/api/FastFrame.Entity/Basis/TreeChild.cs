using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 树的递归下级
    /// </summary>
    public class TreeChild : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 树名称
        /// </summary>
        [StringLength(50)]
        public string TreeName { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public string Super_Id { get; set; }

        /// <summary>
        /// 下级
        /// </summary>
        public string Child_Id { get; set; }
    }
}
