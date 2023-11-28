using FastFrame.Entity.Basis;
using FastFrame.Entity.Chat;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Chat
{
    /// <summary>
    /// 好友管理
    /// </summary>
    public partial class FriendService(
        IRepository<User> repository,  
        IApplicationSession appSession) : IService
    {

        /// <summary>
        /// 好友列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FriendOutput>> Friends()
        {
            var userId = appSession.CurrUser?.Id;
            var list = await repository.Queryable
                .Where(x => x.Id != userId && x.Enable == (int)Entity.Enums.EnabledMark.enabled)
                .Select(x => new FriendOutput()
                {
                    Id = x.Id,
                    HeadIcon_Id = x.HandIcon_Id,
                    Name = x.Name
                })
                .ToListAsync();
            return list;
        }
    }
}
