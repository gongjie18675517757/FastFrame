using FastFrame.Entity;
using System.Threading.Tasks;

namespace FastFrame.Application
{
    /// <summary>
    /// 自动编号服务
    /// </summary>
    public interface IAutoNumberService
    {
        /// <summary>
        /// 生成编号
        /// </summary> 
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task MakeNumberAsync(params IHaveNumber[] entitys);
    }
}
