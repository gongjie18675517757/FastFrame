﻿using FastFrame.CodeGenerate.Info;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using TargetInfo = FastFrame.CodeGenerate.Info.TargetInfo;

namespace FastFrame.CodeGenerate.Build
{
    public class DtoBuilder(string solutionDir, Type baseEntityType) : BaseCShapeCodeBuilder(solutionDir, baseEntityType)
    {
        public override string BuildName => "DTO";

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
                                .Concat(typeof(IHaveMultiFile).IsAssignableFrom(type) ? ["IHaveMultiFileDto"] : Array.Empty<string>())
                                //.Concat(typeof(ITreeEntity).IsAssignableFrom(type) ? new[] { "ITreeModel" } : Array.Empty<string>())
                                .Concat(typeof(IHaveCheck).IsAssignableFrom(type) ? ["IHaveCheckModel"] : Array.Empty<string>())
                                ,
                Path = $"{TargetPath}\\{areaNameSpace}\\{type.Name}\\Dto\\{type.Name}Dto.template.cs",
                CategoryName = "class",
                PropInfos = GetPropInfos(type),
                AttrInfos = Array.Empty<AttrInfo>() /*attrs*/
            };
        }


        public static IEnumerable<PropInfo> GetPropInfos(Type type)
        {
            var instance = type.Assembly.CreateInstance(type.FullName);

            var props = type.GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                var item = props[i];

                if (item.Name == "Id")
                    continue;

                if (item.GetCustomAttribute<ExcludeAttribute>() != null)
                    continue;

                var summary = T4Help.GetPropertySummary(item, XmlDocDir);

                var defaultValue = instance.GetValue(item.Name)?.ToString();
                defaultValue ??= "null";

                if (T4Help.GetNullableType(item.PropertyType) == typeof(string))
                    defaultValue = $"\"{defaultValue}\"";

                var is_range_begin_value = false;
                var is_range_end_value = false;

                if (TryGetAttribute<RangeValueBeginAttribute>(item, out _))
                    is_range_begin_value = true;

                if (i > 0 && TryGetAttribute<RangeValueBeginAttribute>(props[i - 1], out _))
                    is_range_end_value = true;

                yield return new PropInfo()
                {
                    Summary = summary,
                    AttrInfos = (is_range_begin_value || is_range_end_value) ?
                                    [new AttrInfo
                                    {
                                        Name = "Hide",
                                        Parameters = new string[]
                                        {
                                            $"HideMark.{HideMark.All}"
                                        }
                                    }] : GetAttrInfos(item),
                    TypeName = T4Help.GetTypeName(item.PropertyType),
                    Name = item.Name,
                    DefaultValue = defaultValue
                };

                if (is_range_end_value)
                {
                    var type_name = $"ValueRange<{T4Help.GetTypeName(T4Help.GetNullableType(item.PropertyType))}>";
                    yield return new PropInfo()
                    {
                        Summary = summary.Replace("止", ""),
                        AttrInfos = GetAttrInfos(item),
                        TypeName = type_name,
                        Name = item.Name.Replace("End", ""),
                        DefaultValue = defaultValue,
                        GetCodeBlock = [
                                $"return new {type_name}({props[i - 1].Name},{item.Name});"
                            ],
                        SetCodeBlock = [
                            $"{props[i - 1].Name}=value.BeginValue;",
                            $"{item.Name}=value.EndValue;",
                        ]
                    };
                }

                if (TryGetAttribute<RelatedToAttribute>(item, out var relatedToAttribute))
                {
                    var relateTypeName = relatedToAttribute.RelatedType.Name;
                    yield return new PropInfo()
                    {
                        Summary = summary,
                        TypeName = $"string",
                        Name = item.Name.Replace("_Id", "_Value"),
                        AttrInfos = [new AttrInfo
                        {
                            //Name = $"{nameof(ValueRelateFor)}<{relateTypeName}>",
                            Name = nameof(ValueRelateFor),
                            Parameters = [$"nameof({item.Name})", $"typeof({relateTypeName})"]
                        }]
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
                    Name = $"RelatedTo<{relatedToAttribute.RelatedType.Name}>",
                    Parameters = []
                };
            }

            if (TryGetAttribute<EnumItemAttribute>(propertyInfo, out var enumItemAttribute))
            {
                yield return new AttrInfo()
                {
                    Name = "EnumItem",
                    Parameters = new string[] { $"EnumName.{enumItemAttribute.Name}" }
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
