using FastFrame.CodeGenerate.Info;
using FastFrame.Infrastructure;
using System;
using System.Collections.Generic;

namespace FastFrame.CodeGenerate.Build
{
    public class MappingBuilder(string solutionDir, Type baseEntityType) : BaseCShapeCodeBuilder(solutionDir, baseEntityType)
    {
        public override string BuildName => "数据映射";

        public override string TargetPath => $"{SolutionDir}\\FastFrame.Database\\Mapping";

        public override IEnumerable<TargetInfo> GetTargetInfoList()
        {
            foreach (var type in GetTypes())
            { 
                var areaNameSpace = T4Help.GenerateNameSpace(type, null);
                yield return new TargetInfo()
                {
                    NamespaceName = $"FastFrame.Database.Mapping.{areaNameSpace}",
                    ImportNames = new string[] { $"FastFrame.Entity.{areaNameSpace}", "Microsoft.EntityFrameworkCore.Metadata.Builders" },
                    Summary = T4Help.GetClassSummary(type, XmlDocDir),
                    Name = $"{type.Name}Mapping",
                    BaseNames = new string[] { $"BaseEntityMapping<{type.Name}>" },
                    Path = $"{TargetPath}\\{areaNameSpace}\\{type.Name}\\{type.Name}Mapping.template.cs",
                    CategoryName = "class"
                };
            }
        }
    }
}
