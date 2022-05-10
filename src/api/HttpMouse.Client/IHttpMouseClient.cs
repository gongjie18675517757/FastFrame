using System;
using System.Threading;
using System.Threading.Tasks;

namespace HttpMouse.Client
{
    /// <summary>
    /// 客户端接口
    /// </summary>
    public interface IHttpMouseClient : IDisposable
    {
        /// <summary>
        /// 获取是否连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 手动关闭
        /// </summary>
        /// <returns></returns>
        Task Close(CancellationToken? cancellationToken);

        /// <summary>
        /// 接收命令
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ReceiveCMDAsync(CancellationToken cancellationToken); 
    }
}
