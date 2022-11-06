using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FastFrame.Application.Basis
{
    public partial class LoginLogServcie : BaseService<LoginLogModel>
    {
        private readonly IRepository<LoginLog> loginLogs;
        private readonly IRepository<User> users;

        public LoginLogServcie(IRepository<LoginLog> loginLogs, IRepository<User> users)
        {
            this.loginLogs = loginLogs;
            this.users = users;
        }

        protected override IQueryable<LoginLogModel> QueryMain()
        {
            return from a in loginLogs
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
                       User_Id = a.User_Id,
                       FailReason = a.FailReason,
                       IsSuccessful = a.IsSuccessful,
                   };
        }
    }
}
