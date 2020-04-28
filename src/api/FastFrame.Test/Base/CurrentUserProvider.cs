using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;
using System;
using System.Threading.Tasks;

namespace FastFrame.Test
{
    public class CurrentUserProvider : IAppSessionProvider
    {
        public ICurrUser CurrUser => null;

        public string Tenant_Id => "Test";

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
