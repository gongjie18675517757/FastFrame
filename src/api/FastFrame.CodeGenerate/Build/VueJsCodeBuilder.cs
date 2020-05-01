using FastFrame.CodeGenerate.Info;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FastFrame.CodeGenerate.Build
{
    /// <summary>
    /// 生成VUE页面
    /// </summary>
    public class VueJsCodeBuilder : BaseJsCodeBuilder
    {
        public VueJsCodeBuilder(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
        }

        public override string BuildName => "Vue页面";

        public override string TargetPath => $@"{Directory.GetParent(this.SolutionDir).Parent}\admin\vue-admin\src\views";  

        public override IEnumerable<BuildTarget> Build(params string[] targetNames)
        {
            var types = this.GetTypes();

            var listVueContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "List.vue"));
            var addVueContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Add.vue"));
            foreach (var type in types)
            { 
                if (targetNames.Length > 0 && !targetNames.Any(v => type.Name.StartsWith(v)))
                    continue;

                var exportAttr = type.GetCustomAttribute<ExportAttribute>();

                if (exportAttr == null || !exportAttr.ExportMarks.Contains(ExportMark.VuePage))
                    continue;

                var areaName = T4Help.GenerateNameSpace(type, null);
                var path = $"{TargetPath}\\{areaName}\\{type.Name}"; 
                
                yield return new BuildTarget
                {
                    CodeBlock = ReplacePlaceholder(listVueContent, type),
                    TargetPath = Path.Combine(path, "List.vue"),
                    Forcibly = this.Forcibly
                };

                yield return new BuildTarget
                {
                    CodeBlock = ReplacePlaceholder(addVueContent, type),
                    TargetPath = Path.Combine(path, "Add.vue"),
                    Forcibly = this.Forcibly
                };
            }
        }

        private string ReplacePlaceholder(string line, Type type)
        {
            return line.Replace("{{AreaName}}", T4Help.GenerateNameSpace(type, ""))
                .Replace("{{ModuleName}}", type.Name)
                .Replace("{{Description}}", T4Help.GetClassSummary(type, this.XmlDocDir));
        }
    }
}
