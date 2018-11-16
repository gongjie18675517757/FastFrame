using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    [Exclude]
    public class OrganizeHost : BaseEntity
    {
        /// <summary>
        /// 域名
        /// </summary>
        [StringLength(200)]
        [Required]
        public string Host { get; set; }
    }
}
