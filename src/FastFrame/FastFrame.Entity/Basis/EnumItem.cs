using FastFrame.Entity.Enums;
using FastFrame.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{    
    public class EnumItem : BaseEntity, IHasSoftDelete, IHasTenant
    {
        /// <summary>
        /// 键
        /// </summary>
        [StringLength(150)]
        public EnumName EnumName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(200)]
        public string EnumValue { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public string Parent_Id { get; set; }  
    }
}
