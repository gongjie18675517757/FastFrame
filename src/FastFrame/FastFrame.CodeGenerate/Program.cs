using FastFrame.CodeGenerate.Build;
using FastFrame.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFrame.CodeGenerate
{
    class Program
    {
        static void Main()
        {
            var baseType = typeof(IEntity);
            var codeBuildType = typeof(BaseCodeBuild);
            var builds = codeBuildType.Assembly
                    .GetTypes()
                    .Where(x => codeBuildType.IsAssignableFrom(x) && !x.IsAbstract);
            var writer = new CodeWriter();
            writer.WriteFileComplete += (s, e) => Console.WriteLine($"{DateTime.Now}\t{e}\t");
            foreach (var item in builds)
            {
                var constructorInfo = item.GetConstructors().FirstOrDefault();
                var obj = constructorInfo.Invoke(new object[] { "D:\\CoreProject\\FastFrame\\src\\FastFrame", baseType });
                var codeBuild = (BaseCodeBuild)obj;
                writer.Run(codeBuild);
            }
        }
    }
}
