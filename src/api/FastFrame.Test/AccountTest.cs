using FastFrame.Application.Account;
using FastFrame.Application.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Test
{
    [TestClass]
    public class AccountTest : BaseServiceTest
    {
        [TestMethod]
        [DataRow("test1", "123456")]
        [DataRow("test2", "123456")]
        public async Task TestRegist(string name, string password)
        {
            
            var accountService = ServiceProvider.GetService<AccountService>();
            try
            {
                /*ÏÈ×¢²á*/
                var user = await accountService.RegistAsync(new UserDto()
                {
                    Account = name,
                    Name = name,
                    Password = password
                });
                Assert.AreEqual(user.Name, name);

                var currUser = await accountService.LoginAsync(new LoginInput() { Account = name, Password = password });
                Assert.AreEqual(currUser.Name, name);

                var curr = ServiceProvider.GetService<IAppSessionProvider>().CurrUser;

                Assert.AreEqual(curr.Name, name);
                Assert.AreEqual(curr.Id, user.Id);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                var repository = ServiceProvider.GetService<IRepository<User>>();
                var user = await repository.Queryable.Where(x => x.Account == name).FirstOrDefaultAsync();
                if (user != null)
                    await repository.DeleteAsync(user);
                await repository.CommmitAsync();
            }
        }
    }


}
