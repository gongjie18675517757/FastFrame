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
        /// <summary>
        /// 验证唯一性
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        ValueTask<bool> VerifyUnique(string id,string propName, string value);
    }

    /// <summary>
    /// 服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public interface IService<TEntity, TDto> : IService
        where TEntity : class, IEntity, new()
        where TDto : class, IDto<TEntity>, new()
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ValueTask<TDto> AddAsync(TDto input); 

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
        ValueTask<TDto> UpdateAsync(TDto input);

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<TDto> GetAsync(string id);

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        ValueTask<PageList<TDto>> GetListAsync(PagePara pageInfo);  
    }  
}
