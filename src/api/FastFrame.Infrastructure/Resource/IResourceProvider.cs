using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Resource
{
    /// <summary>
    /// 附件资源操作提供者
    /// </summary>
    public interface IResourceProvider
    {
        //OSPlatform.Windows监测运行环境
        static bool IsWindowRunTime()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        //OSPlatform.Linux运行环境
        static bool IsLinuxRunTime()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        static string GetLinuxDirectory(string path)
        {
            string pathTemp = Path.Combine(path);
            return pathTemp.Replace("\\", "/");
        }

        static string GetWindowDirectory(string path)
        {
            string pathTemp = Path.Combine(path);
            return pathTemp.Replace("/", "\\");
        }

        /// <summary>
        /// 目录转换
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetRuntimeDirectory(string path)
        {
            //ForLinux
            if (IsLinuxRunTime())
                return GetLinuxDirectory(path);
            //ForWindows
            if (IsWindowRunTime())
                return GetWindowDirectory(path);

            return path;
        }

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
