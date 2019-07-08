﻿using FastFrame.CodeGenerate.Info;
using FastFrame.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastFrame.Infrastructure.Attrs;

namespace FastFrame.CodeGenerate.Build
{
    public class ViewModelBuild : BaseCodeBuild
    {
        public ViewModelBuild(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
        }

        public override string ProductName => "ViewModel";
        public override string TargetPath => $"{SolutionDir}\\FastFrame.Dto\\ViewModels\\Templates";

        public override IEnumerable<Info.TargetInfo> BuildCodeInfo(string typeName)
        {
            foreach (var type in GetTypes())
            {
                if (!string.IsNullOrWhiteSpace(typeName) && type.Name != typeName)
                    continue;
                if (type.GetCustomAttribute<ExcludeAttribute>() != null)
                    continue;

                var areaNameSpace = T4Help.GenerateNameSpace(type, null);
                yield return new Info.TargetInfo
                {
                    NamespaceName = $"FastFrame.Dto.{areaNameSpace}",
                    ImportNames = new[] { "System", "FastFrame.Entity.Enums" }
                            .Concat(type.GetProperties().Select(v => v.PropertyType.Namespace))
                            .Distinct(),
                    Summary = T4Help.GetClassSummary(type, XmlDocDir),
                    Name = $"{type.Name}ViewModel",
                    Path = $"{TargetPath}",
                    CategoryName = "class",
                    PropInfos = GetPropInfos(type),
                    BaseNames = new[] { "IViewModel" }
                };
            }
        }

        private IEnumerable<PropInfo> GetPropInfos(Type type)
        {
            var fieldNames = type.GetCustomAttribute<RelatedFieldAttribute>()?.FieldNames.ToList();
            if (fieldNames == null)
            {
                fieldNames = type.GetProperties().Select(v => v.Name).ToList();
                var baseFieldNames = type.BaseType.GetProperties().Select(v => v.Name).ToList();
                fieldNames = fieldNames.Where(v => !baseFieldNames.Any(r => r == v)).ToList();
            }

            if (!fieldNames.Contains("Id"))
            {
                fieldNames.Add("Id");
            }
            foreach (var item in fieldNames)
            {
                var property = type.GetProperty(item);

                yield return new PropInfo
                {
                    Name = item,
                    TypeName = T4Help.GetTypeName(property.PropertyType),
                    Summary = T4Help.GetPropertySummary(property, XmlDocDir)
                };
            }
        }
    }
}
