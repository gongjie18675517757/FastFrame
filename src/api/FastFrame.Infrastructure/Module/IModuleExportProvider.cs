using System.Collections.Generic;

namespace FastFrame.Infrastructure.Module
{
    /// <summary>
    /// 模块结构提供者
    /// </summary>
    public interface IModuleExportProvider
    {
        /// <summary>
        /// 注册模块
        /// </summary>
        /// <param name="moduleStructs"></param>
        void RegisterModule(params ModuleStruct[] moduleStructs);

        /// <summary>
        /// 获取模块结构
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ModuleStruct GetModuleStruts(string name);

        /// <summary>
        /// 需要审核的模块
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<string, string>> HaveCheckModuleList();
    }
}