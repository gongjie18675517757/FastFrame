using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure;
using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Options;

namespace FastFrame.Application.Privder
{
    /// <summary>
    /// 资源提供者
    /// </summary>
    public class ResourceProvider : IResourceProvider
    {
        private readonly IOptions<ResourceOption> option;

        public ResourceProvider(IOptions<ResourceOption> option)
        {
            this.option = option;
        }
        public async Task<Stream> ReadAsync(string path)
        {
            await Task.CompletedTask;
            path = Path.Combine(option.Value.BasePath, path);
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
            var dirPath = Path.Combine(option.Value.BasePath, relativelyPath);

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            relativelyPath = Path.Combine(relativelyPath, $"{IdGenerate.NetId()}");
            var path = Path.Combine(option.Value.BasePath, relativelyPath);
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                stream.Position = 0;
                await stream.CopyToAsync(fileStream);
            }
            return relativelyPath;
        }

        public string GetFilePath(string relativelyPath)
            => Path.Combine(option.Value.BasePath, relativelyPath);

        public async Task<bool> ExistsAsync(string relativelyPath)
        {
            await Task.CompletedTask;
            var path = Path.Combine(option.Value.BasePath, relativelyPath);
            return File.Exists(path);
        }
    }
}