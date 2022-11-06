using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 多租户信息
    /// </summary>
    [Export]
    [RelatedField(nameof(FullName))]
    public class Tenant : IEntity, IHasSoftDelete, ITreeEntity
    {
        /// <summary>
        /// 全称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string ShortName { get; set; }

        /// <summary>
        /// URL标识
        /// </summary>
        [Unique]
        [StringLength(50)]
        public string UrlMark { get; set; } = "";

        /// <summary>
        /// 上级
        /// </summary>
        [ReadOnly]
        public string Super_Id { get; set; }

        /// <summary>
        /// 租户标记
        /// </summary>
        [Column("tenant_id")]
        [ReadOnly]
        public string Tenant_Id { get; set; }

        /// <summary>
        /// Logo头像
        /// </summary>
        public string HandIcon_Id { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 树状码
        /// </summary>
        [ReadOnly]
        [StringLength(200)]
        public string TreeCode { get; set; } 


        public void SetNumber(string val)
        {
            TreeCode = val;
        }

        public string GetNumber() => TreeCode;
    }
}
