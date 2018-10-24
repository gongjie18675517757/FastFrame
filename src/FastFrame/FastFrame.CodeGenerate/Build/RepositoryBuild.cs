using FastFrame.CodeGenerate.Info;
using FastFrame.Infrastructure;
using System;
using System.Collections.Generic;

namespace FastFrame.CodeGenerate.Build
{
    public class RepositoryBuild : BaseCodeBuild
    {
        public RepositoryBuild(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
        }

        public override string TargetPath => $"{SolutionDir}\\FastFrame.Repository\\Repository\\Templates";

        public override IEnumerable<TargetInfo> BuildCodeInfo()
        {
            foreach (var type in GetTypes())
            {
                var areaNameSpace = T4Help.GenerateNameSpace(type, null);
                yield return new TargetInfo()
                {
                    NamespaceName = $"FastFrame.Repository.{areaNameSpace}",
                    ImportNames = new string[] {
                        $"FastFrame.Entity.{areaNameSpace}",
                        "FastFrame.Database",
                        "FastFrame.Infrastructure.Interface"
                    },
                    Summary = T4Help.GetClassSummary(type, XmlDocDir)+"[数据访问]",
                    Name = $"{type.Name}Repository",
                    BaseNames = new string[] { $"BaseRepository<{type.Name}>", $"IRepository<{type.Name}>" },
                    Path = $"{TargetPath}",
                    CategoryName = "class",
                    Constructor = new ConstructorInfo()
                    {
                        Parms = new ParameterInfo[] {
                            new ParameterInfo(){ TypeName="DataBase",DefineName="context"},
                            new ParameterInfo(){ TypeName="ICurrentUserProvider",DefineName="currentUserProvider"}
                        },
                        Super = new string[] { "context", "currentUserProvider" }
                    }
                };
            }
        }
    }
}
