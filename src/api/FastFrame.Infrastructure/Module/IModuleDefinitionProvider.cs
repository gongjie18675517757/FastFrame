using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Module
{
    /// <summary>
    /// 模块定义提供者
    /// </summary>
    public interface IModuleDefinitionProvider
    {
        /// <summary>
        /// 定义模块
        /// </summary>
        /// <returns></returns>
        Task<ModuleStruct> DefinitionModuleAsync();
    }
}