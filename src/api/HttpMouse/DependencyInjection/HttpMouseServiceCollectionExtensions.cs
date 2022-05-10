using HttpMouse;
using HttpMouse.Implementions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Transforms;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// HttpMouse的服务注册扩展
    /// </summary>
    public static class HttpMouseServiceCollectionExtensions
    {
        /// <summary>
        /// 注册HttpMouse相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IReverseProxyBuilder AddHttpMouse<TProxyConfigStoreProvider>(this IServiceCollection services)
            where TProxyConfigStoreProvider : class, IProxyConfigStoreProvider
        {
            var optionsKey = new HttpRequestOptionsKey<string>(ConstPool.ProxyRequestHostKey);
            var optionsKey2 = new HttpRequestOptionsKey<string>(ConstPool.ProxyRequestUrlKey);

            var builder = services.AddReverseProxy()
            .AddTransforms(ctx => ctx.AddRequestTransform(request =>
            {
                var req = request.HttpContext.Request;
                var match_proxy_host = req.Host.Host;
                request.ProxyRequest.Options.Set(optionsKey, match_proxy_host);
                request.ProxyRequest.Options.Set(optionsKey2, $"{req.Scheme}://{req.Host.Value}{req.Path}");
                return ValueTask.CompletedTask;
            }));

            services
                .AddSingleton<IHttpMouseClientHandler, HttpMouseClientHandler>()
                .AddSingleton<IReverseConnectionHandler, ReverseConnectionHandler>()
                .AddSingleton<IProxyConfigStoreProvider, TProxyConfigStoreProvider>()
                .AddSingleton<IProxyConfigProvider, HttpMouseProxyConfigProvider>()
                .AddSingleton<IForwarderHttpClientFactory, HttpMouseForwarderHttpClientFactory>();

            return builder;
        }
    }
}
