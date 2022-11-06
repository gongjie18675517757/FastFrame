using FastFrame.Infrastructure;
using System.Threading.Tasks;

namespace FastFrame.Application
{
    public interface IPageListService<TDto> : IService where TDto : class
    {
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        Task<IPageList<TDto>> PageListAsync(IPagination pageInfo);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TDto> GetAsync(string id);
    }
}
