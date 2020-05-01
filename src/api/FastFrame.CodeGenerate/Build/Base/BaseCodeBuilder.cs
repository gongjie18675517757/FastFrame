using FastFrame.CodeGenerate.Info;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFrame.CodeGenerate.Build
{
    public abstract class BaseCodeBuilder : IBaseCodeBuilder
    {
        /// <summary>
        /// 生成者名称
        /// </summary>
        public abstract string BuildName { get; }

        /// <summary>
        /// 是否强制覆盖
        /// </summary>
        public abstract bool Forcibly { get; }

        /// <summary>
        /// 解决方案目录
        /// </summary>
        public string SolutionDir { get; }

        /// <summary>
        /// XML文档目录
        /// </summary>
        public string XmlDocDir => System.IO.Directory.GetCurrentDirectory();

        /// <summary>
        /// 目标存放路径
        /// </summary>
        public abstract string TargetPath { get; }

        /// <summary>
        /// 基类
        /// </summary>
        public Type BaseEntityType { get; }


        public BaseCodeBuilder(string solutionDir, Type baseEntityType)
        {
            this.SolutionDir = solutionDir;
            this.BaseEntityType = baseEntityType;
        }

        /// <summary>
        /// 获取所有需要生成的类
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Type> GetTypes()
        {
            return BaseEntityType.Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && BaseEntityType.IsAssignableFrom(x));
        }

        public abstract IEnumerable<BuildTarget> Build(params string[] targetNames);
    }
}
