using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Identity;
using FastFrame.Infrastructure.Interface;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FastFrame.Test
{
    public class CurrentUserProvider : IApplicationSession
    {
        public ICurrUser CurrUser => null;

        public string Tenant_Id => "Test";

        public string ApplicationRootPath => AppDomain.CurrentDomain.BaseDirectory;

        public IPAddress GetIPAddress()
        {
            return null;
        }

        public Task InitAsync()
        {
            return Task.CompletedTask;
        }

        public Task LoginAsync(ICurrUser currUser)
        {
            return Task.CompletedTask;
        }

        public Task LogOutAsync()
        {
            return Task.CompletedTask;
        }

        public Task RefreshIdentityAsync()
        {
            return Task.CompletedTask;
        }
    }
}
