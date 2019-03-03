using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    public class EnumItem : IEntity, IHasSoftDelete, IHasTenant
    {
        /// <summary>
        /// 键
        /// </summary>
        [StringLength(150)]
        public string EnumName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(200)]
        public string EnumValue { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public string Parent_Id { get; set; }

        public string Tenant_Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Id { get; set; }
    }
}
