using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace FastFrame.WebHost.Privder
{
    public class ModuleExportProvider : IModuleExportProvider
    {
        private readonly IModuleDesProvider descriptionProvider;
        private readonly IApplicationSession appSessionProvider;
        private static readonly Dictionary<string, ModuleStruct> cacheModuleKvs = new Dictionary<string, ModuleStruct>();
        private static IEnumerable<KeyValuePair<string, string>> haveCheckModuleList = null;

        public ModuleExportProvider(IModuleDesProvider descriptionProvider, IApplicationSession appSessionProvider)
        {
            this.descriptionProvider = descriptionProvider;
            this.appSessionProvider = appSessionProvider;
        }

        /// <summary>
        /// 获取模块结构
        /// </summary>  
        public ModuleStruct GetModuleStruts(string name)
        {
            name = name.ToLower();
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            if (cacheModuleKvs.TryGetValue(name, out var @struct))
            {
                return @struct;
            }

            if (!TypeManger.TryGetType(name, out var type))
            {
                return null;
            }

            var fieldInfoStructs = new List<ModuleFieldStrut>();

            var instance = type.Assembly.CreateInstance(type.FullName);
            if (instance is IHasManage hasManage)
            {
                var curr = appSessionProvider.CurrUser;
                hasManage.Create_User_Id = curr?.Id;
                hasManage.CreateTime = DateTime.Now;
                hasManage.Modify_User_Id = curr?.Id;
                hasManage.ModifyTime = DateTime.Now;
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
                fieldInfoStructs.Add(new ModuleFieldStrut()
                {
                    Name = x.Name,
                    Type = nullableType.IsArray ? "Array" : nullableType.Name,
                    Description = descriptionProvider.GetPropertyDescription(x),
                    Hide = hideAttribute?.HideMark.ToString(),
                    Readonly = readOnlyAttribute?.ReadOnlyMark.ToString(),
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
            @struct = new ModuleStruct()
            {
                Name = type.Name,
                Form = instance,
                Description = descriptionProvider.GetClassDescription(type),
                FieldInfoStruts = fieldInfoStructs,
                RelateFields = relatedFieldAttribute?.FieldNames ?? new[] { type.GetProperties().FirstOrDefault().Name },
                IsTree = typeof(ITreeEntity).IsAssignableFrom(type),
                HasManage = typeof(IHasManage).IsAssignableFrom(type),
                HasFiles = typeof(IHaveMultiFile).IsAssignableFrom(type),
                HaveCheck = typeof(IHaveCheck).IsAssignableFrom(type),
            };

            cacheModuleKvs.Add(name, @struct);

            return @struct;
        }

        /// <summary>
        /// 需要审核的模块
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, string>> HaveCheckModuleList()
        {
            lock (this)
            {
                var haveCheckType = typeof(IHaveCheck);
                haveCheckModuleList = TypeManger.RegisterdTypes
                  .Where(v => haveCheckType.IsAssignableFrom(v) && v.IsClass && !v.IsAbstract)
                  .Select(v => new KeyValuePair<string, string>(v.Name, descriptionProvider.GetClassDescription(v)))
                  .Concat(cacheModuleKvs.Values.Where(v => v.HaveCheck).Select(v => new KeyValuePair<string, string>(v.Name, v.Description)))
                  .Distinct()
                  .ToList();
            }
            return haveCheckModuleList;
        }

        /// <summary>
        /// 注册模块
        /// </summary>
        /// <param name="moduleStructs"></param>
        public void RegisterModule(params ModuleStruct[] moduleStructs)
        {
            foreach (var item in moduleStructs)
            {
                cacheModuleKvs.Add(item.Name.ToLower(), item);
            }
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

        private IEnumerable<ModuleFieldRule> GetRules(PropertyInfo prop)
        {
            if (TryGetAttribute<RequiredAttribute>(prop, out _))
                yield return new ModuleFieldRule("required");
            if (TryGetAttribute<StringLengthAttribute>(prop, out var stringLengthAttribute))
                yield return new ModuleFieldRule("stringLength",
                    stringLengthAttribute.MinimumLength.ToString(),
                    stringLengthAttribute.MaximumLength.ToString());
            if (TryGetAttribute<UniqueAttribute>(prop, out _))
                yield return new ModuleFieldRule("unique", prop.DeclaringType.Name, prop.Name);
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
}
