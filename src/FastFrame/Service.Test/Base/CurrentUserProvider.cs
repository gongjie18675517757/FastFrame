using FastFrame.Infrastructure.Interface;
using System;
using System.Threading.Tasks;

namespace Service.Test
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        ICurrUser curr = null;
        public string GetCurrOrganizeId()
        {
            return "test";
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
