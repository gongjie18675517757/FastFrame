using FastFrame.Entity.Basis;
using FastFrame.Repository;
using FastFrame.Service.Services.Basis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Test.Basis
{
    [TestClass]
    public class UserServiceTest : BaseBusinService
    {
        [TestMethod]
        [DataRow("user1")]
        [DataRow("user2")]
        [DataRow("user3")]
        [DataRow("user4")]
        public async Task TestCRUD(string name)
        {
            await Init();
            var repository = ServiceProvider.GetService<IRepository<User>>();
            try
            {
                var service = ServiceProvider.GetService<UserService>();
                var user = new Dto.Basis.UserDto()
                {
                    Account = name,
                    Name = name,
                    Password = name
                };
                await service.AddAsync(user);
                user.Email = $"{name}@xx.com";
                await service.UpdateAsync(user);
                Assert.AreEqual(user.Email, $"{name}@xx.com");
                user = await service.GetAsync(user.Id);
                Assert.AreEqual(user.Name, name);

                await service.DeleteAsync(user.Id);
                var exists = await repository.Queryable.AnyAsync(x => x.Name == name);
                Assert.AreEqual(exists, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                var user = await repository.Queryable.Where(x => x.Account == name).FirstOrDefaultAsync();
                if (user != null)
                    await repository.DeleteAsync(user);
                await repository.CommmitAsync();
            }
        }
    }
}
