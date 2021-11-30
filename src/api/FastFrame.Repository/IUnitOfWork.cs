using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading.Tasks;

namespace FastFrame.Repository
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 添加事务提交后的事件
        /// 注意:此处的回调中一定要处理异常
        /// </summary>
        /// <param name="func">回调参数</param>
        void AddCommitEventListen(Func<IServiceProvider, Task> func);

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        Task<int> CommmitAsync(); 
    }
}
