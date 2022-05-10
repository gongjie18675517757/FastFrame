using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public class ApiRequestLogService : IService, IPageListService<ApiRequestLog>
    {
        private readonly IRepository<ApiRequestLog> repository;
        private readonly IBackgroundJob background;

        public ApiRequestLogService(IRepository<ApiRequestLog> repository, IBackgroundJob background)
        {
            this.repository = repository;
            this.background = background;
        }

        public Task<IPageList<ApiRequestLog>> PageListAsync(IPagination pageInfo)
        {
            return repository.PageListAsync(pageInfo);
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
    }
}
