using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Module
{
    /// <summary>
    /// 键值对
    /// </summary>
    public class EntityKeyValue: IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 来源ID
        /// </summary>
        public string Source_Id { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        [StringLength(150)]
        [Required]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(150)]
        public string Value { get; set; }
    }
}
