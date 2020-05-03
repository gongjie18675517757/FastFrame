using FastFrame.Application;
using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using FastFrame.WebHost.Privder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers
{
    public class CommonController : BaseController
    {
         
        private readonly ITypeProvider typeProvider;
        private readonly IDescriptionProvider descriptionProvider;

        public CommonController( ITypeProvider typeProvider, IDescriptionProvider descriptionProvider)
        {
             
            this.typeProvider = typeProvider;
            this.descriptionProvider = descriptionProvider;
        }

        /// <summary>
        /// 生成ID
        /// </summary> 
        [HttpGet]
        [EveryoneAccess]
        public IEnumerable<string> Get(int count = 1)
        {
            return Enumerable.Range(1, count).Select(x => IdGenerate.NetId());
        }

        /// <summary>
        /// 生成密码
        /// </summary> 
        [HttpGet("{pwd}")]
        [EveryoneAccess]
        public KeyValuePair<string, string> MakePassword(string pwd)
        {
            var user = new User();
            user.GeneratePassword(pwd);
            return new KeyValuePair<string, string>(user.EncryptionKey, user.Password);
        } 

        /// <summary>
        /// 生成模块结构
        /// </summary> 
        [HttpGet("{name}")]
        public ModuleStruct ModuleStruts(string name)
        {
            var type = typeProvider.GetTypeByName(name);
            var fieldInfoStructs = new List<FieldInfoStrut>();

            var instance = type.Assembly.CreateInstance(type.FullName);
            if (instance is IHasManage hasManage)
            {

            }

            foreach (var x in type.GetProperties())
            {
                /*排除标志*/
                if (TryGetAttribute<ExcludeAttribute>(x, out var excludeAttribute))
                    continue;

                /*隐藏标记*/
                TryGetAttribute<HideAttribute>(x, out var hideAttribute);

                /*只读标记*/
                TryGetAttribute<ReadOnlyAttribute>(x, out var readOnlyAttribute);

                /*关联标记*/
                TryGetAttribute<RelatedToAttribute>(x, out var relatedToAttribute);

                /*必填*/
                TryGetAttribute<RequiredAttribute>(x, out var requiredAttribute);

                /*实际类型*/
                var nullableType = T4Help.GetNullableType(x.PropertyType);

                /*长度*/
                TryGetAttribute<StringLengthAttribute>(x, out var stringLengthAttribute);

                /*分组*/
                TryGetAttribute<FormGroupAttribute>(x, out var formGroupAttribute);

                /*数据字典*/
                TryGetAttribute<EnumItemAttribute>(x, out var enumItemAttribute);

                /*字段信息*/
                fieldInfoStructs.Add(new FieldInfoStrut()
                {
                    Name = x.Name,
                    Type = nullableType.IsArray ? "Array" : nullableType.Name,
                    Description = descriptionProvider.GetPropertyDescription(x),
                    Hide = hideAttribute?.HideMark,
                    Readonly = readOnlyAttribute?.ReadOnlyMark,
                    Rules = GetRules(x),
                    Relate = relatedToAttribute?.RelatedType.Name,
                    Length = stringLengthAttribute?.MaximumLength,
                    EnumValues = GetEnumValues(nullableType),
                    IsRequired = requiredAttribute != null,
                    GroupNames = formGroupAttribute?.GroupNames,
                    EnumItemInfo = enumItemAttribute == null ? null : new EnumInfo { Name = enumItemAttribute.Name, SuperPropName = enumItemAttribute.SuperPropName }
                });
            }

            TryGetAttribute<RelatedFieldAttribute>(type, out var relatedFieldAttribute);

            /*模块信息*/
            return new ModuleStruct()
            {
                Name = type.Name,
                Form = instance,
                Description = descriptionProvider.GetClassDescription(type),
                FieldInfoStruts = fieldInfoStructs,
                RelateFields = relatedFieldAttribute?.FieldNames ?? new[] { type.GetProperties().FirstOrDefault().Name },
                IsTree = typeof(ITreeEntity).IsAssignableFrom(type),
                HasManage = typeof(IHasManage).IsAssignableFrom(type)
            };
        }

        private IEnumerable<KeyValuePair<string, string>> GetEnumValues(Type type)
        {
            if (type.IsArray)
                type = type.GetElementType();

            if (!type.IsEnum)
                yield break;

            foreach (var item in Enum.GetNames(type))
            {
                yield return new KeyValuePair<string, string>(
                    item, descriptionProvider.GetEnumSummary(type, item));
            }

        }
        private IEnumerable<Rule> GetRules(PropertyInfo prop)
        {
            if (TryGetAttribute<RequiredAttribute>(prop, out _))
                yield return new Rule("required");
            if (TryGetAttribute<StringLengthAttribute>(prop, out var stringLengthAttribute))
                yield return new Rule("stringLength",
                    stringLengthAttribute.MinimumLength.ToString(),
                    stringLengthAttribute.MaximumLength.ToString());
            if (TryGetAttribute<UniqueAttribute>(prop, out _))
                yield return new Rule("unique", prop.DeclaringType.Name, prop.Name);
        }

        private bool TryGetAttribute<T>(PropertyInfo propertyInfo, out T attr) where T : Attribute
        {
            attr = propertyInfo.GetCustomAttribute<T>();
            if (attr != null)
                return true;
            return false;
        }

        private bool TryGetAttribute<T>(Type type, out T attr) where T : Attribute
        {
            attr = type.GetCustomAttribute<T>();
            if (attr != null)
                return true;
            return false;
        }
    }

    public class ModuleStruct
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(150)]
        public string Description { get; set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public List<FieldInfoStrut> FieldInfoStruts { get; set; }

        /// <summary>
        /// 被关联时显示的字段列表
        /// </summary>
        public IEnumerable<string> RelateFields { get; internal set; }

        /// <summary>
        /// 树结构
        /// </summary>
        public bool IsTree { get; internal set; }

        /// <summary>
        /// 有管理属性
        /// </summary>
        public bool HasManage { get; internal set; }

        /// <summary>
        /// 默认表单
        /// </summary>
        public object Form { get; internal set; }
    }

    public class FieldInfoStrut
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public HideMark? Hide { get; internal set; }
        public ReadOnlyMark? Readonly { get; internal set; }

        public IEnumerable<Rule> Rules { get; internal set; }
        public string Relate { get; internal set; }
        public IEnumerable<KeyValuePair<string, string>> EnumValues { get; internal set; }
        public bool IsRequired { get; internal set; }
        public int? Length { get; internal set; }
        public string[] GroupNames { get; internal set; }
        public EnumInfo EnumItemInfo { get; internal set; }
    }

    public class Rule
    {
        public Rule(string ruleName, params string[] rulePars)
        {
            RuleName = ruleName;
            RulePars = rulePars;
        }

        public string RuleName { get; }

        public string[] RulePars { get; }
    }

    public class EnumInfo
    {
        public string Name { get; set; }

        public string SuperPropName { get; set; }
    }
}
