using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 资源映射
    /// </summary>
    public class ResourceMap : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：Resource
        /// </summary>
        public string File_Id { get; set; }

        /// <summary>
        /// 外键
        /// </summary>
        public string FKey_Id { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        [StringLength(20)]
        public string Key { get; set; }
    }
}
