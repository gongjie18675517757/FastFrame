using FastFrame.CodeGenerate.Info;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FieldInfo = FastFrame.CodeGenerate.Info.FieldInfo;
using MethodInfo = FastFrame.CodeGenerate.Info.MethodInfo;
using ParameterInfo = FastFrame.CodeGenerate.Info.ParameterInfo;
using TargetInfo = FastFrame.CodeGenerate.Info.TargetInfo;

namespace FastFrame.CodeGenerate.Build
{
    public class ControllerBuilder(string solutionDir, Type baseEntityType) : BaseCShapeCodeBuilder(solutionDir, baseEntityType)
    {
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
                    baseNames.Add($"BaseCURDController<{name}Dto>(service)");
                else
                    baseNames.Add($"BaseController<{name}Dto>(service)");

                yield return new TargetInfo()
                {
                    Summary = summary,
                    Path = $"{TargetPath}\\{spaceName}\\{type.Name}\\{type.Name}Controller.template.cs",
                    NamespaceName = $"FastFrame.WebHost.Controllers.{spaceName}",
                    ImportNames = new string[] { 
                        //$"FastFrame.Entity.{spaceName}",
                        $"FastFrame.Application.{spaceName}",
                        $"FastFrame.Application",
                        $"Microsoft.AspNetCore.Mvc",
                        //"FastFrame.Infrastructure.Permission",
                        //"FastFrame.Infrastructure.Interface"
                    },
                    CategoryName = "class",
                    Name = $"{name}Controller",
                    BaseNames = baseNames,
                    FieldInfos = new FieldInfo[] {
                        new(){ TypeName=$"{name}Service",FieldName="service"}
                    },
                    Constructor = new Info.ConstructorInfo()
                    {
                        Parms = [new() { TypeName = $"{name}Service", DefineName = "service" }],
                        Super = ["service"],
                        CodeBlock = ["this.service = service;"]
                    },
                    MethodInfos = new MethodInfo[]
                    {
                        new()
                        {
                            IsOverride=false,
                            MethodName="TreeList",
                            Modifier="public",
                            Parms= [
                                new() {
                                    DefineName="super_id",
                                    TypeName="string"
                                },
                                new() {
                                    DefineName="kw",
                                    TypeName="string"
                                },
                            ],
                            ResultTypeName="IAsyncEnumerable<ITreeModel>",
                            CodeBlock= new string[]
                            {
                                "return service.TreeListAsync(super_id,kw);"
                            },
                            AttrInfos=[new() {
                                Name="HttpGet",
                                Parameters=new []{ "" }
                            }]

                        }
                    }
                    //AttrInfos = new[] {
                    //    new AttrInfo()
                    //    {
                    //        Name="PermissionGroup",
                    //        Parameters=new string[]{ $"nameof({name})" , $"\"{summary}\"" }
                    //    }
                    //}
                };
            }
        }
    }
}
