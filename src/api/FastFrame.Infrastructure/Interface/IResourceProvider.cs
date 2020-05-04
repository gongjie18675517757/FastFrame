using System.IO;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 附件资源操作提供者
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// 保存
        /// </summary> 
        Task<string> WriteAsync(Stream stream);

        /// <summary>
        /// 读取
        /// </summary> 
        Task<Stream> ReadAsync(string relativelyPath);

        /// <summary>
        /// 检查是否存在
        /// </summary> 
        Task<bool> ExistsAsync(string relativelyPath);

        /// <summary>
        /// 获取实际文件路径
        /// </summary>
        /// <param name="relativelyPath"></param>
        /// <returns></returns>
        string GetFilePath(string relativelyPath);
    }
}
