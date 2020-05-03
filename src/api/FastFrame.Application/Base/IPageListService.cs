using FastFrame.Infrastructure;
using System.Threading.Tasks;

namespace FastFrame.Application
{
    public interface IPageListService<TDto> : IService
        where TDto : class, IDto, new()
    {
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        Task<PageList<TDto>> PageListAsync(Pagination pageInfo);
    }
}
