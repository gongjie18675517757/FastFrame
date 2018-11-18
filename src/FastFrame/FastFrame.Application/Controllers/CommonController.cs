using FastFrame.Application.Privder;
using FastFrame.Dto;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers
{
    public class CommonController : BaseController
    {
        private readonly IScopeServiceLoader scopeServiceLoader;
        private readonly ITypeProvider typeProvider;
        private readonly IDescriptionProvider descriptionProvider;

        public CommonController(IScopeServiceLoader scopeServiceLoader, ITypeProvider typeProvider, IDescriptionProvider descriptionProvider)
        {
            this.scopeServiceLoader = scopeServiceLoader;
            this.typeProvider = typeProvider;
            this.descriptionProvider = descriptionProvider;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> Get(int count=1)
        {
            return Enumerable.Range(1, count).Select(x => IdGenerate.NetId());
        }

        /// <summary>
        /// 验证唯一性
        /// </summary>      
        [HttpPost]
        public async Task<bool> VerififyUnique(UniqueInput uniqueInput)
        {
            var entityType = typeProvider.GetTypeByName(uniqueInput.ModuleName);
            var areaName = T4Help.GenerateNameSpace(entityType, "");
            var serverName = $"FastFrame.Service.Services.{areaName}.{entityType.Name}Service";
            var serviceType = typeof(IService).Assembly.GetType(serverName);
            if (serviceType == null)
                throw new Exception("模块名称传入不正确!");

            var service = (IVerifyUniqueService)scopeServiceLoader.GetService(serviceType);
            return await service.VerifyUnique(uniqueInput);
        }

        /// <summary>
        /// 生成模块结构
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<ModuleStruct> ModuleStruts(string name)
        {
            var type = typeProvider.GetTypeByName(name);
            var fieldInfoStructs = new List<FieldInfoStrut>();

            var instance = type.Assembly.CreateInstance(type.FullName);

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

                /*实际类型*/
                var nullableType = T4Help.GetNullableType(x.PropertyType);

                /*字段信息*/
                fieldInfoStructs.Add(new FieldInfoStrut()
                {
                    Name = x.Name,
                    Type = nullableType.Name,
                    Description = await descriptionProvider.GetPropertyDescription(x),
                    Hide = hideAttribute?.HideMark,
                    Readonly = readOnlyAttribute?.ReadOnlyMark,
                    DefaultValue = instance.GetValue(x.Name),
                    Rules = GetRules(x),
                    Relate = relatedToAttribute?.RelatedType.Name, 
                });
            }

            TryGetAttribute<RelatedFieldAttribute>(type, out var relatedFieldAttribute);

            /*树状标记*/
            TryGetAttribute<TreeAttribute>(type, out var treeAttribute);

            /*模块信息*/
            return new ModuleStruct()
            {
                Name = type.Name,
                Description = await descriptionProvider.GetClassDescription(type),
                FieldInfoStruts = fieldInfoStructs,
                RelateFields = relatedFieldAttribute?.FieldNames,
                TreeKey= treeAttribute?.Key
            };
        } 

        private IEnumerable<Rule> GetRules(PropertyInfo prop)
        {
            if (TryGetAttribute<RequiredAttribute>(prop, out var requiredAttribute))
                yield return new Rule("required");
            if (TryGetAttribute<StringLengthAttribute>(prop, out var stringLengthAttribute))
                yield return new Rule("stringLength",
                    stringLengthAttribute.MinimumLength.ToString(),
                    stringLengthAttribute.MaximumLength.ToString());
            if (TryGetAttribute<UniqueAttribute>(prop, out var uniqueAttribute))
                yield return new Rule("unique",prop.DeclaringType.Name,prop.Name);
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
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public List<FieldInfoStrut> FieldInfoStruts { get; set; }

        /// <summary>
        /// 被关联时显示的字段列表
        /// </summary>
        public IEnumerable<string> RelateFields { get; internal set; }
        public string TreeKey { get; internal set; }
    }

    public class FieldInfoStrut
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public HideMark? Hide { get; internal set; }
        public ReadOnlyMark? Readonly { get; internal set; }
        public object DefaultValue { get; internal set; }
        public IEnumerable<Rule> Rules { get; internal set; }
        public string Relate { get; internal set; } 
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
}
