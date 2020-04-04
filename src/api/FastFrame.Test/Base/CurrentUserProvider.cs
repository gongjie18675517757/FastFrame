using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;
using System;
using System.Threading.Tasks;

namespace FastFrame.Test
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        ICurrUser curr = null;
        public ITenant GetCurrOrganizeId()
        {
            return new Tenant() { Id = "test", Super_Id = "" };
        }

        public ICurrUser GetCurrUser()
        {
            return curr;
        }

        public Task Login(ICurrUser currUser)
        {
            curr = currUser;
            return Task.CompletedTask;
        }

        public Task LogOut()
        {
            curr = null;
            return Task.CompletedTask;
        }

        public void Refresh()
        {
        }
    }
}
