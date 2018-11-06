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
        public async ValueTask<Stream> GetResource(string path)
        {
            await Task.CompletedTask;
            path = Path.Combine(option.Value.BasePath, path);
            if (File.Exists(path))
                return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return null;
        }

        public async ValueTask<string> SetResource(Stream stream)
        {
            var dirPath = Path.Combine(option.Value.BasePath,
                    $"{DateTime.Now.Year}",
                    $"{DateTime.Now.Month}",
                    $"{DateTime.Now.Day}");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var path = Path.Combine(dirPath, $"{IdGenerate.NetId()}");
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                stream.Position = 0;
                await stream.CopyToAsync(fileStream);
            }
            return path;
        }
    }
}