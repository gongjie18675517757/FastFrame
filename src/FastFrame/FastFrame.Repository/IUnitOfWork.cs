using System.Threading.Tasks;

namespace FastFrame.Repository
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        /// 异步提交
        /// </summary>
        /// <returns></returns>
        Task<int> CommmitAsync();
    }
}
