using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;
using System.Linq;

namespace FastFrame.SourceGenerator
{
    [Generator]
    public class Class2 : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            //Debugger.Launch();

            var code = @"namespace FastFrame.Database
{
    /// <summary>
    /// 数据库
    /// </summary>
  public class HelloGenerator11
  {
    public   void Test() => System.Console.WriteLine(""Hello Generator"");
    public   void Test2() => System.Console.WriteLine(""Hello Generator"");
    public   void Test24() => System.Console.WriteLine(""Hello Generator"");
  }
}"; 

            context.AddSource("HelloGenerator1.cs", code);
        }

        public void Initialize(GeneratorInitializationContext context)
        { 
        }
    }
}
