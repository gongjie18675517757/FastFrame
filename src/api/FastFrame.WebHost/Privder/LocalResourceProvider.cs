using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Resource;
using FastFrame.WebHost.Middleware;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
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
        /// 生成一个目录(相对路径)
        /// </summary>
        /// <returns></returns>
        public string MakeDirectoryAsRelativelyPath()
        {
            /*目录的相对路径*/
            var dir_relativelyPath = Path.Combine($"{DateTime.Now.Year}",
                                              $"{DateTime.Now.Month}",
                                              $"{DateTime.Now.Day}",
                                              IdGenerate.NetLongId().ToString());

            return dir_relativelyPath;
        }

        /// <summary>
        /// 生成一个绝对路径
        /// </summary>
        /// <returns></returns>
        public string MakeDirectoryAbsolutePath()
        {
            var path = IResourceProvider.GetRuntimeDirectory(Path.Combine(option.CurrentValue.BasePath, MakeDirectoryAsRelativelyPath()));

            var directoryInfo = new DirectoryInfo(path);

            directoryInfo.Create();
            return directoryInfo.FullName;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="input_file_name"></param>
        /// <param name="content_type"></param>
        /// <returns></returns>
        public async Task<string> WriteAsync(Stream stream, string input_file_name, string content_type)
        {
            var store_file_name = input_file_name;
            store_file_name = store_file_name.CheckIsNullOrWhiteSpace(Path.GetRandomFileName());
            var file_extension = Path.GetExtension(store_file_name);
            file_extension = file_extension.CheckIsNullOrWhiteSpace(".obj"); 

            /*判断是否对文件名混淆*/
            var has_encryption = true;

            /*指定的文件类型不混淆*/
            if (has_encryption && !option.CurrentValue.UnwantedEncryptionFileNameRegex.IsNullOrWhiteSpace())
                has_encryption = !Regex.IsMatch(file_extension, option.CurrentValue.UnwantedEncryptionFileNameRegex);  

            if (has_encryption)
                store_file_name = Path.GetRandomFileName();

            /*文件的相对路径*/
            var file_relativelyPath = Path.Combine(MakeDirectoryAsRelativelyPath(), store_file_name);

            var base_path = option.CurrentValue.BasePath;

            /*文件完整路径*/
            var file_full_path = IResourceProvider.GetRuntimeDirectory(Path.Combine(base_path, file_relativelyPath));

            var file_info = new FileInfo(file_full_path);

            if (!file_info.Directory.Exists)
                file_info.Directory.Create();

            using (var fileStream = file_info.Create())
            {
                stream.Position = 0;
                await stream.CopyToAsync(fileStream);

                await fileStream.FlushAsync();
            }

            return file_relativelyPath;
        }


        /// <summary>
        /// 根据相对路径，返回完整路径
        /// </summary>
        /// <param name="relativelyPath"></param>
        /// <returns></returns>
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
                file_Name = new FileInfo(file_name).FullName;
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