using FastFrame.Database;
using FastFrame.Entity;
using FastFrame.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace FastFrame.Repository
{
    public class BaseQueryable<T> : IQueryRepository<T> where T : class, IQuery
    {
        private readonly DataBase context;
        private readonly ICurrentUserProvider currentUserProvider;

        public BaseQueryable(DataBase context, ICurrentUserProvider currentUserProvider)
        {
            this.context = context;
            this.currentUserProvider = currentUserProvider;
        }

        /// <summary>
        /// 查询表达式
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> Queryable
        {
            get
            {
                IQueryable<T> queryable = context.Set<T>();
                var tenant = currentUserProvider.GetCurrOrganizeId();
                if (typeof(IHasTenant).IsAssignableFrom(typeof(T)))
                {
                    queryable.Where(v => EF.Property<string>(v, "tenant_id") == tenant.Id);
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
