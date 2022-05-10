using System;
using System.Buffers;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HttpMouse.Core;

namespace HttpMouse.Implementions
{
    /// <summary>
    /// 客户端
    /// </summary>
    sealed class HttpMouseClient : IHttpMouseClient
    {
        private readonly ProxyConfig proxyConfig;
        private readonly WebSocket webSocket;

        /// <summary>
        /// 基于websocket的主连接
        /// </summary>
        /// <param name="proxyConfig"></param> 
        /// <param name="webSocket"></param> 
        public HttpMouseClient(ProxyConfig proxyConfig, WebSocket webSocket)
        {
            this.proxyConfig = proxyConfig;
            this.webSocket = webSocket;
        }

        /// <summary>
        /// 发送创建反向连接指令
        /// </summary> 
        /// <param name="connectionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SendCreateConnectionAsync(Guid connectionId, CancellationToken cancellationToken)
        {
            return this.webSocket.SendCommandAsync(ICommand.CREATE_WEB_PROXY_CONNECTION, connectionId.ToByteArray(), cancellationToken);
        }

        /// <summary>
        /// 等待关闭
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task WaitingCloseAsync(CancellationToken cancellationToken = default)
        {
            //var buffer = ArrayPool<byte>.Shared.Rent(4);
            try
            {
                while (cancellationToken.IsCancellationRequested == false)
                {
                    await this.webSocket.ReceiveCommandAsync(cancellationToken);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                //ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// 由于异常而关闭
        /// </summary> 
        /// <param name="error">异常原因</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CloseAsync(string error, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.webSocket.CloseAsync(WebSocketCloseStatus.ProtocolError, error, cancellationToken);
            }
            catch
            {
            }
        }

        public override string ToString()
        {
            return $"{this.proxyConfig.AppToken}->{this.proxyConfig.TargetAddress}";
        }
    }
}
