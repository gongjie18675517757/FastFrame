using System.IO;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 资源操作提供者
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// 保存资源
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        ValueTask<string> SetResource(Stream stream);

        /// <summary>
        /// 打开资源
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        ValueTask<Stream> GetResource(string path);
    } 
}
