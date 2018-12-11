using FastFrame.Dto.Dtos.Chat;
using FastFrame.Entity.Basis;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Chat
{
    /// <summary>
    /// 好友管理
    /// </summary>
    public partial class FriendService : IService
    {
        private readonly IRepository<User> repository;
        private readonly Infrastructure.Interface.ICurrentUserProvider currentUserProvider;

        public FriendService(IRepository<User> repository, Infrastructure.Interface.ICurrentUserProvider currentUserProvider)
        {
            this.repository = repository;
            this.currentUserProvider = currentUserProvider;
        }

        /// <summary>
        /// 好友列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FriendOutput>> Friends()
        {
            var userId = currentUserProvider.GetCurrUser().Id;
            return await repository.Queryable
                .Where(x => x.Id != userId && !x.IsDisabled)
                .Select(x => new FriendOutput()
                {
                    Id = x.Id,
                    HeadIcon_Id = x.HandIconId,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}
