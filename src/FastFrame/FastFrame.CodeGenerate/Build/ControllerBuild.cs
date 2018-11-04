using FastFrame.CodeGenerate.Info;
using FastFrame.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;
using FieldInfo = FastFrame.CodeGenerate.Info.FieldInfo;
using ParameterInfo = FastFrame.CodeGenerate.Info.ParameterInfo;

namespace FastFrame.CodeGenerate.Build
{
    public class ControllerBuild : BaseCodeBuild
    {
        public ControllerBuild(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
        }

        public override string TargetPath => $"{SolutionDir}\\FastFrame.Application\\Controllers\\Templates";

        public override IEnumerable<TargetInfo> BuildCodeInfo()
        {
            foreach (var type in GetTypes())
            {
                if (type.GetCustomAttribute<Infrastructure.Attrs.ExportAttribute>() == null)
                    continue;

                var spaceName = T4Help.GenerateNameSpace(type, null);
                var name = type.Name;
                yield return new TargetInfo()
                {
                    Summary = T4Help.GetClassSummary(type, XmlDocDir),
                    Path = TargetPath,
                    NamespaceName = $"FastFrame.Application.Controllers.{spaceName}",
                    ImportNames = new string[] {
                        $"FastFrame.Dto.{spaceName}",
                        $"FastFrame.Entity.{spaceName}",
                        $"FastFrame.Service.Services.{spaceName}",
                        "FastFrame.Infrastructure.Interface"
                    },
                    CategoryName = "class",
                    Name = $"{name}Controller",
                    BaseNames = new string[] { $"BaseController<{name}, {name}Dto>" },
                    FieldInfos = new FieldInfo[] {
                        new FieldInfo(){ TypeName=$"{name}Service",FieldName="service"},
                        new FieldInfo(){ TypeName=$"IScopeServiceLoader",FieldName="serviceLoader"}
                    },
                    Constructor = new Info.ConstructorInfo()
                    {
                        Parms = new ParameterInfo[] {
                            new ParameterInfo() { TypeName = $"{name}Service", DefineName = "service" },
                            new ParameterInfo(){ TypeName="IScopeServiceLoader",DefineName="serviceLoader"}
                        },
                        Super = new string[] { "service", "serviceLoader" },
                        CodeBlock = new string[] {
                            "this.service = service;",
                            "this.serviceLoader = serviceLoader;"
                        }
                    }
                };
            }
        }
    }

}
