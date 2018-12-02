using FastFrame.CodeGenerate.Info;
using FastFrame.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Entity;

namespace FastFrame.CodeGenerate.Build
{
    public class DtoBuild : BaseCodeBuild
    {
        public DtoBuild(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
        }

        public override string TargetPath => $"{SolutionDir}\\FastFrame.Dto\\Dtos\\Templates";


        public override IEnumerable<TargetInfo> BuildCodeInfo()
        {
            foreach (var type in GetTypes())
            {
                if (type.GetCustomAttribute<ExcludeAttribute>() != null)
                    continue;

                yield return GetTargetInfo(type);
            }
        }

        public TargetInfo GetTargetInfo(Type type)
        {
            var areaNameSpace = T4Help.GenerateNameSpace(type, null);

            var attrs = type.GetCustomAttributes<UniqueAttribute>().Select(x => new AttrInfo()
            {
                Name = "Unique",
                Parameters = x.UniqueNames.Select(y => $"\"{y}\"")
            }).Union(type.GetCustomAttributes<RelatedFieldAttribute>().Select(x => new AttrInfo()
            {
                Name = "RelatedField",
                Parameters = new[] {
                    $"\"{x.DefaultName}\""
                }.Union(x.OtherNames.Select(y => $"\"{y}\""))
            }));

            return new TargetInfo()
            {
                NamespaceName = $"FastFrame.Dto.{areaNameSpace}",
                ImportNames = new string[] {
                        $"FastFrame.Entity.{areaNameSpace}",
                        "FastFrame.Infrastructure.Attrs",
                        "global::System.ComponentModel.DataAnnotations",
                        "FastFrame.Entity.Enums",
                        "FastFrame.Entity.Basis",
                        "System"
                    }
                .Union(type.GetProperties()
                    .Select(x => x.GetCustomAttribute<RelatedToAttribute>())
                    .Where(x => x != null)
                    .SelectMany(x => new[] { x.RelatedType.Namespace ,
                        $"FastFrame.Dto.{T4Help.GenerateNameSpace(x.RelatedType,"")}" }))
                    .Distinct(),
                Summary = T4Help.GetClassSummary(type, XmlDocDir),
                Name = $"{type.Name}Dto",
                BaseNames = new string[] { $"BaseDto<{type.Name}>" },
                Path = $"{TargetPath}",
                CategoryName = "class",
                PropInfos = GetPropInfos(type),
                AttrInfos = attrs
            };
        }


        public IEnumerable<PropInfo> GetPropInfos(Type type)
        {
            var names= string.Join(",", type.GetProperties().Select(x => x.Name));
            Console.WriteLine(names);
            foreach (var item in type.GetProperties())
            {
                if (item.Name == "Id")
                    continue;
                if (item.Name == "Tenant_Id")
                    continue;
                if (item.Name == "IsDeleted")
                    continue;

                if (item.GetCustomAttribute<ExcludeAttribute>() != null)
                    continue;

                var summary = T4Help.GetPropertySummary(item, XmlDocDir);

                yield return new PropInfo()
                {
                    Summary = summary,
                    AttrInfos = GetAttrInfos(item),
                    TypeName = T4Help.GetTypeName(item.PropertyType),
                    Name = item.Name
                };

                if (TryGetAttribute<RelatedToAttribute>(item, out var relatedToAttribute))
                {
                    yield return new PropInfo()
                    {
                        Summary = summary,
                        TypeName = $"{T4Help.GetTypeName(relatedToAttribute.RelatedType)}Dto",
                        Name = item.Name.Replace("_Id", "")
                    };
                }
            }

            if (typeof(IHasManage).IsAssignableFrom(type))
            {
                yield return new PropInfo()
                {
                    Summary = "管理属性",
                    TypeName = "Foreign",
                    Name = "Foreign"
                };

                yield return new PropInfo()
                {
                    Summary = "创建人",
                    TypeName = "User",
                    Name = "Create_User"
                };

                yield return new PropInfo()
                {
                    Summary = "修改人",
                    TypeName = "User",
                    Name = "Modify_User"
                };
            }
        }

        public IEnumerable<AttrInfo> GetAttrInfos(PropertyInfo propertyInfo)
        {
            if (TryGetAttribute<StringLengthAttribute>(propertyInfo, out var stringLengthAttribute))
            {
                yield return new AttrInfo
                {
                    Name = "StringLength",
                    Parameters = new string[] {
                        stringLengthAttribute.MaximumLength.ToString()
                    }
                };
            }

            if (TryGetAttribute<RequiredAttribute>(propertyInfo, out var requiredAttribute))
            {
                yield return new AttrInfo() { Name = "Required", };
            }

            if (TryGetAttribute<UniqueAttribute>(propertyInfo, out var uniqueAttribute))
            {
                yield return new AttrInfo()
                {
                    Name = "Unique",
                    Parameters = uniqueAttribute.UniqueNames.Select(x => $"\"{x}\"")
                };
            }

            if (TryGetAttribute<HideAttribute>(propertyInfo, out var hideAttribute))
            {
                yield return new AttrInfo()
                {
                    Name = "Hide",
                    Parameters = new string[]
                    {
                        $"HideMark.{hideAttribute.HideMark.ToString()}"
                    }
                };
            }

            if (TryGetAttribute<EmailAddressAttribute>(propertyInfo, out var emailAddressAttribute))
            {
                yield return new AttrInfo() { Name = "EmailAddress" };
            }

            if (TryGetAttribute<PhoneAttribute>(propertyInfo, out var phoneAttribute))
            {
                yield return new AttrInfo() { Name = "Phone" };
            }

            if (TryGetAttribute<ReadOnlyAttribute>(propertyInfo, out var readOnlyAttribute))
            {
                yield return new AttrInfo()
                {
                    Name = "ReadOnly",
                    Parameters = new string[] { $"ReadOnlyMark.{readOnlyAttribute.ReadOnlyMark.ToString()}" }
                };
            }

            if (TryGetAttribute<RelatedToAttribute>(propertyInfo, out var relatedToAttribute))
            {
                yield return new AttrInfo()
                {
                    Name = "RelatedTo",
                    Parameters = new string[] { $"typeof({relatedToAttribute.RelatedType.Name})" }
                };
            }

            if (TryGetAttribute<RelatedFieldAttribute>(propertyInfo, out var relatedFieldAttribute))
            {
                yield return new AttrInfo()
                {
                    Name = "RelatedField",
                    Parameters = new[] { $"\"{relatedFieldAttribute.DefaultName}\"" }
                    .Union(
                        relatedFieldAttribute.OtherNames.Select(x => $"\"{x}\""))
                };
            }
        }

        public bool TryGetAttribute<T>(PropertyInfo propertyInfo, out T attr) where T : Attribute
        {
            attr = propertyInfo.GetCustomAttribute<T>();
            if (attr != null)
                return true;
            return false;
        }

        public bool TryGetAttribute<T>(Type type, out T attr) where T : Attribute
        {
            attr = type.GetCustomAttribute<T>();
            if (attr != null)
                return true;
            return false;
        }

        public bool TryGetAttributes<T>(PropertyInfo propertyInfo, out IEnumerable<T> attrs) where T : Attribute
        {
            attrs = propertyInfo.GetCustomAttributes<T>();
            if (attrs.Any())
                return true;
            return false;
        }
    }
}
