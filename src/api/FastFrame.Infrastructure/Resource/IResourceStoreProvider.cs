using System.IO;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Resource
{
    /// <summary>
    /// 资源存储
    /// </summary>
    public interface IResourceStoreProvider
    {
        /// <summary>
        /// 尝试保存资源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contentType"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task<IResourceInfo> TrySaveResource(string name, string contentType, Stream stream);

        /// <summary>
        /// 尝试获取资源
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        Task<IResourceStreamInfo> TryGetResource(string resourceId);
    }
}
