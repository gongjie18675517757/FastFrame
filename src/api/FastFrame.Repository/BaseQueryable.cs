using FastFrame.Database;
using FastFrame.Entity;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using AspectCore.DependencyInjection;

namespace FastFrame.Repository
{
    public class BaseQueryable<T> : IQueryRepository<T> where T : class, IQuery
    {
        private readonly DataBase context;
        private IQueryable<T> queryable;

        [FromServiceContext]
        protected IAppSessionProvider AppSession { get; set; }

        protected ICurrUser CurrUser => AppSession?.CurrUser;

        public BaseQueryable(DataBase context)
        {
            this.context = context;
        }

        /// <summary>
        /// 查询表达式
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> Queryable
        {
            get
            {
                if (queryable == null)
                {
                    IQueryable<T> query = context.Set<T>().AsNoTracking();
                    var tenantId = AppSession?.Tenant_Id;
                    if (!tenantId.IsNullOrWhiteSpace() && typeof(IHasTenant).IsAssignableFrom(typeof(T)))
                    {
                        query = query.Where(v => EF.Property<string>(v, "tenant_id") == tenantId);
                    }

                    queryable = query;
                }

                return queryable;
            }
        }

        public Type ElementType => Queryable.ElementType;

        public Expression Expression => Queryable.Expression;

        public IQueryProvider Provider => Queryable.Provider;

        public IEnumerator<T> GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }
    }
}
