﻿using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 图片库
    /// </summary>  
    [Unique(nameof(Super_Id), nameof(Name))]
    [Export(ExportMark.Service, ExportMark.DTO)]
    public class Meidia : BaseEntity, ITreeEntity, IViewModelable<Meidia>
    {
        /// <summary>
        /// 上级
        /// </summary>
        [RelatedTo(typeof(Meidia))]
        public string Super_Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        [Required]
        [IsPrimaryField]
        public string Name { get; set; }

        /// <summary>
        /// 资源
        /// </summary> 
        public string Resource_Id { get; set; }

        /// <summary>
        /// 是否文件夹
        /// </summary>
        public bool IsFolder { get; set; }

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


        private static Expression<Func<Meidia, IViewModel>> vm_expression = v => new DefaultViewModel { Id = v.Id, Value = v.Name + "(" + v.TreeCode + ")" };

        public static Expression<Func<Meidia, IViewModel>> BuildExpression()
        {
            return vm_expression;
        }
    }
}
