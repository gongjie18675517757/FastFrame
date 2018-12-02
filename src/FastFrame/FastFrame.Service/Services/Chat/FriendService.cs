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

        public FriendService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 好友列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FriendOutput>> Friends()
        {
            return await repository.Queryable.Select(x => new FriendOutput() { Id = x.Id, HeadIcon_Id = x.HandIconId, Name = x.Name }).ToListAsync();
        }
    }
}
