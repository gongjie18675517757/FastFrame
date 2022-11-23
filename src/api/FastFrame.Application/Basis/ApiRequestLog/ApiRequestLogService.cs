using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Cache;
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
    public class ApiRequestLogService : BaseService<ApiRequestLog>, IIntervalWorkHost
    {
        private readonly IRepository<ApiRequestLog> repository;
        private readonly ICacheProvider cacheProvider;

        public ApiRequestLogService(IServiceProvider loader, IRepository<ApiRequestLog> repository, ICacheProvider cacheProvider) : base(loader)
        {
            this.repository = repository;
            this.cacheProvider = cacheProvider;
        }



        /// <summary>
        /// 定时插入记录
        /// </summary> 
        [IntervalWork("ApiRequestLogService.BackgroundInsert", "0 * * * * ?")]
        public virtual async Task BackgroundInsert()
        {
            while (true)
            {
                for (int i = 0; i < 100; i++)
                {
                    var log = await cacheProvider.ListPopAsync<ApiRequestLog>(ApiRequestLog.ListKeyName);
                    if (log == null)
                        continue;

                    await repository.AddAsync(log); 
                }

                var count = await repository.CommmitAsync();

                if (count == 0)
                    break;
            }
        }

        protected override IQueryable<ApiRequestLog> DefaultQueryable()
        {
            return repository;
        }
    }
}
