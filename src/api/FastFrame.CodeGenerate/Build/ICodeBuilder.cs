using FastFrame.CodeGenerate.Info;
using System.Collections.Generic;

namespace FastFrame.CodeGenerate.Build
{
    public interface IBaseCodeBuilder
    {
        /// <summary>
        /// 生成者名称
        /// </summary>
        string BuildName { get; }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="targetTypeNames"></param>
        /// <returns></returns>
        IEnumerable<BuildTarget> Build(params string[] targetTypeNames);

        /// <summary>
        /// 强制覆盖
        /// </summary>
        bool Forcibly { get; }
    }
}