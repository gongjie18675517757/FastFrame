using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading.Tasks;

namespace FastFrame.Repository
{
    public interface IUnitOfWork
    {
        ///// <summary>
        ///// 事务是否开启中
        ///// </summary>
        //bool IsTransactionOpening { get; }

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        Task<int> CommmitAsync();

        ///// <summary>
        ///// 提交事务
        ///// </summary>
        ///// <returns></returns>
        //int Commmit();

        ///// <summary>
        ///// 开启异步事务
        ///// </summary>
        ///// <returns></returns>
        //Task BeginTransactionAsync(IsolationLevel level);

        ///// <summary>
        ///// 开启事务
        ///// </summary>
        ///// <returns></returns>
        //void BeginTransaction(IsolationLevel level);
    }
}
