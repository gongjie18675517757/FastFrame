using FastFrame.Entity.Proxy;

namespace FastFrame.Application.Proxy
{
    public partial class ProxyClientDto
    {
        /// <summary>
        /// 代理目标
        /// </summary>
        public Dictionary<ProxyTargetEnum, ProxyTarget> ProxyDic { get; set; }
    }
}
