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
        protected ICurrentUserProvider CurrUserProvider { get; set; }

        protected ICurrUser currUser => CurrUserProvider?.GetCurrUser();

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
                if (queryable != null)
                    return queryable;
                else
                {
                    IQueryable<T> query = context.Set<T>().AsNoTracking();
                    var tenant = CurrUserProvider.GetCurrOrganizeId();
                    if (tenant != null &&
                        !tenant.Id.IsNullOrWhiteSpace() &&
                        typeof(IHasTenant).IsAssignableFrom(typeof(T)))
                    {
                        query = query.Where(v => EF.Property<string>(v, "tenant_id") == tenant.Id);
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
