using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 动态表单模块
    /// </summary>    
    [Export]
    public class DFModule : BaseEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [StringLength(50)]
        [ReadOnly]
        public string Name { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [ReadOnly]
        public int Version { get; set; } = 1;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }   

        /// <summary>
        /// 是否需要审核
        /// </summary>
        public bool HaveCheck { get; set; }

        /// <summary>
        /// 是否需要编号
        /// </summary>
        public bool HaveNumber { get; set; }
    }

    /// <summary>
    /// 动态表单分组
    /// </summary>
    public class DFModuleGroup : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 所属模块
        /// </summary>
        public string DFModule_Id { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string GroupName { get; set; } 
    }

    /// <summary>
    /// 动态表单字段
    /// </summary>
    public class DFModuleField : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 所属模块
        /// </summary>
        public string DFModule_Id { get; set; }

        /// <summary>
        /// 所属分组
        /// </summary>
        public string DFModuleGroup_Id { get; set; }

        /// <summary>
        /// 上级字段：为从表时
        /// </summary>
        public string Super_Id { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [StringLength(50)]
        [ReadOnly]
        public string Name { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        [Required]
        public FieldTypeEnum? FieldType { get; set; } = FieldTypeEnum._text;

        /// <summary>
        /// 是否为摘要字段
        /// </summary>
        public bool IsRelateField { get; set; }

        /// <summary>
        /// 列表展示
        /// </summary>
        public bool ShowList { get; set; }

        /// <summary>
        /// 必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 只读标记
        /// </summary>
        public ReadOnlyMark? Readonly { get; set; }

        /// <summary>
        /// 关联模块
        /// </summary>
        public string Relate { get; set; }

        /// <summary>
        /// 枚举项(";"分隔)
        /// </summary>
        [StringLength(500)]
        public string EnumValues { get; set; }

        /// <summary>
        /// 数据字典项
        /// </summary>
        public string EnumItemName { get; set; }
    }

    /// <summary>
    /// 字段验证规则
    /// </summary>
    public class DFModuleFieldRule : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 所属模块
        /// </summary>
        public string DFModule_Id { get; set; }

        /// <summary>
        /// 关联字段
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 验证规则
        /// </summary>
        public FieldRuleEnum RuleEnum { get; set; }

        /// <summary>
        /// 自定义验证规则
        /// </summary>
        public string FuncContent { get; set; }
    }

    /// <summary>
    /// 预定义的表单验证规则
    /// </summary>
    public enum FieldRuleEnum
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        email,

        /// <summary>
        /// 手机号
        /// </summary>
        phone,

        /// <summary>
        /// 唯一验证
        /// </summary>
        unique,

        /// <summary>
        /// 自定义
        /// </summary>
        func,
    }

    /// <summary>
    /// 字段类型枚举
    /// </summary>
    public enum FieldTypeEnum
    {
        /// <summary>
        /// 单行文本(长度&lt;=200)
        /// </summary>
        _text,

        /// <summary>
        /// 多行文本(200&lt;长度&lt;=2000)
        /// </summary>
        _textarea,

        /// <summary>
        /// 富文本
        /// </summary>
        _richtext,

        /// <summary>
        /// 整数
        /// </summary>
        _int32,

        /// <summary>
        /// 小数
        /// </summary>
        _decimal,

        /// <summary>
        /// 枚举(单选)
        /// </summary>
        _enum,

        /// <summary>
        /// 枚举(多选)
        /// </summary>
        _enumMultiple,

        /// <summary>
        /// 数据字典(预定义+单选)
        /// </summary>
        _enumitem,

        /// <summary>
        /// 数据字典(预定义+多选)
        /// </summary>
        _enumitemMultiple,

        /// <summary>
        /// 数据字典(自定义+单选)
        /// </summary>
        _custEnumitem,

        /// <summary>
        /// 数据字典(自定义+多选)
        /// </summary>
        _custEnumitemMultiple,

        /// <summary>
        /// 仅日期
        /// </summary>
        _date,

        /// <summary>
        /// 仅时间
        /// </summary>
        _time,

        /// <summary>
        /// 日期时间
        /// </summary>
        _datetime,

        /// <summary>
        /// 单文件
        /// </summary>
        _file,

        /// <summary>
        /// 远程选择
        /// </summary>
        _releate,

        /// <summary>
        /// 从表(表单填写)
        /// </summary>
        _childFormTable,

        /// <summary>
        /// 子表(选择关联)
        /// </summary>
        _childSelectTable,

        /// <summary>
        /// 附件列表
        /// </summary>
        _files,

        /// <summary>
        /// 图片列表
        /// </summary>
        _images,
    }

    /// <summary>
    /// 动态表单
    /// </summary>
    public class DFForm : BaseEntity, IHaveNumber
    {
        /// <summary>
        /// 关联:DFModule
        /// </summary>
        public string DFModule_Id { get; set; }

        /// <summary>
        /// 关联:DFModule
        /// </summary>
        public string DFModuleName { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        [ReadOnly]
        public string Number { get; set; } = "保存时自动生成";

        public string GetModuleName() => DFModuleName;
    }

    /// <summary>
    /// 动态表单值
    /// </summary>
    public class DFFormText : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:DFModuleField
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 关联:DFForm
        /// </summary>
        public string DFForm_Id { get; set; }

        /// <summary>
        /// 上级字段：为从表时
        /// </summary>
        public string Row_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// 动态表单值
    /// </summary>
    public class DFFormTextarea : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:DFModuleField
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 关联:DFForm
        /// </summary>
        public string DFForm_Id { get; set; }

        /// <summary>
        /// 上级字段：为从表时
        /// </summary>
        public string Row_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(2000)]
        public string Value { get; set; }
    }

    /// <summary>
    /// 动态表单值
    /// </summary>
    public class DFFormRichtext : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:DFModuleField
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 关联:DFForm
        /// </summary>
        public string DFForm_Id { get; set; }

        /// <summary>
        /// 上级字段：为从表时
        /// </summary>
        public string Row_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(4000)]
        public string Value { get; set; }
    }

    /// <summary>
    /// 动态表单值
    /// </summary>
    public class DFFormInt32 : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:DFModuleField
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 关联:DFForm
        /// </summary>
        public string DFForm_Id { get; set; }

        /// <summary>
        /// 上级字段：为从表时
        /// </summary>
        public string Row_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary> 
        public int? Value { get; set; }
    }

    /// <summary>
    /// 动态表单值
    /// </summary>
    public class DFFormDecimal : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:DFModuleField
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 关联:DFForm
        /// </summary>
        public string DFForm_Id { get; set; }

        /// <summary>
        /// 上级字段：为从表时
        /// </summary>
        public string Row_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary> 
        public decimal? Value { get; set; }
    }

    /// <summary>
    /// 动态表单值
    /// </summary>
    public class DFFormDate : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:DFModuleField
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 关联:DFForm
        /// </summary>
        public string DFForm_Id { get; set; }

        /// <summary>
        /// 上级字段：为从表时
        /// </summary>
        public string Row_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary> 
        public DateOnly? Value { get; set; }
    }

    /// <summary>
    /// 动态表单值
    /// </summary>
    public class DFFormTime : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:DFModuleField
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 关联:DFForm
        /// </summary>
        public string DFForm_Id { get; set; }

        /// <summary>
        /// 上级字段：为从表时
        /// </summary>
        public string Row_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary> 
        public TimeOnly? Value { get; set; }
    }

    /// <summary>
    /// 动态表单值
    /// </summary>
    public class DFFormDateTime : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:DFModuleField
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 关联:DFForm
        /// </summary>
        public string DFForm_Id { get; set; }

        /// <summary>
        /// 上级字段：为从表时
        /// </summary>
        public string Row_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary> 
        public DateTime? Value { get; set; }
    }

    /// <summary>
    /// 动态表单值
    /// </summary>
    public class DFFormRelate : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:DFModuleField
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 关联:DFForm
        /// </summary>
        public string DFForm_Id { get; set; }

        /// <summary>
        /// 上级字段：为从表时
        /// </summary>
        public string Row_Id { get; set; }

        /// <summary>
        /// 值
        /// </summary> 
        public string Value { get; set; }

        /// <summary>
        /// 关联键
        /// </summary>
        public string Value_Id { get; set; }
    }

    /// <summary>
    /// 动态表单值
    /// </summary>
    public class DFFormRow : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:DFModuleField
        /// </summary>
        public string DFModuleField_Id { get; set; }

        /// <summary>
        /// 关联:DFForm
        /// </summary>
        public string DFForm_Id { get; set; } 
    }
}
