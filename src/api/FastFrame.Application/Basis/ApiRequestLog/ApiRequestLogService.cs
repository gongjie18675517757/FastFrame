using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.IntervalWork;
using FastFrame.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ApiRequestLogService> logger;

        public ApiRequestLogService(IRepository<ApiRequestLog> repository, ILogger<ApiRequestLogService> logger)
        {
            this.repository = repository;
            this.logger = logger;
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
        public async void BackgroundInsert(ApiRequestLog apiRequestLog)
        {
            using (var serviceScope = Loader.CreateScope())
            {
                try
                {
                    await serviceScope.ServiceProvider.GetService<ApiRequestLogService>().InsertAsync(apiRequestLog);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "写入Log失败");
                }
            }
        }

        protected override IQueryable<ApiRequestLog> DefaultQueryable()
        {
            return repository;
        }
    }
}
