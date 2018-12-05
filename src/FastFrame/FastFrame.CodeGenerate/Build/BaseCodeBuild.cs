using FastFrame.CodeGenerate.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastFrame.CodeGenerate.Build
{
    public abstract class BaseCodeBuild
    {
        public abstract string ProductName { get; }
        /// <summary>
        /// 解决方案目录
        /// </summary>
        public string SolutionDir { get; }

        /// <summary>
        /// XML文档目录
        /// </summary>
        public string XmlDocDir => $"{SolutionDir}\\Lib";

        /// <summary>
        /// 目标存放路径
        /// </summary>
        public abstract string TargetPath { get; }

        /// <summary>
        /// 基类
        /// </summary>
        public Type BaseEntityType { get; }

        public BaseCodeBuild(string solutionDir, Type baseEntityType)
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

        /// <summary>
        /// 构造代码信息
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TargetInfo> BuildCodeInfo(string typeName);
    }
}
