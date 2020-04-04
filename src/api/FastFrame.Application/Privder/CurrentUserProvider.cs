using CSRedis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Privder
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly CSRedisClient cSRedisClient;
        private readonly IDescriptionProvider descriptionProvider;
        private readonly Database.DataBase dataBase;
        private string tokenName = "_code";
        private string token;
        private ICurrUser currUser;
        private ITenant tenant = null;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor,
            CSRedisClient cSRedisClient, IDescriptionProvider descriptionProvider, Database.DataBase dataBase)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.cSRedisClient = cSRedisClient;
            this.descriptionProvider = descriptionProvider;
            this.dataBase = dataBase;
        }
        private bool tryGetHost(string[] headerNames, out string host)
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
        public ITenant GetCurrOrganizeId()
        {
            if (tenant != null)
                return tenant;
            /*
             * X-ORIGINAL-HOST
             * Origin
             * Referer
             */
            var host = getHost();

            var tenantId = dataBase.Set<TenantHost>()
                .Where(x => x.Host == host)
                .Select(x => x.Tenant_Id)
                .FirstOrDefault();

            tenant = dataBase.Set<Tenant>()
                .Where(x => x.Id == tenantId)
                .OrderByDescending(x => x.Super_Id)
                .FirstOrDefault();

            return tenant;
        }

        private string getHost()
        {
            string host = httpContextAccessor.HttpContext.Request.Host.Value;
            if (!tryGetHost(new string[] { "X-ORIGINAL-HOST", "Origin", "Referer" }, out host))
                host = httpContextAccessor.HttpContext.Request.Host.Value;
            return host;
        }

        public ICurrUser GetCurrUser()
        {
            if (currUser == null)
            {
                var token = GetToken();
                if (!token.IsNullOrWhiteSpace())
                {
                    var user = cSRedisClient.Get<CurrUser>(token);
                    if (user != null)
                        currUser = user;
                }
            }
            return currUser;
        }

        public async Task Login(ICurrUser currUser)
        {
            var host = getHost();
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

        public async Task LogOut()
        {
            var token = GetToken();
            await cSRedisClient.DelAsync(currUser.ToKen);
        }

        public void Refresh()
        {
            cSRedisClient.Set(currUser.ToKen, currUser, 60 * 60 * 24);
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
    }
}