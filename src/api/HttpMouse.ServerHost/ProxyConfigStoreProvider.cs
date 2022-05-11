using HttpMouse.Implementions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HttpMouse.ServerHost
{
    internal class ProxyConfigStoreProvider : IProxyConfigStoreProvider
    {
        private readonly ConcurrentBag<Action<IEnumerable<ProxyConfig>>> actions = new();
        private readonly ConcurrentBag<ProxyConfig> proxyConfigs = new();
        private static readonly object _lock = new();

        public void RegisterChangeCallback(Action<IEnumerable<ProxyConfig>> callback)
        {
            actions.Add(callback);
        }

        public bool TryGetConfigByAppToken(string app_token, out ProxyConfig proxyConfig)
        {
            proxyConfig = proxyConfigs.FirstOrDefault(v => v.AppToken == app_token)!;
            return proxyConfig != null;
        }

        public bool TryGetConfigByMatchHost(string match_host, out ProxyConfig proxyConfig)
        {
            proxyConfig = proxyConfigs.FirstOrDefault(v => v.MatchHost == match_host)!;
            return proxyConfig != null;
        }

        public void UpdateProxys(IEnumerable<ProxyConfig> proxyConfigs)
        {
            lock (_lock)
            {
                this.proxyConfigs.Clear();
                foreach (var item in proxyConfigs)
                    this.proxyConfigs.Add(item);
            }

            foreach (var item in actions)
                item?.Invoke(this.proxyConfigs);
        }
    }
}
