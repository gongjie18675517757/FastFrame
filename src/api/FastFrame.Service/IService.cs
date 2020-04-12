using FastFrame.Dto;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using System;
using System.Threading.Tasks;

namespace FastFrame.Service
{
    /// <summary>
    ///  服务接口
    /// </summary>
    public interface IService
    {
    }

    public interface IVerifyUniqueService
    {
        /// <summary>
        /// 验证唯一性
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> VerifyUnique(UniqueInput uniqueInput);
    }

    /// <summary>
    /// 服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public interface IService<TEntity, TDto> : IVerifyUniqueService, IService
        where TEntity : class, IEntity, new()
        where TDto : class, IDto<TEntity>, new()
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
        /// <param name="id"></param>
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

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        Task<PageList<TDto>> GetListAsync(Pagination pageInfo);
    }
}
