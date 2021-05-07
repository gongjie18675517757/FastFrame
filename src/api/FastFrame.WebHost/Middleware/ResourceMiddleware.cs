using FastFrame.Infrastructure.Interface;
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

namespace FastFrame.WebHost.Middleware
{
    public class ResourceMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Regex resPathRegex;
        private readonly Regex reqPathRegex;
        private readonly FileExtensionContentTypeProvider provider;

        public ResourceMiddleware(RequestDelegate next, IOptions<ResourceOption> options)
        {
            this.next = next;
            resPathRegex = new Regex(options.Value.DownLoadPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            reqPathRegex = new Regex(options.Value.UploadPathRegexText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            provider = new FileExtensionContentTypeProvider();
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

            /*响应下载*/
            if (path.HasValue && resPathRegex.IsMatch(path.Value))
            {
                if (context.Request.Headers.ContainsKey("If-Modified-Since"))
                {
                    context.Response.StatusCode = 304;
                    return;
                }

                var resourceId = resPathRegex.Match(path.Value).Groups[1].Value;
                var resourceName = resPathRegex.Match(path.Value).Groups[2].Value; 

                var resourceStreamInfo = await context.RequestServices.GetService<IResourceStoreProvider>().TryGetResource(resourceId);
                if (resourceStreamInfo == null)
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(new
                    {
                        Code = 404,
                        Message = "资源过期"
                    }.ToJson(), Encoding.UTF8);
                    return;
                }

                provider.TryGetContentType(Path.GetExtension(resourceStreamInfo.Name), out var contentType);
                context.Response.StatusCode = 200;
                context.Response.ContentType = contentType ?? "application/octet-stream";

                //context.Response.Headers.TryAdd("Content-Disposition", $"attachment;filename={WebUtility.UrlEncode(resourceStreamInfo.Name)}");
                context.Response.Headers.TryAdd("cache-control", new[] { "public,max-age=31536000" });
                context.Response.Headers.TryAdd("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") });
                context.Response.Headers.TryAdd("Last-Modified", DateTime.Now.ToString());
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
                await context.Response.WriteAsync(resultList.ToJson(), Encoding.UTF8);
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
        /// 响应上传的正则
        /// 如：/api/resource/upload
        /// </summary>
        public string UploadPathRegexText { get; set; }
    }
}
