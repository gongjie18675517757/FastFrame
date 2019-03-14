using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Module
{
    /// <summary>
    /// 字段
    /// </summary>
    public class StrucField : IEntity, IHasTenant, IHasSoftDelete
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        [Required]
        [ReadOnly(ReadOnlyMark.All)]
        public string Name { get; set; }
        
        /// <summary>
        /// 属性名称
        /// </summary>
        [StringLength(50)]
        [Required]
        [ReadOnly(ReadOnlyMark.All)]
        public string TypeName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [StringLength(150)]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 隐藏属性
        /// </summary>
        public HideMark? Hide { get;  set; } 


        /// <summary>
        /// 只读属性
        /// </summary>
        public ReadOnlyMark? Readonly { get;  set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [StringLength(50)]
        public string DefaultValue { get;  set; }

        /// <summary>
        /// 关联自
        /// </summary>
        public string Relate_Id { get;  set; }

        /// <summary>
        /// 是否长文本
        /// </summary>
        public bool IsTextArea { get;  set; }

        /// <summary>
        /// 是否富文本
        /// </summary>
        public bool IsRichText { get;  set; }  

        /// <summary>
        /// 是否需要必填
        /// </summary>
        public bool IsRequired { get;  set; }

        public string Struct_Id { get; set; }

        public string Id { get; set; }
        public string Tenant_Id { get; set; }
        public bool IsDeleted { get; set; }

    }
}
