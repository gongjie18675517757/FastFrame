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

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor,
            CSRedisClient cSRedisClient, IDescriptionProvider descriptionProvider, Database.DataBase dataBase)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.cSRedisClient = cSRedisClient;
            this.descriptionProvider = descriptionProvider;
            this.dataBase = dataBase;
        }
        public string GetCurrOrganizeId()
        {
            string host;
            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-ORIGINAL-HOST", out var sv) && sv.Any())
            {
                var arr = sv.ToArray();
                host = arr.FirstOrDefault();
            }
            else
            {
                host = httpContextAccessor.HttpContext.Request.Host.Value;
                //host = new Uri(host).Authority;
            }

            return dataBase.Set<TenantHost>().Where(x => x.Host == host).FirstOrDefault()?.Tenant_Id;
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
            await cSRedisClient.SetAsync(currUser.ToKen, currUser, 60*60*24);
            httpContextAccessor.HttpContext.Response.Headers.Add(tokenName, currUser.ToKen);
            httpContextAccessor.HttpContext.Response.Cookies.Delete(tokenName);
            httpContextAccessor.HttpContext.Response.Cookies.Append(tokenName, currUser.ToKen, new CookieOptions()
            {
                Expires = new DateTimeOffset(DateTime.Now.AddYears(1)),
                HttpOnly = true,
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
            token = string.Empty;
            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue(tokenName, out var headerValue))
            {
                token = headerValue.First();
            }
            if (token.IsNullOrWhiteSpace() && httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(tokenName, out var cookieValue))
            {
                token = cookieValue;
            }
            if (token.IsNullOrWhiteSpace() && httpContextAccessor.HttpContext.Request.Query.TryGetValue(tokenName, out var queryValue))
            {
                token = queryValue.First();
            }

            return token;
        }
    }
}