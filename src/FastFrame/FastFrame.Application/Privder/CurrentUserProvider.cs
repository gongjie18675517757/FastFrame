using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace FastFrame.Application.Privder
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

        }
        public string GetCurrOrganizeId()
        {
            return httpContextAccessor.HttpContext.GetRouteValue("organize")?.ToString();
        }

        public ICurrUser GetCurrUser()
        {
            throw new NotImplementedException();
        }

        public Task Login(ICurrUser currUser)
        {
            throw new NotImplementedException();
        }

        public Task LogOut(ICurrUser currUser)
        {
            throw new NotImplementedException();
        }
    }
}