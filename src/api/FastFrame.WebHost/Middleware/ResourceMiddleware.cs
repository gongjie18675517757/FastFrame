using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FastFrame.Infrastructure;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Net;
using Microsoft.AspNetCore.Components.Routing;
using FastFrame.Infrastructure.Resource;
using System.IO.Pipes;
using FastFrame.Infrastructure.Lock;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Mime;

namespace FastFrame.WebHost.Middleware
{
    public class ResourceMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILockFacatory lockFacatory;
        private readonly Regex downloadPathRegex;
        private readonly Regex thumbnailPathRegex;
        private readonly Regex reqPathRegex;
        private readonly Regex reqBigPathRegex;
        private readonly FileExtensionContentTypeProvider provider;

        public ResourceMiddleware(RequestDelegate next, IOptions<ResourceOption> options, ILockFacatory lockFacatory)
        {
            this.next = next;
            this.lockFacatory = lockFacatory;
            downloadPathRegex = new Regex(options.Value.DownLoadPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            thumbnailPathRegex = new Regex(options.Value.ThumbnailPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            reqPathRegex = new Regex(options.Value.UploadPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            reqBigPathRegex = new Regex(options.Value.UploadBigFilePathText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            provider = new FileExtensionContentTypeProvider();
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            var contentType = string.Empty;
            /*判断是否下载的请求*/
            bool has_download = downloadPathRegex.IsMatch(path.Value),

                /*判断是否请求缩略图的请求*/
                has_thumbnail = thumbnailPathRegex.IsMatch(path.Value);

            /*响应下载*/
            if (path.HasValue && (has_download || has_thumbnail))
            {
                if (context.Request.Headers.ContainsKey("If-Modified-Since"))
                {
                    context.Response.StatusCode = 304;
                    return;
                }

                GroupCollection groups;
                if (has_download)
                    groups = downloadPathRegex.Match(path.Value).Groups;
                else
                    groups = thumbnailPathRegex.Match(path.Value).Groups;

                var resource_id = groups["resource_id"].Value;
                var resource_name = groups["resource_name"].Value;

                var resourceStreamInfo = await context.RequestServices.GetService<IResourceStoreProvider>().TryGetResourceReader(resource_id);
                var reader = resourceStreamInfo.GetResourceReader();
                if (!reader.Exists())
                {
                    await NotFound(context);
                    return;
                }

                /*响应文件下载*/
                if (has_download)
                {
                    if (context.Request.Query.TryGetValue("has_down", out var _) || resource_name.IsNullOrWhiteSpace())
                    {
                        context.Response.Headers
                            .TryAdd("Content-Disposition",
                            $"attachment;filename={WebUtility.UrlEncode(resource_name.CheckIsNullOrWhiteSpace(resourceStreamInfo.Name))}");
                    }

                    if (reader.TryGetRelativelyPath(out var relatively_path))
                    {
                        context.Request.Path = $"/files/{relatively_path.Replace("\\", "/")}";
                    }
                }

                /*匹配文件名*/
                if (resource_name.IsNullOrWhiteSpace())
                    resource_name = resourceStreamInfo.Name;

                /*匹配内容类型*/
                if (!provider.TryGetContentType(Path.GetExtension(resource_name), out contentType))
                    contentType = resourceStreamInfo.ContentType;
                if (contentType.IsNullOrWhiteSpace())
                    contentType = "application/octet-stream";

                context.Response.ContentType = contentType;

                /*响应缩略图*/
                if (has_thumbnail && contentType.StartsWith("image"))
                {
                    context.Response.StatusCode = 200;

                    context.Response.Headers.TryAdd("cache-control", new[] { "public,max-age=31536000" });
                    context.Response.Headers.TryAdd("Expires", new[] { resourceStreamInfo.ModifyTime.AddYears(10).ToString("R") });
                    context.Response.Headers.TryAdd("Last-Modified", resourceStreamInfo.ModifyTime.ToString("R"));
                    context.Response.Headers.TryAdd("ETag", resource_id);

                    int width = 300, height = 300;
                    _ = context.Request.Query.TryGetValue("width", out var widthText) && int.TryParse(widthText, out width);
                    _ = context.Request.Query.TryGetValue("height", out var heightText) && int.TryParse(heightText, out height);

                    if (!reader.TryGetStream(out var stream))
                    {
                        await NotFound(context);
                        return;
                    }

                    using (stream)
                    {
                        var newStream = ImageExtended.GetPicThumbnail(stream, height, width, 50);
                        newStream.Position = 0;
                        await newStream.CopyToAsync(context.Response.Body);
                        return;
                    }
                }
            }

            /*响应上传*/
            if (path.HasValue && reqPathRegex.IsMatch(path.Value))
            {
                var resourceStoreProvider = context.RequestServices.GetService<IResourceStoreProvider>();
                var resultList = new List<IResourceInfo>();
                foreach (var file in context.Request.Form.Files)
                {
                    var stream = file.OpenReadStream();
                    var resourceInfo = await resourceStoreProvider.TrySaveResource(file.Name, file.ContentType, stream);
                    resultList.Add(resourceInfo);
                }
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                await context.Response.WriteJsonAsync(resultList.ToJson());
                return;
            }

            /*响应大文件上传*/
            if (path.HasValue && reqBigPathRegex.IsMatch(path.Value))
            {
                /*文件的id*/
                var file_id = reqBigPathRegex.Match(path.Value).Groups["file_id"].Value;

                /*提交文件元数据*/
                if (context.Request.Method == HttpMethods.Post && file_id.IsNullOrWhiteSpace())
                {
                    var fileMetadata = await context.Request.ReadFromJsonAsync<FileMetadata>();
                    fileMetadata.ComplateIndexs = new bool[fileMetadata.TotalChunkFiles];

                    /*最终文件*/
                    var resourceStoreProvider = context.RequestServices.GetService<IResourceStoreProvider>();
                    var resourceInfo = await resourceStoreProvider
                        .TrySaveEmptyResource(fileMetadata.Name, fileMetadata.ContentType, fileMetadata.Size, fileMetadata.FileMD5);

                    /*没有命中缓存时*/
                    if (resourceInfo.HasUpload)
                    {
                        /*写入元数据*/
                        var resourceRedear = await resourceStoreProvider.TryGetResourceReader(resourceInfo.Id);
                        resourceRedear.GetResourceReader().TryGetLocalFileFullName(out var file_path);
                        var dir_path = new FileInfo(file_path).Directory.FullName;
                        var metadata_file = new FileInfo(Path.Combine(dir_path, "metadata.json"));
                        using var streamWriter = metadata_file.CreateText();
                        await streamWriter.WriteLineAsync(fileMetadata.ToJson());

                        /*写入所有分片文件*/
                        for (int i = 0; i < fileMetadata.TotalChunkFiles; i++)
                        {
                            var chunk_name = Path.Combine(dir_path, $"{i}.chunk");
                            using var _ = File.Create(chunk_name);
                        }
                    }


                    /*响应*/
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteJsonAsync(resourceInfo.ToJson());
                    return;
                }

                /*提交分片文件*/
                if (context.Request.Method == HttpMethods.Post && !file_id.IsNullOrWhiteSpace())
                {
                    var resourceStreamInfo = await context.RequestServices.GetService<IResourceStoreProvider>().TryGetResourceReader(file_id);

                    if (!resourceStreamInfo.GetResourceReader().TryGetLocalFileFullName(out var file_name))
                    {
                        await NotFound(context);
                        return;
                    }

                    var dir_path = new FileInfo(file_name).Directory.FullName;

                    if (!context.Request.Form.TryGetValue("file_index", out var val) || !int.TryParse(val, out var file_index))
                    {
                        await BidRequest(context, "文件索引不正确");
                        return;
                    }

                    if (context.Request.Form.Files == null || !context.Request.Form.Files.Any())
                    {
                        await BidRequest(context, "未接收到有效文件");
                        return;
                    }

                    var formFile = context.Request.Form.Files["chunk_file"];
                    var file_path = Path.Combine(dir_path, $"{file_index}.chunk");

                    var file = new FileInfo(file_path);
                    using var targetStream = file.OpenWrite();
                    using var sourceStream = formFile.OpenReadStream();
                    await sourceStream.CopyToAsync(targetStream);
                    await sourceStream.FlushAsync();
                    await targetStream.FlushAsync();
                    targetStream.Close();

                    /*更新文件各分片的上传状态*/
                    await TryExistsComplateAndMergeFiles(file_id, dir_path, file_index, file_name);

                    context.Response.StatusCode = 200;
                    return;
                }
            }

            await next(context);

            //if (contentType != "application/octet-stream" && context.Response.ContentType == "application/octet-stream")
            //{
            //    context.Response.Headers.ContentType = contentType;
            //}
        }


        /// <summary>
        /// 尝试检查文件是否完整并合并分片文件(如果完整)
        /// </summary>
        /// <param name="file_id"></param>
        /// <param name="dir_path"></param>
        /// <param name="file_index"></param>
        /// <param name="dest_file_name"></param>
        /// <returns></returns>
        private async Task TryExistsComplateAndMergeFiles(string file_id, string dir_path, int file_index, string dest_file_name)
        {
            FileMetadata metadata;
            ILockHolder lockHolder;
            var metadata_file = new FileInfo(Path.Combine(dir_path, "metadata.json"));

            /*循环不断的获取锁*/
            while (true)
            {
                lockHolder = await lockFacatory.TryCreateLockAsync(file_id, TimeSpan.FromSeconds(2));
                if (lockHolder != null)
                    break;
            }

            /*更新完成情况*/
            using (lockHolder)
            {
                var json = await File.ReadAllTextAsync(metadata_file.FullName);
                metadata = json.ToObject<FileMetadata>();
                metadata.ComplateIndexs[file_index] = true;
                await File.WriteAllTextAsync(metadata_file.FullName, metadata.ToJson());
            }

            /*检查是不是全部的分片都上传了*/
            if (metadata.ComplateIndexs.Any(v => !v))
                return;

            /*合并分片文件到最终文件*/
            var source_files = metadata_file.Directory.GetFiles("*.chunk");
            var buffer = new byte[1024];
            var read_bytes = 0;

            using var write_stream = File.OpenWrite(dest_file_name);
            foreach (var file in source_files)
            {
                using var read_stream = file.OpenRead();
                while (true)
                {
                    read_bytes = await read_stream.ReadAsync(buffer.AsMemory(0, 1024));
                    if (read_bytes == 0)
                        break;

                    await write_stream.WriteAsync(buffer.AsMemory(0, read_bytes));
                }
            }

            foreach (var file in source_files)
                file.Delete();
        }

        /// <summary>
        /// 响应404
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task NotFound(HttpContext context)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";

            await context.Response.WriteJsonAsync(new
            {
                Code = 404,
                Message = "资源过期"
            });
        }

        /// <summary>
        /// 响应400
        /// </summary>
        /// <param name="context"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static async Task BidRequest(HttpContext context, string msg)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            await context.Response.WriteJsonAsync(new
            {
                Code = 400,
                Message = msg
            });
        }
    }


    public class BidFileUploadInput
    {

    }



    /// <summary>
    /// 资源配置
    /// </summary>
    public class ResourceOption
    {
        /// <summary>
        /// 父路径
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// 响应下载的正则
        /// 例：/api/resource/download/([^/]+)
        /// </summary>
        public string DownLoadPathRegexText { get; set; }

        /// <summary>
        /// 响应获取缩略图的正则
        /// </summary>
        public string ThumbnailPathRegexText { get; set; }

        /// <summary>
        /// 响应上传的正则
        /// 如：/api/resource/upload
        /// </summary>
        public string UploadPathRegexText { get; set; }

        /// <summary>
        /// 响应大文件上传的正则
        /// 如:/api/resources/big_upload/{aabbccddefg}
        /// </summary>
        public string UploadBigFilePathText { get; set; }

        /// <summary>
        /// 不需要编码的文件类型
        /// </summary>
        public string UnwantedEncryptionFileNameRegex { get; set; }
    }
}
