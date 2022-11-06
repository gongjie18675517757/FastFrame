using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.IntervalWork;
using FastFrame.Repository;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    /// <summary>
    /// 记录请求日志
    /// </summary>
    public class ApiRequestLogService : BaseService<ApiRequestLog>
    {
        private readonly IRepository<ApiRequestLog> repository;
        private readonly IBackgroundJob background;

        public ApiRequestLogService(IRepository<ApiRequestLog> repository, IBackgroundJob background)
        {
            this.repository = repository;
            this.background = background;
        } 

        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="apiRequestLog"></param>
        /// <returns></returns>
        public async Task InsertAsync(ApiRequestLog apiRequestLog)
        {
            await repository.AddAsync(apiRequestLog);
            await repository.CommmitAsync();
        }

        /// <summary>
        /// 插入记录(立即返回)
        /// </summary>
        /// <param name="apiRequestLog"></param>
        public void BackgroundInsert(ApiRequestLog apiRequestLog)
        {
            background.SetTimeout<ApiRequestLogService>(v => v.InsertAsync(apiRequestLog), null);
        }

        protected override IQueryable<ApiRequestLog> QueryMain()
        {
            return repository;
        }
    }
}
