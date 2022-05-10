using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Entity.Proxy
{
    /// <summary>
    /// 内网穿透隧道
    /// </summary>
    [Export]
    [RelatedField(nameof(Name))]
    public class ProxyClient : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        [Required]
        [Unique]
        public string Name { get; set; }

        /// <summary>
        /// ClientId
        /// </summary>
        [StringLength(50)]
        public string ClientToken { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }
    }

    /// <summary>
    /// 代理目标
    /// </summary>
    public class ProxyTarget : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 域名标识
        /// </summary>
        [StringLength(10)] 
        [Unique]
        public string OriginMark { get; set; }

        /// <summary>
        /// 关联:ProxyClient
        /// </summary>
        public string ProxyClient_Id { get; set; }

        /// <summary>
        /// 目标地址URL
        /// </summary>
        public virtual string Host { get; set; }

        /// <summary>
        /// 代理方式
        /// </summary>
        public virtual ProxyTargetEnum TargetEnum { get; set; }
    }


    /// <summary>
    /// 代理方式
    /// </summary>
    public enum ProxyTargetEnum
    {
        /// <summary>
        /// TCP
        /// </summary>
        tcp,

        /// <summary>
        /// UDP
        /// </summary>
        udp,

        /// <summary>
        /// HTTP
        /// </summary>
        http
    }
}
