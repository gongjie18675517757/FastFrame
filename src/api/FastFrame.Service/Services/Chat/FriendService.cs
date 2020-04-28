﻿using FastFrame.Dto.Dtos.Chat;
using FastFrame.Entity.Basis;
using FastFrame.Entity.Chat;
using FastFrame.Infrastructure.Interface;
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
        private readonly IRepository<FriendMessage> messageRepository;
        private readonly IRepository<MessageTarget> targetRepository;
        private readonly IAppSessionProvider appSession;

        public FriendService(
            IRepository<User> repository,
            IRepository<FriendMessage> messageRepository,
            IRepository<MessageTarget> targetRepository,
            IAppSessionProvider appSession)
        {
            this.repository = repository;
            this.messageRepository = messageRepository;
            this.targetRepository = targetRepository;
            this.appSession = appSession;
        }

        /// <summary>
        /// 好友列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FriendOutput>> Friends()
        {
            var userId = appSession.CurrUser?.Id;
            var list = await repository.Queryable
                .Where(x => x.Id != userId && x.Enable == Entity.Enums.EnabledMark.Enabled)
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
