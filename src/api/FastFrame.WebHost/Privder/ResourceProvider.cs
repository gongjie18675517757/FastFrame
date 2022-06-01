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

        public ResourceProvider(IOptionsMonitor<ResourceOption> option)
        {

            this.option = option;
        }
        public async Task<Stream> ReadAsync(string path)
        {
            await Task.CompletedTask;
            path = IResourceProvider.GetRuntimeDirectory(Path.Combine(option.CurrentValue.BasePath, path));

            if (File.Exists(path))
                return File.OpenRead(path);
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
            using (var fileStream = File.Create(path))
            {
                stream.Position = 0;
                await stream.CopyToAsync(fileStream);

                await fileStream.FlushAsync();
            }

            return relativelyPath;
        }

        public string GetFilePath(string relativelyPath)
            => IResourceProvider.GetRuntimeDirectory(Path.Combine(option.CurrentValue.BasePath, relativelyPath));

        public async Task<bool> ExistsAsync(string relativelyPath)
        {
            await Task.CompletedTask;
            var path = IResourceProvider.GetRuntimeDirectory(Path.Combine(option.CurrentValue.BasePath, relativelyPath));
            return File.Exists(path);
        }
    } 
}