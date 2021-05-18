using System.Collections.Generic;

namespace FastFrame.Infrastructure.Module
{
    /// <summary>
    /// 模块字段结构
    /// </summary>
    public class ModuleFieldStrut
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 字段说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 隐藏标记
        /// </summary>
        public string Hide { get; set; }

        /// <summary>
        /// 只读标记
        /// </summary>
        public string Readonly { get; set; }

        /// <summary>
        /// 验证规则
        /// </summary>
        public IEnumerable<ModuleFieldRule> Rules { get; set; }

        /// <summary>
        /// 关联自
        /// </summary>
        public string Relate { get; set; }

        /// <summary>
        /// 枚举项
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> EnumValues { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int? Length { get; set; }

        /// <summary>
        /// 表单分组
        /// </summary>
        public string[] GroupNames { get; set; }

        /// <summary>
        /// 数据字典
        /// </summary>
        public EnumInfo EnumItemInfo { get; set; }
    } 


}
