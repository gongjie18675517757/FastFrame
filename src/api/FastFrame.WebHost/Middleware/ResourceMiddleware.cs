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
        private readonly FileExtensionContentTypeProvider provider;

        public ResourceMiddleware(RequestDelegate next, IOptions<ResourceOption> options)
        {
            this.next = next;
            downloadPathRegex = new Regex(options.Value.DownLoadPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            thumbnailPathRegex = new Regex(options.Value.ThumbnailPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            reqPathRegex = new Regex(options.Value.UploadPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            provider = new FileExtensionContentTypeProvider();
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

            /*响应下载*/
            if (path.HasValue && (downloadPathRegex.IsMatch(path.Value) || thumbnailPathRegex.IsMatch(path.Value)))
            {
                if (context.Request.Headers.ContainsKey("If-Modified-Since"))
                {
                    context.Response.StatusCode = 304;
                    return;
                }

                var resourceId = string.Empty;
                if (downloadPathRegex.IsMatch(path.Value))
                    resourceId = downloadPathRegex.Match(path.Value).Groups[1].Value;
                else if (thumbnailPathRegex.IsMatch(path.Value))
                    resourceId = thumbnailPathRegex.Match(path.Value).Groups[1].Value;

                //var resourceName = downloadPathRegex.Match(path.Value).Groups[2].Value;

                var resourceStreamInfo = await context.RequestServices.GetService<IResourceStoreProvider>().TryGetResource(resourceId);
                if (resourceStreamInfo == null)
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteJsonAsync(new
                    {
                        Code = 404,
                        Message = "资源过期"
                    });
                    return;
                }

                provider.TryGetContentType(Path.GetExtension(resourceStreamInfo.Name), out var contentType);
                contentType ??= resourceStreamInfo.ContentType;

                /*响应缩略图*/
                if (contentType != null && contentType.StartsWith("image") && thumbnailPathRegex.IsMatch(path.Value))
                {
                    int width = 300, height = 300;
                    _ = context.Request.Query.TryGetValue("width", out var widthText) && int.TryParse(widthText, out width);
                    _ = context.Request.Query.TryGetValue("height", out var heightText) && int.TryParse(heightText, out height);
                    var newStream = ImageExtended.GetPicThumbnail(resourceStreamInfo.ResourceBlobStream, height, width, 50);

                    if (newStream != null)
                        resourceStreamInfo.ReplaceBlobStream(newStream);
                }

                context.Response.StatusCode = 200;
                context.Response.ContentType = contentType ?? "application/octet-stream";

                if (context.Request.Query.TryGetValue("has_down", out var _))
                    context.Response.Headers.TryAdd("Content-Disposition", $"attachment;filename={WebUtility.UrlEncode(resourceStreamInfo.Name)}");

                context.Response.Headers.TryAdd("cache-control", new[] { "public,max-age=31536000" });
                context.Response.Headers.TryAdd("Expires", new[] { resourceStreamInfo.ModifyTime.AddYears(10).ToString("R") });
                context.Response.Headers.TryAdd("Last-Modified", resourceStreamInfo.ModifyTime.ToString("R"));
                context.Response.Headers.TryAdd("ETag", resourceId);

                context.Response.ContentLength = resourceStreamInfo.ResourceBlobStream.Length;
                resourceStreamInfo.ResourceBlobStream.Position = 0;
                await resourceStreamInfo.ResourceBlobStream.CopyToAsync(context.Response.Body);
                
                await context.Response.Body.FlushAsync();
                return;
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

            await next(context);
        }
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
    }
}
