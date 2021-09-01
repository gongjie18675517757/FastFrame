using FastFrame.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Repository
{
    public interface IRepository<T> : IQueryRepository<T>, IUnitOfWork where T : class, IEntity
    {
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(string id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 获取查询表达式
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Queryable { get; }

        /// <summary>
        /// 执行SQL   
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<int> ExecSqlAsync(string sql);

        /// <summary>
        /// 获取数据库表名
        /// </summary> 
        string GetDbTableName(); 

        /// <summary>
        /// 获取数据库列名
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        string GetDbColumnName(string propName); 
    }
}
