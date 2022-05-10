using System;

namespace HttpMouse.Core
{
    /// <summary>
    /// 命令
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 配置更新
        /// </summary>
        public const ushort CONFIG_CHANGE = 1;

        /// <summary>
        /// 创建WEB代理连接
        /// </summary>
        public const ushort CREATE_WEB_PROXY_CONNECTION = 2;

        /// <summary>
        /// 客户端LOG
        /// </summary>
        public const ushort CLIENT_LOG = 3;

        /// <summary>
        /// 命令内容
        /// </summary>
        ushort CommandName { get; }

        /// <summary>
        /// 命令内容
        /// </summary>
        ArraySegment<byte> CommandBody { get; }
    }
}
