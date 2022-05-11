using FastFrame.Application.Account;
using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FastFrame.Test
{
    public abstract class BaseBusinService : BaseServiceTest
    {
        public virtual async Task Init()
        {
            var accountService = ServiceProvider.GetService<AccountService>();
            var user = await accountService.RegistAsync(new UserDto()
            {
                Account = IdGenerate.NetId(),
                Name = IdGenerate.NetId(),
                Password = "123456"
            });
            await accountService.LoginAsync(new LoginInput()
            {
                Account = user.Account,
                Password = "123456"
            });
        }
        public override void Dispose()
        { 
            GC.SuppressFinalize(this);
        }
    }
}
