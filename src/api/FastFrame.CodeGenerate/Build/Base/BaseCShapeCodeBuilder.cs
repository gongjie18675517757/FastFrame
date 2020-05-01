﻿using FastFrame.CodeGenerate.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastFrame.Infrastructure;

namespace FastFrame.CodeGenerate.Build
{
    /// <summary>
    /// 后台代码生成
    /// </summary>
    public abstract class BaseCShapeCodeBuilder : BaseCodeBuilder
    {
        public BaseCShapeCodeBuilder(string solutionDir, Type baseEntityType) : base(solutionDir, baseEntityType)
        {
        }

        public override bool Forcibly => true;

        /// <summary>
        /// 构造代码信息
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TargetInfo> GetTargetInfoList();

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="targetNames"></param>
        /// <returns></returns>
        public override IEnumerable<BuildTarget> Build(params string[] targetNames)
        {
            var targets = GetTargetInfoList();
            foreach (var target in targets)
            {
                if (targetNames.Length > 0 && !targetNames.Any(v => target.Name.StartsWith(v)))
                    continue;

                yield return ConvertToBuildTarget(target);
            }
        }

        /// <summary>
        /// 转换结构
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual BuildTarget ConvertToBuildTarget(TargetInfo target)
        {
            var write = new StringBuilder();

            /*类型所在命名空间*/
            write.WriteCodeLine($"namespace {target.NamespaceName}", 0);

            /*命名空间开始*/
            write.WriteCodeLine($"{{", 0);

            /*引用命名空间*/
            foreach (var item in target.ImportNames)
            {
                write.WriteCodeLine($"using {item}; ", 1);
            }

            /*空行*/
            write.WriteCodeLine($"", 2);

            /*类说明*/
            write.WriteCodeLine($"/// <summary>", 1);
            write.WriteCodeLine($"/// {target.Summary} ", 1);
            write.WriteCodeLine($"/// </summary>", 1);

            /*类特性*/
            foreach (var attr in target.AttrInfos)
            {
                write.WriteCodeLine($"[{attr.Name}({string.Join(",", attr.Parameters.Select(x => x.ToString()))})]", 1);
            }

            /*类定义*/
            write.WriteCodeLine($"public partial {target.CategoryName} {target.Name}:{string.Join(",", target.BaseNames)}", 1);

            /*类开始*/
            write.WriteCodeLine($"{{", 1);

            /*字段*/
            //write.WriteCodeLine("/*字段*/", 2);
            foreach (var field in target.FieldInfos)
            {
                write.WriteCodeLine($"private readonly {field.TypeName} {field.FieldName};", 2);
            }
            write.WriteCodeLine("", 2);

            /*构造函数*/
            //write.WriteCodeLine("/*构造函数*/", 2);
            if (target.Constructor != null)
            {
                write.WriteCodeLine($"public {target.Name}({string.Join(",", target.Constructor.Parms.Select(x => $"{x.TypeName} {x.DefineName}"))})", 2);
                if (target.Constructor.Super.Any())
                    write.WriteCodeLine($" : base({string.Join(",", target.Constructor.Super)})", 3);

                /*构造函数开始*/
                write.WriteCodeLine($"{{", 2);

                /*构造函数的代码块*/
                foreach (var block in target.Constructor.CodeBlock)
                {
                    write.WriteCodeLine(block, 3);
                }

                /*构造函数结束*/
                write.WriteCodeLine($"}}", 2);
            }


            /*属性列表*/
            //write.WriteCodeLine("/*属性*/", 2);
            foreach (var prop in target.PropInfos)
            {
                /*空行*/
                write.WriteCodeLine($"", 2);

                /*属性说明*/
                write.WriteCodeLine($"/// <summary>", 2);
                write.WriteCodeLine($"/// {prop.Summary} ", 2);
                write.WriteCodeLine($"/// </summary>", 2);

                /*属性特性*/
                foreach (var attr in prop.AttrInfos)
                {
                    write.WriteCodeLine($"[{attr.Name}({string.Join(",", attr.Parameters)})]", 2);
                }

                /*属性定义*/
                write.WriteCodeLine($"public {prop.TypeName} {prop.Name} {{get;set;}}", 2);
            }
            write.WriteCodeLine("", 2);


            /*方法列表*/
            //write.WriteCodeLine("/*方法*/", 2);
            foreach (var method in target.MethodInfos)
            {
                write.WriteCodeLine($"{method.Modifier}{(method.IsOverride ? " override " : " ")}{method.ResultTypeName} {method.MethodName}({string.Join(",", method.Parms.Select(x => $"{x.TypeName} {x.DefineName}"))}) ", 2);
                /*方法开始*/
                write.WriteCodeLine($"{{", 2);

                /*方法代码块*/
                foreach (var block in method.CodeBlock)
                {
                    write.WriteCodeLine(block, 3);
                }

                /*方法结束*/
                write.WriteCodeLine($"}}", 2);
            }
            write.WriteCodeLine("", 2);

            /*类结束*/
            write.WriteCodeLine($"}}", 1);

            /*命名空间结束*/
            write.WriteCodeLine($"}}", 0);

            return new BuildTarget { TargetPath = target.Path, CodeBlock = write.ToString(), Forcibly = this.Forcibly };
        }
    }
}
