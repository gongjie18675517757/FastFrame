using CSRedis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    public class AppSessionProvider : IAppSessionProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMemoryCache memoryCache;
        private readonly AccountService accountService;
        private readonly CSRedisClient cSRedisClient;
        private readonly string tokenName = "FastFrame:Authorize";
        private string token;
        private Tenant tenant;

        public AppSessionProvider(IHttpContextAccessor httpContextAccessor,
                                   IMemoryCache memoryCache,
                                   AccountService accountService,
                                   CSRedisClient cSRedisClient)
        {
            this.httpContextAccessor = httpContextAccessor;

            this.memoryCache = memoryCache;
            this.accountService = accountService;
            this.cSRedisClient = cSRedisClient;
        }

        public ICurrUser CurrUser { get; private set; }

        public string Tenant_Id => tenant?.Tenant_Id;


        private bool TryGetHeaderValue(string[] headerNames, out string host)
        {
            host = string.Empty;
            foreach (var headerName in headerNames)
            {
                if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue(headerName, out var sv) && sv.Any())
                {
                    var arr = sv.ToArray();
                    host = arr.FirstOrDefault();
                    return true;
                }
            }
            return false;
        } 

        private string GetHost()
        {
            if (!TryGetHeaderValue(new string[] { "X-ORIGINAL-HOST", "Origin", "Referer" }, out var host))
                host = httpContextAccessor.HttpContext.Request.Host.Value;
            return host;
        } 

        public async Task LoginAsync(ICurrUser currUser)
        {
            var host = GetHost();
            host = string.Join(".", host.Split(new char[] { '.' }).Skip(1));
            await cSRedisClient.SetAsync(currUser.ToKen, currUser, 60 * 60 * 24);
            httpContextAccessor.HttpContext.Response.Headers.Add(tokenName, currUser.ToKen);
            httpContextAccessor.HttpContext.Response.Cookies.Delete(tokenName);
            httpContextAccessor.HttpContext.Response.Cookies.Append(tokenName, currUser.ToKen, new CookieOptions()
            {
                Expires = new DateTimeOffset(DateTime.Now.AddYears(1)),
                HttpOnly = true,
                //Domain = ".localhost",
                Path = "/",

            });
        }

        public async Task LogOutAsync()
        {
            if (CurrUser != null)
                await cSRedisClient.DelAsync(CurrUser.ToKen);
        }

        public async Task RefreshIdentityAsync()
        {
            await cSRedisClient.SetAsync(CurrUser.ToKen, CurrUser, 60 * 60 * 24);
            await accountService.RefreshTokenAsync(CurrUser.ToKen);
        }

        private string GetToken()
        {
            if (!token.IsNullOrWhiteSpace())
                return token;
            var request = httpContextAccessor.HttpContext.Request;
            token = string.Empty;
            if (request.Headers.TryGetValue(tokenName, out var headerValue))
                token = headerValue.First();
            if (token.IsNullOrWhiteSpace() && request.Cookies.TryGetValue(tokenName, out var cookieValue))
                token = cookieValue;
            if (token.IsNullOrWhiteSpace() && request.Query.TryGetValue(tokenName, out var queryValue))
                token = queryValue.First();
            return token;
        }

        public async Task InitAsync()
        {
            var token = GetToken();
            if (!token.IsNullOrWhiteSpace())
            {
                if (await accountService.ExistsTokenAsync(token))
                {
                    var user = cSRedisClient.Get<CurrUser>(token);
                    if (user != null)
                    {
                        CurrUser = user;
                    }
                }
            }

            var host = GetHost();

            if (memoryCache.TryGetValue<Tenant[]>("Cache:Multi-Tenant-List", out var tenants) &&
               memoryCache.TryGetValue<TenantHost[]>("Cache:Multi-TenantHost-List", out var tenantHosts))
            {
                var tenantHost = tenantHosts.FirstOrDefault(v => v.Host == host);
                tenantHost ??= tenantHosts.FirstOrDefault(v => v.Host == "*");
                tenant = tenants.FirstOrDefault(v => v.Id == tenantHost?.Id) ?? tenants.FirstOrDefault(v => v.UrlMark == "*");
            }
            else
            {
                tenant = new Tenant();
            }
        }
    }
}