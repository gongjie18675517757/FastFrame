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
        public ICurrUser CurrUser { get; private set; }

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

        public void Login(ICurrUser currUser)
        {
            CurrUser = currUser;
        }

        public void LogOut()
        {
            CurrUser = null;
        }

        public void RefreshIdentity()
        {

        }
    }
}
