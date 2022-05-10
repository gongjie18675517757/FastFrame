using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HttpMouse.Client
{
    /// <summary>
    /// 客户端选项
    /// </summary>
    public class HttpMouseClientOptions
    {
        /// <summary>
        /// 服务器Uri 
        /// </summary>
        [AllowNull]
        [Required]
        public Uri ServerUri { get; set; }

        /// <summary>
        /// 客户端id 
        /// </summary>
        [Required]
        [AllowNull]
        public string ClientId { get; set; } 
    }
}
