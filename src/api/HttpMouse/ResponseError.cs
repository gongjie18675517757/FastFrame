using System.IO;
using System.Text;

namespace HttpMouse
{
    /// <summary>
    /// 错误响应
    /// </summary>
    public static class ResponseError
    {
        /// <summary>
        /// 客户端离线
        /// </summary>
        /// <returns></returns>
        public static Stream OfflinePage()
        {
            var bytes = Encoding.UTF8.GetBytes(
                $"HTTP/1.1 200 OK\r\nContent-Type:text/html; charset=utf-8\r\n\r\n{Resources.Page_Offline}\r\n");

            return new MemoryStream(bytes);
        }

        /// <summary>
        /// 没有域名
        /// </summary>
        /// <returns></returns>
        public static Stream HostReqired()
        {
            var bytes = Encoding.UTF8.GetBytes(
                $"HTTP/1.1 200 OK\r\nContent-Type:text/html; charset=utf-8\r\n\r\n{Resources.Page_HostRequired}\r\n");

            return new MemoryStream(bytes);
        }

        /// <summary>
        /// 没有此站点
        /// </summary>
        /// <returns></returns>
        public static Stream NoSite()
        {
            var bytes = Encoding.UTF8.GetBytes(
                $"HTTP/1.1 200 OK\r\nContent-Type:text/html; charset=utf-8\r\n\r\n{Resources.Page_NoSite}\r\n");

            return new MemoryStream(bytes);
        }
    }
}
