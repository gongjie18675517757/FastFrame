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
        Task<string> SetResource(Stream stream);

        /// <summary>
        /// 打开资源
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<Stream> GetResource(string path);

        /// <summary>
        /// 获取实际文件路径
        /// </summary>
        /// <param name="relativelyPath"></param>
        /// <returns></returns>
        string GetFilePath(string relativelyPath);
    } 
}
