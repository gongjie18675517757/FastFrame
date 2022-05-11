using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using HttpMouse.Core;
using System.Text;

namespace HttpMouse.Implementions
{
    /// <summary>
    /// 客户端处理者
    /// </summary> 
    sealed class HttpMouseClientHandler : IHttpMouseClientHandler
    {
        private const string CLIENT_ID = "HttpMouse-ClientToKen";
        private readonly IProxyConfigStoreProvider proxyConfigStoreProvider;
        private readonly ILogger<HttpMouseClientHandler> logger;
        private readonly ConcurrentDictionary<string, IHttpMouseClient> clients = new();


        /// <summary>
        /// 客户端变化后事件
        /// </summary>
        public event Action<IHttpMouseClient[]> ClientsChanged;

        /// <summary>
        /// 客户端处理者
        /// </summary>
        /// <param name="proxyConfigStoreProvider"></param> 
        /// <param name="logger"></param>
        public HttpMouseClientHandler(
            IProxyConfigStoreProvider proxyConfigStoreProvider,
            ILogger<HttpMouseClientHandler> logger)
        {
            this.proxyConfigStoreProvider = proxyConfigStoreProvider;
            this.logger = logger;
        }

        /// <summary>
        /// 处理连接
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task HandleConnectionAsync(HttpContext context, Func<Task> next)
        {
            if (context.WebSockets.IsWebSocketRequest == false ||
                context.Request.Headers.TryGetValue(CLIENT_ID, out var clientIdValues) == false ||
                proxyConfigStoreProvider.TryGetConfigByAppToken(clientIdValues, out var proxyConfig) == false)
            {
                await next();
                return;
            } 

             
          
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var client = new HttpMouseClient(proxyConfig, webSocket); 

            // 验证连接唯一
            if (this.clients.TryAdd(proxyConfig.AppToken, client) == false)
            {
                await client.CloseAsync($"已在其它地方存在{proxyConfig.AppToken}的客户端实例");
                return;
            }

            await webSocket.SendCommandAsync(ICommand.CONFIG_CHANGE, Encoding.UTF8.GetBytes(proxyConfig.TargetAddress),new System.Threading.CancellationToken());
            var client_string = client.ToString();
            logger.LogInformation("{client_string}连接过来", client_string);
            ClientsChanged?.Invoke(clients.Values.ToArray()); 
            await client.WaitingCloseAsync(); 
            logger.LogInformation("{client_string}断开连接", client_string);
            clients.TryRemove(proxyConfig.AppToken, out _);
            ClientsChanged?.Invoke(clients.Values.ToArray());
        }

        /// <summary>
        /// 尝试获取连接 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string clientId, [MaybeNullWhen(false)] out IHttpMouseClient value)
        {
            return this.clients.TryGetValue(clientId, out value);
        }
    }
}
