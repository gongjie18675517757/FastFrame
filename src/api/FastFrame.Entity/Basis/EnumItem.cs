﻿using FastFrame.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 数字字典
    /// </summary> 
    [Export]
    [Unique(nameof(KeyEnum), nameof(IntKey))]
    [Unique(nameof(KeyEnum), nameof(Super_Id), nameof(TextValue))]
    public class EnumItem : BaseEntity, ITreeEntity, IViewModelable<EnumItem>
    {
        /// <summary>
        /// 是否系统枚举
        /// </summary>
        [Hide]
        [ReadOnly]
        public bool IsSystemEnum { get; set; }

        /// <summary>
        /// 字典类别
        /// </summary>
        [Required]
        [EnumItem(EnumName.EnumNames)]
        public int? KeyEnum { get; set; }

        /// <summary>
        /// 上级
        /// </summary>  
        [RelatedTo(typeof(EnumItem))]
        public string Super_Id { get; set; } 

        /// <summary>
        /// 字典数字值
        /// </summary>
        [Required]
        public int? IntKey { get; set; }

        /// <summary>
        /// 字典文本值
        /// </summary>
        [StringLength(150)]
        [Required]
        public string TextValue { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortVal { get; set; }

        private static readonly Expression<Func<EnumItem, IViewModel>> vm_expression =
                        v => new DefaultViewModel
                        {
                            Id = v.Id,
                            Value = v.TextValue
                        };

        public static Expression<Func<EnumItem, IViewModel>> BuildExpression() => vm_expression;

        public Expression<Func<EnumItem, IViewModel>> GetBuildExpression() => vm_expression;

        /// <summary>
        /// 树状码
        /// </summary>
        [Exclude]
        public string TreeCode { get; set; }
    }
}
