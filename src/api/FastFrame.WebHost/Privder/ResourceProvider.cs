using FastFrame.Infrastructure.Resource;
using FastFrame.WebHost.Middleware;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    /// <summary>
    /// 资源提供者
    /// </summary>
    public class ResourceProvider : IResourceProvider
    {
        private readonly IOptionsMonitor<ResourceOption> option;

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

        public ResourceProvider(IOptionsMonitor<ResourceOption> option)
        {

            this.option = option;
        }
        public async Task<Stream> ReadAsync(string path)
        {
            await Task.CompletedTask;
            path = GetRuntimeDirectory(Path.Combine(option.CurrentValue.BasePath, path));

            if (File.Exists(path))
                return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return null;
        }

        public async Task<string> WriteAsync(Stream stream)
        {
            var relativelyPath = Path.Combine(
                    $"{DateTime.Now.Year}",
                    $"{DateTime.Now.Month}",
                    $"{DateTime.Now.Day}");
            var dirPath = Path.Combine(option.CurrentValue.BasePath, relativelyPath);

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            relativelyPath = Path.Combine(relativelyPath, Path.GetRandomFileName());
            var path = Path.Combine(option.CurrentValue.BasePath, relativelyPath);
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                stream.Position = 0;
                await stream.CopyToAsync(fileStream);

                await fileStream.FlushAsync();
            }

            return relativelyPath;
        }

        public string GetFilePath(string relativelyPath)
            => GetRuntimeDirectory(Path.Combine(option.CurrentValue.BasePath, relativelyPath));

        public async Task<bool> ExistsAsync(string relativelyPath)
        {
            await Task.CompletedTask;
            var path = GetRuntimeDirectory(Path.Combine(option.CurrentValue.BasePath, relativelyPath));
            return File.Exists(path);
        }
    }


}