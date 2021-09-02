﻿using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FastFrame.Application.Basis
{
    public partial class LoginLogServcie : IPageListService<LoginLogModel>
    {
        private readonly IRepository<LoginLog> loginLogs;
        private readonly IRepository<User> users;

        public LoginLogServcie(IRepository<LoginLog> loginLogs, IRepository<User> users)
        {
            this.loginLogs = loginLogs;
            this.users = users;
        }

        public async Task<PageList<LoginLogModel>> PageListAsync(Pagination pageInfo)
        {
            var query = from a in loginLogs
                        join b in users on a.User_Id equals b.Id
                        select new LoginLogModel
                        {
                            Account = b.Account,
                            Id = a.Id,
                            IsEnabled = a.IsEnabled,
                            ExpiredTime = a.ExpiredTime,
                            IPAddress = a.IPAddress,
                            LastTime = a.LastTime,
                            LoginTime = a.LoginTime,
                            Name = b.Name,
                            User_Id = a.User_Id
                        };

            var list=await query.PageListAsync(pageInfo);

            return list;
        }
    }
}
