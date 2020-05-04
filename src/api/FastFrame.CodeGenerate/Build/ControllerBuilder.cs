using FastFrame.CodeGenerate.Info;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FieldInfo = FastFrame.CodeGenerate.Info.FieldInfo;
using ParameterInfo = FastFrame.CodeGenerate.Info.ParameterInfo;

namespace FastFrame.CodeGenerate.Build
{
    public class ControllerBuilder : BaseCShapeCodeBuilder
    {
        public ControllerBuilder(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
        }

        public override string TargetPath => $"{SolutionDir}\\FastFrame.WebHost\\Controllers";

        public override string BuildName => "API控制器"; 
       

        public override IEnumerable<TargetInfo> GetTargetInfoList()
        {
            foreach (var type in GetTypes())
            { 
                var exportAttr = type.GetCustomAttribute<ExportAttribute>();

                if (exportAttr == null || !exportAttr.ExportMarks.Contains(ExportMark.Controller))
                    continue;

                var spaceName = T4Help.GenerateNameSpace(type, null);
                var name = type.Name;
                var summary = T4Help.GetClassSummary(type, XmlDocDir);

                var baseNames = new List<string>();
                if (typeof(Entity.IHasManage).IsAssignableFrom(type))
                    baseNames.Add($"BaseCURDController<{name}Dto>");
                else
                    baseNames.Add($"BaseController<{name}Dto>");

                yield return new TargetInfo()
                {
                    Summary = summary,
                    Path = $"{TargetPath}\\{spaceName}\\{type.Name}\\{type.Name}Controller.template.cs",
                    NamespaceName = $"FastFrame.WebHost.Controllers.{spaceName}",
                    ImportNames = new string[] { 
                        $"FastFrame.Entity.{spaceName}",
                        $"FastFrame.Application.{spaceName}",
                        "FastFrame.Infrastructure.Permission",
                        "FastFrame.Infrastructure.Interface"
                    },
                    CategoryName = "class",
                    Name = $"{name}Controller",
                    BaseNames = baseNames,
                    FieldInfos = new FieldInfo[] {
                        new FieldInfo(){ TypeName=$"{name}Service",FieldName="service"}
                    },
                    Constructor = new Info.ConstructorInfo()
                    {
                        Parms = new ParameterInfo[] {
                            new ParameterInfo() { TypeName = $"{name}Service", DefineName = "service" }
                        },
                        Super = new string[] { "service" },
                        CodeBlock = new string[] {
                            "this.service = service;"
                        }
                    },
                    AttrInfos = new[] {
                        new AttrInfo()
                        {
                            Name="Permission",
                            Parameters=new string[]{ $"nameof({name})" , $"\"{summary}\"" }
                        }
                    }
                };
            }
        }
    } 
}
