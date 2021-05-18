using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    public class TenantHost:IEntity
    {
        /// <summary>
        /// 域名
        /// </summary>
        [StringLength(200)]
        [Required]
        public string Host { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Required]
        public string Tenant_Id { get; set;  }
    }
}
