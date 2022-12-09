using FastFrame.CodeGenerate.Info;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using TargetInfo = FastFrame.CodeGenerate.Info.TargetInfo;

namespace FastFrame.CodeGenerate.Build
{
    public class DtoBuilder : BaseCShapeCodeBuilder
    {
        public override string BuildName => "DTO";
        public DtoBuilder(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
        }

        public override string TargetPath => $"{SolutionDir}\\FastFrame.Application";


        public override IEnumerable<TargetInfo> GetTargetInfoList()
        {
            foreach (var type in GetTypes())
            {
                var exportAttr = type.GetCustomAttribute<ExportAttribute>();

                if (exportAttr == null || !exportAttr.ExportMarks.Contains(ExportMark.DTO))
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
            });

            return new Info.TargetInfo()
            {
                NamespaceName = $"FastFrame.Application.{areaNameSpace}",
                ImportNames = new string[] {
                        $"FastFrame.Entity.{areaNameSpace}",
                        "FastFrame.Infrastructure",
                        "System.ComponentModel.DataAnnotations",
                        "FastFrame.Entity.Enums",
                        "FastFrame.Entity.Basis",
                        "FastFrame.Entity",
                        "System.Collections.Generic",
                        "System"
                    }
                .Union(type.GetProperties()
                    .Select(x => x.GetCustomAttribute<RelatedToAttribute>())
                    .Where(x => x != null)
                    .SelectMany(x => new[] { x.RelatedType.Namespace ,
                        $"FastFrame.Application.{T4Help.GenerateNameSpace(x.RelatedType,"")}" }))
                    .Distinct(),
                Summary = T4Help.GetClassSummary(type, XmlDocDir),
                Name = $"{type.Name}Dto",
                BaseNames = new string[] { $"BaseDto<{type.Name}>" }
                                .Concat(typeof(IHaveMultiFile).IsAssignableFrom(type) ? new[] { "IHaveMultiFileDto" } : Array.Empty<string>())
                                //.Concat(typeof(ITreeEntity).IsAssignableFrom(type) ? new[] { "ITreeModel" } : Array.Empty<string>())
                                .Concat(typeof(IHaveCheck).IsAssignableFrom(type) ? new[] { "IHaveCheckModel" } : Array.Empty<string>())
                                ,
                Path = $"{TargetPath}\\{areaNameSpace}\\{type.Name}\\Dto\\{type.Name}Dto.template.cs",
                CategoryName = "class",
                PropInfos = GetPropInfos(type),
                AttrInfos = Array.Empty<AttrInfo>() /*attrs*/
            };
        }


        public IEnumerable<PropInfo> GetPropInfos(Type type)
        {
            var instance = type.Assembly.CreateInstance(type.FullName);

            foreach (var item in type.GetProperties())
            {
                if (item.Name == "Id")
                    continue;

                if (item.GetCustomAttribute<ExcludeAttribute>() != null)
                    continue;

                var summary = T4Help.GetPropertySummary(item, XmlDocDir);

                var defaultValue = instance.GetValue(item.Name)?.ToString();
                defaultValue ??= "null";

                if (T4Help.GetNullableType(item.PropertyType) == typeof(string))
                    defaultValue = $"\"{defaultValue}\"";

                yield return new PropInfo()
                {
                    Summary = summary,
                    AttrInfos = GetAttrInfos(item),
                    TypeName = T4Help.GetTypeName(item.PropertyType),
                    Name = item.Name,
                    DefaultValue = defaultValue
                };

                if (TryGetAttribute<RelatedToAttribute>(item, out var _))
                {
                    //var relateTypeName = relatedToAttribute.RelatedType.Name;
                    yield return new PropInfo()
                    {
                        Summary = summary,
                        TypeName = $"string",
                        Name = item.Name.Replace("_Id", "_Value")
                    };
                }
            }

            //if (typeof(ITreeEntity).IsAssignableFrom(type))
            //    yield return new PropInfo
            //    {
            //        Name = "ChildCount",
            //        DefaultValue = "0",
            //        Summary = "下级数量",
            //        TypeName = "int"
            //    };

            if (typeof(IHaveMultiFile).IsAssignableFrom(type))
                yield return new PropInfo
                {
                    Name = "Files",
                    DefaultValue = "Array.Empty<ResourceModel>()",
                    Summary = "附件",
                    TypeName = "IEnumerable<ResourceModel>"
                };

            if (typeof(IHaveCheck).IsAssignableFrom(type))
                yield return new PropInfo
                {
                    Name = "CheckerIds",
                    DefaultValue = "Array.Empty<string>()",
                    Summary = "可审核人",
                    TypeName = "IEnumerable<string>"
                };

            if (typeof(IHaveCheck).IsAssignableFrom(type))
                yield return new PropInfo
                {
                    Name = "StepList",
                    DefaultValue = "Array.Empty<Flow.FlowStepModel>()",
                    Summary = "流程步骤",
                    TypeName = "IEnumerable<Flow.FlowStepModel>"
                };
        }

        public static IEnumerable<AttrInfo> GetAttrInfos(PropertyInfo propertyInfo)
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

            if (TryGetAttribute<RequiredAttribute>(propertyInfo, out _))
            {
                yield return new AttrInfo() { Name = "Required", };
            }         
            
            if (TryGetAttribute<IsPrimaryFieldAttribute>(propertyInfo, out _))
            {
                yield return new AttrInfo() { Name = "IsPrimaryField", };
            }

            //if (TryGetAttribute<UniqueAttribute>(propertyInfo, out var uniqueAttribute))
            //{
            //    yield return new AttrInfo()
            //    {
            //        Name = "Unique",
            //        Parameters = uniqueAttribute.UniqueNames.Select(x => $"\"{x}\"")
            //    };
            //}

            if (TryGetAttribute<HideAttribute>(propertyInfo, out var hideAttribute))
            {
                yield return new AttrInfo()
                {
                    Name = "Hide",
                    Parameters = new string[]
                    {
                        $"HideMark.{hideAttribute.HideMark}"
                    }
                };
            }

            if (TryGetAttribute<EmailAddressAttribute>(propertyInfo, out _))
            {
                yield return new AttrInfo() { Name = "EmailAddress" };
            }

            if (TryGetAttribute<PhoneAttribute>(propertyInfo, out _))
            {
                yield return new AttrInfo() { Name = "Phone" };
            }

            //if (TryGetAttribute<ReadOnlyAttribute>(propertyInfo, out var readOnlyAttribute))
            //{
            //    yield return new AttrInfo()
            //    {
            //        Name = "ReadOnly",
            //        Parameters = new string[] { $"ReadOnlyMark.{readOnlyAttribute.ReadOnlyMark.ToString()}" }
            //    };
            //}

            if (TryGetAttribute<RelatedToAttribute>(propertyInfo, out var relatedToAttribute))
            {
                yield return new AttrInfo()
                {
                    Name = "RelatedTo",
                    Parameters = new string[] { $"typeof({relatedToAttribute.RelatedType.Name})" }
                };
            }

            if (TryGetAttribute<EnumItemAttribute>(propertyInfo, out var enumItemAttribute))
            {
                yield return new AttrInfo()
                {
                    Name = "EnumItem",
                    Parameters = new string[] { $"\"{enumItemAttribute.Name}\"" }
                };
            }

            
        }

        public static bool TryGetAttribute<T>(PropertyInfo propertyInfo, out T attr) where T : Attribute
        {
            attr = propertyInfo.GetCustomAttribute<T>();
            if (attr != null)
                return true;
            return false;
        }

        public static bool TryGetAttribute<T>(Type type, out T attr) where T : Attribute
        {
            attr = type.GetCustomAttribute<T>();
            if (attr != null)
                return true;
            return false;
        }

        public static bool TryGetAttributes<T>(PropertyInfo propertyInfo, out IEnumerable<T> attrs) where T : Attribute
        {
            attrs = propertyInfo.GetCustomAttributes<T>();
            if (attrs.Any())
                return true;
            return false;
        }
    }
}
