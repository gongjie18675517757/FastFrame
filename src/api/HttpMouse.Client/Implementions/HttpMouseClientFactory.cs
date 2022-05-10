using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace HttpMouse.Client.Implementions
{
    /// <summary>
    /// 客户端工厂
    /// </summary>
    sealed class HttpMouseClientFactory : IHttpMouseClientFactory
    { 
        private const string CLINET_ID = "HttpMouse-ClientToKen"; 

        private readonly IOptionsMonitor<HttpMouseClientOptions> options;
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// 客户端工厂
        /// </summary>
        /// <param name="options"></param>
        /// <param name="loggerFactory"></param>
        public HttpMouseClientFactory(IOptionsMonitor<HttpMouseClientOptions> options,ILoggerFactory loggerFactory)
        {
            this.options = options;
            this.loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 创建客户端实例
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IHttpMouseClient> CreateAsync(CancellationToken cancellationToken)
        {
            var opt = this.options.CurrentValue;
            var uriBuilder = new UriBuilder(opt.ServerUri);
            uriBuilder.Scheme = uriBuilder.Scheme == Uri.UriSchemeHttp ? "ws" : "wss";

            var webSocket = new ClientWebSocket();
            webSocket.Options.RemoteCertificateValidationCallback = delegate { return true; }; 
            webSocket.Options.SetRequestHeader(CLINET_ID, opt.ClientId); 

            await webSocket.ConnectAsync(uriBuilder.Uri, cancellationToken);
            return new HttpMouseClient(webSocket, opt, loggerFactory.CreateLogger<IHttpMouseClient>());
        }
    }
}
