using FastFrame.CodeGenerate.Build;
using FastFrame.Entity;
using FastFrame.Infrastructure;
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
            var baseType = typeof(IEntity);
            var types = baseType.Assembly.GetTypes().Where(x => baseType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
            var typeNames = types.Select((x, index) => (index + 1, x.Name)).ToDictionary(x => x.Item1, x => x.Name);

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
                var obj = constructorInfo.Invoke(new object[] { "D:\\CoreProject\\FastFrame\\src\\FastFrame", baseType });
                var codeBuild = (BaseCodeBuild)obj;
                writer.Run(codeBuild);
            }

            var types2 = types
                .Where(x =>
                      x.Name == typeName
                    && x.GetCustomAttribute<Infrastructure.Attrs.ExportAttribute>() != null);

            var basePath = @"D:\CoreProject\FastFrame\src\ClientApp\src\views";
            var docPath = @"D:\CoreProject\FastFrame\src\FastFrame\Lib";
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
                        var lines = new[] {
                            "<template>",
                            "  <Page v-bind=\"page\"/>",
                            "</template>",
                            "<script>",
                            "import Page from '@/components/Page/BasisListPage.vue'",
                            "export default {",
                              "  props:{",
                             "      success:Function,",
                             "      close:Function,",
                             "      pars:Object",
                             "  },",
                            "  components: {",
                            "    Page",
                            "  },",
                            "  data() {",
                            "    return {",
                            "      page: {",
                            "        moduleInfo: {",
                             $"          area:'{area.Key}',",
                             $"          name: '{type.Name}',",
                             $"          direction: '{T4Help.GetClassSummary(type,docPath)}'",
                             "        },",
                             "        pageInfo:{",
                             "          success:this.success,",
                             "          close:this.close,",
                             "          pars:this.pars",
                             "        }",
                             "      }",
                             "    }",
                             "  }",
                             "}",
                             "</script>"
                        };
                        WriteLines(listPath, lines);
                    }

                    var addPath = Path.Combine(pagePath, "Add.vue");
                    if (!File.Exists(addPath))
                    {
                        var lines = new[] {
                            "<template>",
                             "  <Page v-bind=\"page\" @success=\"$emit('success',$event)\"/>",
                             "</template>",
                             "",
                             "<script>",
                             "import Page from '@/components/Page/BasisFormPage.vue'",
                             "export default {",
                             "  props:{",
                             "      success:Function,",
                             "      close:Function,",
                             "      pars:Object",
                             "  },",
                             "  components: {",
                             "    Page",
                             "  },",
                             "  data() {",
                             "    return {",
                             "      page: {",
                             "        moduleInfo: {",
                            $"          area:'{area.Key}',",
                            $"          name: '{type.Name}',",
                            $"          direction: '{T4Help.GetClassSummary(type,docPath)}',",
                             "        },",
                             "        pageInfo:{",
                             "          success:this.success,",
                             "          close:this.close,",
                             "          pars:this.pars",
                             "        }",
                             "      }",
                             "    }",
                             "  }",
                             "}",
                             "</script>",
                             "",
                             "<style>",
                             "</style>"
                        };
                        WriteLines(addPath, lines);
                    }
                }
            }

            goto START;
        }

        static void WriteLines(string path, IEnumerable<string> lines)
        {
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
