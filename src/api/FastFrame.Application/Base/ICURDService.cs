using System.Threading.Tasks;

namespace FastFrame.Application
{
    /// <summary>
    /// 服务接口
    /// </summary> 
    /// <typeparam name="TDto"></typeparam>
    public interface ICURDService<TDto> : IVerifyUniqueService, IService, IPageListService<TDto>
        where TDto : class, IDto, new()
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> AddAsync(TDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteAsync(params string[] ids);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(TDto input);

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TDto> GetAsync(string id);
    }
}
