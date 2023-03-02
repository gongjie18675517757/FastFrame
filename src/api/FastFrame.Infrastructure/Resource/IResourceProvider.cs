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
        /// <summary>
        /// 保存文件
        /// </summary> 
        Task<string> WriteAsync(Stream stream, string file_name, string content_type);

        /// <summary>
        /// 读取
        /// </summary> 
        Task<IResourceReader> ReadAsync(string relativelyPath);

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


        //OSPlatform.Windows监测运行环境
        public static bool IsWindowRunTime()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        //OSPlatform.Linux运行环境
        public static bool IsLinuxRunTime()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public static string GetLinuxDirectory(string path)
        {
            string pathTemp = Path.Combine(path);
            return pathTemp.Replace("\\", "/");
        }

        public static string GetWindowDirectory(string path)
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
        /// 生成一个绝对路径
        /// </summary>
        /// <returns></returns>
        string MakeDirectoryAbsolutePath();
    }
}
