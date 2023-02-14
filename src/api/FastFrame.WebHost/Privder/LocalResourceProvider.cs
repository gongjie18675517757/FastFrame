using FastFrame.Infrastructure;
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
    /// 资源提供者(使用本地文件目录)
    /// </summary>
    public class LocalResourceProvider : IResourceProvider
    {
        private readonly IOptionsMonitor<ResourceOption> option;

        public LocalResourceProvider(IOptionsMonitor<ResourceOption> option)
        {
            this.option = option;
        }



        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="relativelyPath"></param>
        /// <returns></returns>
        public async Task<IResourceReader> ReadAsync(string relativelyPath)
        {
            await Task.CompletedTask;
            var path = GetFilePath(relativelyPath);
            return new LocalResourceReader(path, relativelyPath);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="input_file_name"></param>
        /// <returns></returns>
        public async Task<string> WriteAsync(Stream stream, string input_file_name)
        {
            var relativelyPath = Path.Combine(
                    $"{DateTime.Now.Year}",
                    $"{DateTime.Now.Month}",
                    $"{DateTime.Now.Day}");
            var dirPath = Path.Combine(option.CurrentValue.BasePath, relativelyPath);

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var file_name = $"{IdGenerate.NetLongId()}.{Path.GetExtension(input_file_name)}";
            relativelyPath = Path.Combine(relativelyPath, file_name);

            var full_file_name = Path.Combine(option.CurrentValue.BasePath, relativelyPath);

            using (var fileStream = File.Create(full_file_name))
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
            var file_name = GetFilePath(relativelyPath);
            var resourceReader = await ReadAsync(file_name);
            return resourceReader.Exists();
        }

        private class LocalResourceReader : IResourceReader
        {
            private readonly string file_Name;
            private readonly string relativelyPath;

            public LocalResourceReader(string file_name, string relativelyPath)
            {
                file_Name = file_name;
                this.relativelyPath = relativelyPath;
            }

            public bool TryGetStream(out Stream stream)
            {
                if (Exists())
                {
                    stream = File.OpenRead(file_Name);
                    return stream.CanRead;
                }

                stream = null;
                return false;
            }

            public bool TryGetLocalFileFullName(out string file_name)
            {
                file_name = file_Name;
                return true;
            }

            public bool Exists()
            {
                return File.Exists(file_Name);
            }

            public bool TryGetRelativelyPath(out string relativelyPath)
            {
                relativelyPath = this.relativelyPath;
                return File.Exists(file_Name);
            }
        }
    }
}