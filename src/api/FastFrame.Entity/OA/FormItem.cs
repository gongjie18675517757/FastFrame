using FastFrame.Infrastructure.Attrs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFrame.Entity.OA
{
    /// <summary>
    /// 表单
    /// </summary>
    [Export]
    public class Form
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
    }

    /// <summary>
    /// 表单项
    /// </summary>   
    public class FormItem
    { 
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public FormItemCategory Category { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 是否Email
        /// </summary>
        public bool IsEmail { get; set; }

        /// <summary>
        /// 是否手机号
        /// </summary>
        public bool IsPhone { get; set; }

        /// <summary>
        /// 最小值[长度]
        /// </summary>
        public long? MinValue { get; set; }

        /// <summary>
        /// 最大值[长度]
        /// </summary>
        public long? MaxValue { get; set; }

        /// <summary>
        /// 表单
        /// </summary>
        public string Form_Id { get; set; }
    }

    /// <summary>
    /// 表单项类型
    /// </summary>
    public enum FormItemCategory
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text,

        /// <summary>
        /// 多行文本
        /// </summary>
        TextArea,

        /// <summary>
        /// 选择框
        /// </summary>
        Select,

        /// <summary>
        /// 多选框
        /// </summary>
        MuchSelect,

        /// <summary>
        /// 日期
        /// </summary>
        Date,

        /// <summary>
        /// 时间
        /// </summary>
        Time,

        /// <summary>
        /// 日期+时间
        /// </summary>
        DateTime,

        /// <summary>
        /// 数字
        /// </summary>
        Number, 
    }

    /// <summary>
    /// 选择项数据源
    /// </summary>
    public class FormItemDataSource
    {
        /// <summary>
        /// 表单项
        /// </summary>
        public string FormItem_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// 表单值:文本
    /// </summary>
    [Exclude]
    public class FormItemTextValue
    {
        /// <summary>
        /// 表单实例
        /// </summary>
        public string Instance_Id { get; set; }

        /// <summary>
        /// 表单项
        /// </summary>
        public string FormItem_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(500)]
        public string Value { get; set; }
    }

    /// <summary>
    /// 表单值:数值
    /// </summary>
    [Exclude]
    public class FormItemNumberValue
    {
        /// <summary>
        /// 表单实例
        /// </summary>
        public string Instance_Id { get; set; }

        /// <summary>
        /// 表单项
        /// </summary>
        public string FormItem_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(500)]
        public decimal Value { get; set; }
    }

    /// <summary>
    /// 表单值:数值
    /// </summary>
    [Exclude]
    public class FormItemDateTimeValue
    {
        /// <summary>
        /// 表单实例
        /// </summary>
        public string Instance_Id { get; set; }

        /// <summary>
        /// 表单项
        /// </summary>
        public string FormItem_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(500)]
        public DateTime Value { get; set; }
    }
}
