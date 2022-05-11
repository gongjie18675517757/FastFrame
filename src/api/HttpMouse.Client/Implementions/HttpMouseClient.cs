using System;
using System.Buffers;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HttpMouse.Core;
using Microsoft.Extensions.Logging;

namespace HttpMouse.Client.Implementions
{
    /// <summary>
    /// 客户端
    /// </summary>
    sealed class HttpMouseClient : IHttpMouseClient
    {
        private readonly ClientWebSocket webScoket;
        private readonly HttpMouseClientOptions options;
        private readonly ILogger logger;
        private readonly CancellationTokenSource disposeCancellationTokenSource = new();
        private string proxy_target_url;

        /// <summary>
        /// 获取是否连接
        /// </summary>
        public bool IsConnected => this.webScoket.State == WebSocketState.Open;

        /// <summary>
        /// 客户端
        /// </summary>
        /// <param name="webScoket"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public HttpMouseClient(ClientWebSocket webScoket, HttpMouseClientOptions options, ILogger logger)
        {
            this.webScoket = webScoket;
            this.options = options;
            this.logger = logger;
        }

        /// <summary>
        /// 关闭客户端
        /// </summary>
        /// <returns></returns>
        public async Task Close(CancellationToken? cancellationToken)
        {
            if (IsConnected)
                await webScoket.CloseAsync(WebSocketCloseStatus.InvalidMessageType, "手动关闭", cancellationToken ?? disposeCancellationTokenSource.Token);
        }

        /// <summary>
        /// 传输数据
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ReceiveCMDAsync(CancellationToken cancellationToken)
        {
            //var pack_header_buffer = ArrayPool<byte>.Shared.Rent(2);
            using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.disposeCancellationTokenSource.Token);
            try
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    var cmd = await this.webScoket.ReceiveCommandAsync(cancellationTokenSource.Token);

                    await HandleCommandAsync(cmd, cancellationTokenSource.Token);
                    //var connectionId = await this.ReadConnectionIdAsync(cancellationTokenSource.Token);
                    //this.TransportAsync(connectionId, cancellationTokenSource.Token);
                }
            }
            catch (Exception)
            {
                cancellationTokenSource.Cancel();
                throw;
            }
        }

        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task HandleCommandAsync(ICommand cmd, CancellationToken cancellationToken)
        {
            await Task.Yield();

            switch (cmd.CommandName)
            {
                case ICommand.CONFIG_CHANGE:
                    this.proxy_target_url = Encoding.UTF8.GetString(cmd.CommandBody);
                    break;
                case ICommand.CREATE_WEB_PROXY_CONNECTION:
                    var connectionId = new Guid(cmd.CommandBody);
                    this.TransportAsync(connectionId, cancellationToken);
                    break;
                case ICommand.CLIENT_LOG:
                    var msg = Encoding.UTF8.GetString(cmd.CommandBody);
                    logger.LogInformation(msg);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 绑定上下游的连接进行双向传输
        /// </summary> 
        /// <param name="connectionId"></param>
        /// <param name="cancellationToken"></param>
        private async void TransportAsync(Guid connectionId, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Yield();

                using var upConnection = await this.CreateUpConnectionAsync(cancellationToken);
                using var downConnection = await this.CreateDownConnectionAsync(connectionId, cancellationToken);

                var taskX = upConnection.CopyToAsync(downConnection, cancellationToken);
                var taskY = downConnection.CopyToAsync(upConnection, cancellationToken);

                await Task.WhenAny(taskX, taskY);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 创建下游连接
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Stream> CreateDownConnectionAsync(Guid connectionId, CancellationToken cancellationToken)
        {
            var server = this.options.ServerUri;
            var endpoint = new DnsEndPoint(server.Host, server.Port);
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            await socket.ConnectAsync(endpoint, cancellationToken);

            Stream connection = new NetworkStream(socket, ownsSocket: true);
            if (server.Scheme == Uri.UriSchemeHttps)
            {
                var sslConnection = new SslStream(connection, false, delegate { return true; });
                await sslConnection.AuthenticateAsClientAsync(server.Host);
                connection = sslConnection;
            }

            var reverse = $"REVERSE /{connectionId} HTTP/1.1\r\nHost: {server.Host}\r\n\r\n";
            var request = Encoding.ASCII.GetBytes(reverse);
            await connection.WriteAsync(request, cancellationToken);

            return connection;
        }

        /// <summary>
        /// 创建上游连接
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Stream> CreateUpConnectionAsync(CancellationToken cancellationToken)
        {
            var client = new Uri(this.proxy_target_url!);
            var endpoint = new DnsEndPoint(client.Host, client.Port);
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            await socket.ConnectAsync(endpoint, cancellationToken);

            return new NetworkStream(socket, ownsSocket: true);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.disposeCancellationTokenSource.Cancel();
            this.disposeCancellationTokenSource.Dispose();
            this.webScoket.Dispose();
        } 
    }
}
