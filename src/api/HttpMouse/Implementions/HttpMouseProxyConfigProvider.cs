using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading;
using Yarp.ReverseProxy.Configuration;

namespace HttpMouse.Implementions
{
    /// <summary>
    /// httpMouse代理配置提供者
    /// </summary>
    internal sealed class HttpMouseProxyConfigProvider : IProxyConfigProvider
    {
        private readonly object _configLock = new();
        private volatile InMemoryConfig _config;

        public HttpMouseProxyConfigProvider([NotNull] IProxyConfigStoreProvider inputChangeProvider)
        {
            _config = new InMemoryConfig(Array.Empty<RouteConfig>(), Array.Empty<ClusterConfig>());

            inputChangeProvider.RegisterChangeCallback(Update);
        }

        public IProxyConfig GetConfig() => _config;

        private void Update([NotNull] IEnumerable<ProxyConfig> proxyConfigInputs)
        {
            lock (_configLock)
            {
                var routeConfigs = proxyConfigInputs
                    .Select(v => new RouteConfig
                    {
                        ClusterId = v.AppToken,
                        RouteId = v.AppToken,
                        Match = new RouteMatch
                        {
                            Hosts = new string[] 
                            {
                                v.MatchHost
                                //$"https://{v.MatchHost}",
                            }
                        }
                    })
                    .ToArray();

                var clusterConfigs = proxyConfigInputs
                    .Select(v => new ClusterConfig
                    {
                        ClusterId = v.AppToken,
                        Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                        {
                             {
                                "default",
                                new DestinationConfig() {
                                    Address = v.TargetAddress,
                                }
                            }
                        }
                    })
                    .ToArray();

                Update(routeConfigs, clusterConfigs);
            }
        }

        private void Update(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
        {
            var oldConfig = _config;
            _config = new InMemoryConfig(routes, clusters);
            oldConfig.SignalChange();
        }

        private class InMemoryConfig : IProxyConfig
        {
            private readonly CancellationTokenSource _cts = new();

            public InMemoryConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
            {
                Routes = routes;
                Clusters = clusters;
                ChangeToken = new CancellationChangeToken(_cts.Token);
            }

            public IReadOnlyList<RouteConfig> Routes { get; }

            public IReadOnlyList<ClusterConfig> Clusters { get; }

            public IChangeToken ChangeToken { get; }

            internal void SignalChange()
            {
                _cts.Cancel();
            }
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public interface IProxyConfigStoreProvider
    {
        /// <summary>
        /// 根据app_token返回此客户端的配置
        /// </summary>
        /// <param name="app_token"></param>
        /// <param name="proxyConfig"></param>
        /// <returns></returns>
        bool TryGetConfigByAppToken(string app_token, out ProxyConfig proxyConfig);

        /// <summary>
        /// 根据match_host返回此客户端的配置
        /// </summary>
        /// <param name="match_host"></param>
        /// <param name="proxyConfig"></param>
        /// <returns></returns>
        bool TryGetConfigByMatchHost(string match_host, out ProxyConfig proxyConfig);

        /// <summary>
        /// 注册代理配置变化回调
        /// </summary>
        void RegisterChangeCallback(Action<IEnumerable<ProxyConfig>> callback);

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="proxyConfigs"></param>
        void UpdateProxys(IEnumerable<ProxyConfig> proxyConfigs);
    }


    /// <summary>
    /// 客户端配置项
    /// </summary>
    public class ProxyConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientAppToken">客户端标识</param>
        /// <param name="matchHost">匹配域名</param>
        /// <param name="targetAddress">客户代理到目标地址</param>
        public ProxyConfig(string clientAppToken, string matchHost, string targetAddress)
        {
            AppToken = clientAppToken;
            TargetAddress = new Uri(targetAddress).GetComponents(UriComponents.HttpRequestUrl, UriFormat.Unescaped);
            MatchHost = matchHost;
        }

        /// <summary>
        /// 客户端标识
        /// </summary>
        public string AppToken { get; }

        /// <summary>
        /// 匹配域名
        /// </summary>
        public string MatchHost { get; }

        /// <summary>
        /// HTTP代理的目标地址
        /// </summary>
        public string TargetAddress { get; }
    }
}

