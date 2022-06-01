using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// api请求记录
    /// </summary>
    public class ApiRequestLog : IEntity, INotGenerateKey
    {

        public const string ListKeyName = "ApiRequestLogList";

        public string Id { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 请求人
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// 请求人
        /// </summary>
        [StringLength(User.NameLength)]
        public string UserName { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [StringLength(200)]
        public string Path { get; set; }

        /// <summary>
        /// 耗时数(毫秒)
        /// </summary>
        public long Milliseconds { get; set; }

        /// <summary>
        /// 响应状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 请求大小
        /// </summary>
        public long? RequestLength { get; set; }
    }
}
