using FastFrame.Entity;
using System.Threading.Tasks;

namespace FastFrame.Application
{
    /// <summary>
    /// 提供流程单据的摘要
    /// </summary>
    /// <typeparam name="TCheckEntity"></typeparam>
    public interface IWorkFlowDescriptionProvider<TCheckEntity> 
        where TCheckEntity : class, IHaveCheck, new()
    {
        /// <summary>
        /// 返回单据摘要
        /// </summary>
        /// <param name="check_entity"></param>
        /// <returns></returns>
        Task<string> GetDescription(TCheckEntity check_entity);
    }
}
