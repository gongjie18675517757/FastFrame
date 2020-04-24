using FastFrame.CodeGenerate.Build;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FastFrame.CodeGenerate
{
    class Program
    {
        static void Main()
        {
            string typeName = "";
            string rootPath = new DirectoryInfo("../../../../").FullName;
            var baseType = typeof(IEntity);
            var types = baseType.Assembly.GetTypes().Where(x => baseType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
            var typeNames = types.Select((x, index) => (index + 1, x.Name))
                    .ToDictionary(x => x.Item1, x => x.Name);

        START:
            Console.WriteLine("请输入要生成的类型:");
            foreach (var (index, Name) in typeNames)
            {
                Console.WriteLine($"{index}:{Name}");
            }
            Console.WriteLine("0:全部");

        INPUT:
            Console.Write(">:");
            var inputIndex = Console.ReadLine();
            if (int.TryParse(inputIndex, out var intIndex) && (typeNames.ContainsKey(intIndex) || intIndex == 0))
            {
                typeName = intIndex == 0 ? "" : typeNames[intIndex];
            }
            else if (typeNames.Values.Contains(inputIndex))
            {
                typeName = inputIndex;
            }
            else
            {
                goto INPUT;
            }

            var codeBuildType = typeof(BaseCodeBuild);
            var builds = codeBuildType.Assembly
                    .GetTypes()
                    .Where(x => codeBuildType.IsAssignableFrom(x) && !x.IsAbstract);
            var writer = new CodeWriter(typeName);
            writer.WriteFileComplete += (s, e) => Console.WriteLine($"{DateTime.Now}\t{e}\t");
            foreach (var item in builds)
            {
                var constructorInfo = item.GetConstructors().FirstOrDefault();
                var obj = constructorInfo.Invoke(new object[] { rootPath, baseType });
                var codeBuild = (BaseCodeBuild)obj;
                writer.Run(codeBuild);
            }

            var types2 = types
                .Where(x => (typeName.IsNullOrWhiteSpace() || x.Name == typeName) && 
                            x.GetCustomAttribute<ExportAttribute>()?.ExportMarks.Contains(ExportMark.VuePage) == true);

            var basePath = $@"{Directory.GetParent(rootPath).Parent}\admin\vue-admin\src\views";
            var docPath = Directory.GetCurrentDirectory();
            var listVueContent = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "List.vue"));
            var addVueContent = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Add.vue"));

            string ReplacePlaceholder(string line, Type type)
            {
                return line.Replace("{{AreaName}}", T4Help.GenerateNameSpace(type, ""))
                    .Replace("{{ModuleName}}", type.Name)
                    .Replace("{{Description}}", T4Help.GetClassSummary(type, docPath));
            }

            foreach (var area in types2.GroupBy(x => T4Help.GenerateNameSpace(x, null)))
            {
                var path = Path.Combine(basePath, area.Key);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                foreach (var type in area)
                {
                    var pagePath = Path.Combine(path, type.Name);
                    if (!Directory.Exists(pagePath)) Directory.CreateDirectory(pagePath);

                    var listPath = Path.Combine(pagePath, "List.vue");

                    if (!File.Exists(listPath))
                    {
                        WriteLines(listPath,
                            listVueContent.Select(r => ReplacePlaceholder(r, type)));
                    }

                    var addPath = Path.Combine(pagePath, "Add.vue");


                    if (!File.Exists(addPath) && typeof(IHasManage).IsAssignableFrom(type))
                    {
                        WriteLines(addPath, addVueContent
                            .Select(r => ReplacePlaceholder(r, type)));
                    }
                }
            }

            goto START;
        }

        static void WriteLines(string path, IEnumerable<string> lines)
        {
            Console.WriteLine(path);
            using (var file = File.Create(path))
            {
                using (var write = new StreamWriter(file, Encoding.Default))
                {
                    foreach (var line in lines)
                    {
                        write.WriteLine(line);
                    }
                }
            }
        }
    }
}
