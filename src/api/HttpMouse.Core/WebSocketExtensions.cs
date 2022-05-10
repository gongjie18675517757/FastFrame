using System;
using System.Buffers;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace HttpMouse.Core
{
    public static class WebSocketExtensions
    {
        /// <summary>
        /// 接受命令
        /// [0,1]表示命令
        /// [2,3]表示长度
        /// [4..]表示命令BODY
        /// </summary>
        /// <param name="websock"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<ICommand> ReceiveCommandAsync(this WebSocket websock, CancellationToken cancellationToken)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(2);
            try
            {
                /*取命令名称*/
                await websock.ReceiveEndOfMessageAsync(buffer, 2, cancellationToken);
                var cmd_name = BitConverter.ToUInt16(buffer);

                /*取请求头长度*/
                await websock.ReceiveEndOfMessageAsync(buffer, 2, cancellationToken);
                var len = BitConverter.ToUInt16(buffer);

                /*返还缓冲区*/
                ArrayPool<byte>.Shared.Return(buffer);

                /*定义存放body的缓冲区*/
                var body_buffer = ArrayPool<byte>.Shared.Rent(len);
                await websock.ReceiveEndOfMessageAsync(body_buffer, len, cancellationToken);

                return new xxCommand(cmd_name, new ArraySegment<byte>(body_buffer,0,len));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// 把一条消息收完整
        /// [0,1]表示命令
        /// [2,3]表示长度
        /// [4..]表示命令BODY
        /// </summary>
        /// <param name="websock"></param>
        /// <param name="buffer"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="WebSocketException"></exception>
        private static async Task ReceiveEndOfMessageAsync(this WebSocket websock, byte[] buffer, int length, CancellationToken cancellationToken)
        {
            var c = 0;

            while (true)
            {
                var result = await websock.ReceiveAsync(new ArraySegment<byte>(buffer, c, length - c), cancellationToken);
                if (result.MessageType == WebSocketMessageType.Close)
                    throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely, result.CloseStatusDescription);

                c += result.Count;

                //if (result.EndOfMessage)
                //    break;
                if (c >= length)
                    break;
            }
        }

        private class xxCommand : ICommand, IDisposable
        {
            public xxCommand(ushort commandName, ArraySegment<byte> commandBody)
            {
                CommandName = commandName;
                CommandBody = commandBody;
            }

            public ushort CommandName { get; }

            public ArraySegment<byte> CommandBody { get; }

            public void Dispose()
            {
                ArrayPool<byte>.Shared.Return(CommandBody.Array);
            }
        }


        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="websock"></param>
        /// <param name="cmd_name"></param>
        /// <param name="cmd_body"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task SendCommandAsync(this WebSocket websock, ushort cmd_name, ArraySegment<byte> cmd_body, CancellationToken cancellationToken)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(2);
            try
            {
                /*写命令名称*/
                BitConverter.TryWriteBytes(buffer, cmd_name);
                await websock.SendAsync(new ArraySegment<byte>(buffer,0,2), WebSocketMessageType.Binary, true, cancellationToken);

                /*写命令长度*/
                BitConverter.TryWriteBytes(buffer, (ushort)cmd_body.Count);
                await websock.SendAsync(new ArraySegment<byte>(buffer, 0, 2), WebSocketMessageType.Binary, true, cancellationToken);

                /*写命令body*/
                await websock.SendAsync(cmd_body, WebSocketMessageType.Binary, true, cancellationToken);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }
}
