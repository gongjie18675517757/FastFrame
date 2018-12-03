using FastFrame.Database;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using FastFrame.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Service.Test
{
    public abstract class BaseBusinService : BaseServiceTest
    {
        public virtual async Task Init()
        {
            var accountService = ServiceProvider.GetService<AccountService>();
            var user = await accountService.RegistAsync(new FastFrame.Dto.Basis.UserDto()
            {
                Account = IdGenerate.NetId(),
                Name = IdGenerate.NetId(),
                Password = "123456"
            });
            await accountService.LoginAsync(new FastFrame.Dto.Dtos.LoginInput()
            {
                Account = user.Account,
                Password = "123456"
            });
        }
        public override void Dispose()
        { 
            base.Dispose();
        }
    }
}
