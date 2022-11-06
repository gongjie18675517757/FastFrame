using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 部门
    /// </summary>
    [Export]
    [RelatedField(nameof(Name))]
    public class Dept : BaseEntity, ITreeEntity
    { 
        /// <summary>
        /// 上级
        /// </summary>
        [RelatedTo(typeof(Dept))]
        public string Super_Id { get; set; } 

        /// <summary>
        /// 部门代码
        /// </summary>
        [StringLength(50)]
        [ReadOnly]
        public string TreeCode { get; set; } = "保存时生成";

        /// <summary>
        /// 部门名称
        /// </summary>
        [StringLength(50), Required]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string Remarks { get; set; }


        public void SetNumber(string val)
        {
            TreeCode = val;
        }

        public string GetNumber() => TreeCode;
    }
}
