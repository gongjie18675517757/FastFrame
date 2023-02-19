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

namespace FastFrame.WebHost.Middleware
{
    public class ResourceMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Regex downloadPathRegex;
        private readonly Regex thumbnailPathRegex;
        private readonly Regex reqPathRegex;
        private readonly Regex reqBigPathRegex;
        private readonly FileExtensionContentTypeProvider provider;

        public ResourceMiddleware(RequestDelegate next, IOptions<ResourceOption> options)
        {
            this.next = next;
            downloadPathRegex = new Regex(options.Value.DownLoadPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            thumbnailPathRegex = new Regex(options.Value.ThumbnailPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            reqPathRegex = new Regex(options.Value.UploadPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            reqBigPathRegex = new Regex(options.Value.UploadBigFilePathText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            provider = new FileExtensionContentTypeProvider();
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

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
                    if (context.Request.Query.TryGetValue("has_down", out var _))
                        context.Response.Headers.TryAdd("Content-Disposition", $"attachment;filename={WebUtility.UrlEncode(resource_name)}");

                    if (reader.TryGetRelativelyPath(out var relatively_path))
                        context.Request.Path = $"/files/{relatively_path.Replace("\\", "/")}";
                }

                /*匹配文件名*/
                if (resource_name.IsNullOrWhiteSpace())
                    resource_name = resourceStreamInfo.Name;

                /*匹配内容类型*/
                if (!provider.TryGetContentType(Path.GetExtension(resource_name), out var contentType))
                    contentType = resourceStreamInfo.ContentType;
                if (contentType.IsNullOrWhiteSpace())
                    contentType = "application/octet-stream";


                /*响应缩略图*/
                if (has_thumbnail && contentType.StartsWith("image"))
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = contentType;
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

            }

            await next(context);
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
    }
}
