using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WxModule
{
    public interface IWxHandle
    {
        Task<string> PayCallback(WxResult<PayCallbackData> wxResult);
    }

    public class WxMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string path;

        public WxMiddleware(RequestDelegate next, string path)
        {
            _next = next;
            this.path = path;
        }

        private async Task<string> readBody(Stream stream)
        {
            var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
        private string getValue(XmlDocument doc, string name)
        {
            var text = doc.SelectSingleNode($"//{name}")?.Value?.Trim() ?? "";
            return text.Substring(9, text.Length - 8 - 4);
        }
        private async Task<WxResult<T>> readAsXmlFromBody<T>(Stream stream) where T : class, new()
        {
            var result = new WxResult<T>();
            var bodyString = await readBody(stream);
            var doc = new XmlDocument();
            doc.LoadXml(bodyString);
            
            result.return_code = getValue(doc, "return_code");
            result.return_msg= getValue(doc, "return_msg");

            if(result.return_code== "SUCCESS")
            {

            }

            return result;
        }

        public async Task Invoke(HttpContext context, IOptions<WxConfig> options, IWxHandle wxHandle)
        {
            var config = options.Value;
            if (context.Request.Path.HasValue)
            {
                if (context.Request.Method == "POST" && context.Request.Path.Value == config.PayCallbackPath)
                {
                    var bodyContent = readBody(context.Request.Body);
                    await wxHandle.PayCallback();
                }
            }
            else
            {
                await this._next(context);
            }
        }
    }

    public class WxHandleService
    {

    }

    public class WxConfig
    {
        /// <summary>
        /// 支付回调路径
        /// </summary>
        public string PayCallbackPath { get; set; }

    }

    public static class WxHandleExtensions
    {
        public static IApplicationBuilder UseWxHandle(this IApplicationBuilder app)
        {
            return app.UseMiddleware<WxMiddleware>();
        }

        public static IServiceCollection AddWxHandle(this IServiceCollection services, IConfigurationSection configuration)
        {
            services.Configure<WxConfig>(configuration);
            return services;
        }
    }
}
