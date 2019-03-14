using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Module
{
    /// <summary>
    /// 规则
    /// </summary>
    public class StrucRule : IEntity, IHasTenant, IHasSoftDelete
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string RuleName { get; }


        public string Field_Id { get; set; }

        public string Id { get; set; }

        public string Tenant_Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
