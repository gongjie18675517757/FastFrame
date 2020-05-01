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
            var types = baseType
                            .Assembly
                            .GetTypes()
                            .Where(x => baseType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract)
                            .OrderBy(v => v.Namespace)
                            .ThenBy(v => v.Name)
                            .ToArray();

            var typeGroups = types
                                .Select((x, index) => new { Index = index + 1, Type = x })
                                .GroupBy(v => T4Help.GenerateNameSpace(v.Type, null));


        START:

            Console.WriteLine("请输入要生成的类型:");
            foreach (var g in typeGroups)
            {
                Console.WriteLine();
                Console.WriteLine($"命名空间：{g.Key}");
                foreach (var item in g)
                {
                    var str = $"{item.Index}:{item.Type.Name}".PadRight(20, ' ');
                    Console.Write(str);

                    if (item.Index % 5 == 0)
                        Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("0:全部");

        INPUT:
            Console.Write(">:");
            var inputIndex = Console.ReadLine();
            if (int.TryParse(inputIndex, out var intIndex) && intIndex >= 0 && types.Length > intIndex - 1)
            {
                typeName = intIndex == 0 ? "" : types[intIndex].Name;
            }
            else if (types.Any(v => v.Name == inputIndex))
            {
                typeName = inputIndex;
            }
            else
            {
                goto INPUT;
            }

            var codeBuildType = typeof(BaseCodeBuilder);
            var builds = codeBuildType.Assembly
                    .GetTypes()
                    .Where(x => codeBuildType.IsAssignableFrom(x) && !x.IsAbstract);

            foreach (var item in builds)
            {
                var constructorInfo = item.GetConstructors().FirstOrDefault();
                var obj = constructorInfo.Invoke(new object[] { rootPath, baseType });
                var builder = (IBaseCodeBuilder)obj;

                RunWrite(builder, new string[] { typeName }, v => Console.WriteLine(Path.GetFullPath(v.TargetPath)));
            }

            goto START;
        }

        static void RunWrite(IBaseCodeBuilder codeBuild, string[] targetTypeNames, Action<Info.BuildTarget> cb = null)
        {
            targetTypeNames = targetTypeNames.Where(v => !v.IsNullOrWhiteSpace()).ToArray();
            var targets = codeBuild.Build(targetTypeNames);
            foreach (var target in targets)
            {
                if (!target.Forcibly)
                    if (File.Exists(target.TargetPath))
                        continue;

                var dirName = Path.GetDirectoryName(target.TargetPath);
                if (!Directory.Exists(dirName))
                    Directory.CreateDirectory(dirName);

                File.WriteAllText(target.TargetPath, target.CodeBlock);
                cb?.Invoke(target);
            }
        }
    }
}
