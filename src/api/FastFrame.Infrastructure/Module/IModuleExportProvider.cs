namespace FastFrame.Infrastructure.Module
{
    /// <summary>
    /// 模块结构提供者
    /// </summary>
    public interface IModuleExportProvider
    {
        /// <summary>
        /// 获取模块结构
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ModuleStruct GetModuleStruts(string name);
    }
}