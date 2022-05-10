
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Yarp.ReverseProxy.Forwarder;

namespace HttpMouse.Implementions
{
    /// <summary>
    /// 反向连接的HttpClient工厂
    /// </summary>
    sealed class HttpMouseForwarderHttpClientFactory : ForwarderHttpClientFactory
    {
        private readonly IReverseConnectionHandler reverseConnectionHandler;
        private readonly ILogger<HttpMouseForwarderHttpClientFactory> logger;
        private readonly IProxyConfigStoreProvider proxyConfigStoreProvider;
        private readonly HttpRequestOptionsKey<string> matchProxyHostOptionsKey = new(ConstPool.ProxyRequestHostKey);
        private readonly HttpRequestOptionsKey<string> matchProxyUrlOptionsKey = new(ConstPool.ProxyRequestUrlKey);

        /// <summary>
        /// 反向连接的HttpClient工厂
        /// </summary>
        /// <param name="reverseConnectionHandler"></param>
        /// <param name="logger"></param>
        /// <param name="proxyConfigStoreProvider"></param>
        public HttpMouseForwarderHttpClientFactory(
            IReverseConnectionHandler reverseConnectionHandler,
            ILogger<HttpMouseForwarderHttpClientFactory> logger,
            IProxyConfigStoreProvider proxyConfigStoreProvider)
        {
            this.reverseConnectionHandler = reverseConnectionHandler;
            this.logger = logger;
            this.proxyConfigStoreProvider = proxyConfigStoreProvider;
        }

        /// <summary>
        /// 配置handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="handler"></param>
        protected override void ConfigureHandler(ForwarderHttpClientContext context, SocketsHttpHandler handler)
        {
            base.ConfigureHandler(context, handler);
            handler.ConnectCallback = this.ConnectCallback;
        }

        /// <summary>
        /// 连接回调
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        private async ValueTask<Stream> ConnectCallback(SocketsHttpConnectionContext context, CancellationToken cancellation)
        {
            if (!context.InitialRequestMessage.Options.TryGetValue(matchProxyHostOptionsKey, out var host))
                //throw new InvalidOperationException($"无法取到请求host");
                return ResponseError.HostReqired();

            if (!proxyConfigStoreProvider.TryGetConfigByMatchHost(host, out var proxyConfig))
                //throw new InvalidOperationException($"未获取到此地址的配置");
                return ResponseError.NoSite();

            context.InitialRequestMessage.Options.TryGetValue(matchProxyUrlOptionsKey, out var s_url);
            var t_url = context.InitialRequestMessage.RequestUri?.ToString();

            logger.LogInformation($"s_url:{s_url}>>t_url:{t_url}");

            try
            {
                return await this.reverseConnectionHandler.CreateAsync(proxyConfig.AppToken, cancellation);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex.Message);
                throw;
            }
        }


    }
}
