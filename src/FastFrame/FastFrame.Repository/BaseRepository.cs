using FastFrame.Database;
using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.Repository
{
    internal class BaseRepository<T> : BaseUnitOrWork, IRepository<T> where T : class, IEntity
    {
        private readonly DataBase context;
        private readonly ICurrentUserProvider currentUserProvider;
        private readonly IEventBus eventBus;
        private readonly ICurrUser currUser;

        public BaseRepository(DataBase context, ICurrentUserProvider currentUserProvider, IEventBus eventBus) : base(context)
        {
            this.context = context;
            this.currentUserProvider = currentUserProvider;
            this.eventBus = eventBus;
            currUser = currentUserProvider.GetCurrUser();
        }

        /// <summary>
        /// 添加
        /// </summary> 
        public virtual async Task<T> AddAsync(T entity)
        {
            /*验证唯一性+关联性*/
            entity.Id = IdGenerate.NetId();
            if (entity is IHasTenant tenant)
            {
                context.Entry(entity).Property<string>("tenant_id").CurrentValue = currentUserProvider.GetCurrOrganizeId().Id;
            }

            await Verification(entity);

            if (entity is IHasManage hasManage)
            {
                hasManage.CreateTime = DateTime.Now;
                hasManage.ModifyTime = DateTime.Now;
                var curr = currentUserProvider.GetCurrUser();
                if (curr != null)
                {
                    hasManage.Create_User_Id = curr.Id;
                    hasManage.Modify_User_Id = curr.Id;
                }
            }

            var entityEntry = context.Entry(entity);
            entityEntry.State = EntityState.Added;

            await eventBus.TriggerAsync(new EntityAdding<T>(entityEntry.Entity));
            return entityEntry.Entity;
        }

        /// <summary>
        /// 验证数据
        /// </summary> 
        public virtual async Task Verification(T entity)
        {
            /*验证唯一性
             *1,组合唯一
             *2,单个唯一
             */
            var uniqueAttributes = entity.GetType().GetCustomAttributes<UniqueAttribute>();
            foreach (var uniqueAttribute in uniqueAttributes)
            {
                var names = uniqueAttribute.UniqueNames;
                var exist = await Queryable.DynamicQuery(new QueryCondition()
                {
                    Filters = names.Select(x => new Filter()
                    {
                        Compare = "==",
                        Name = x,
                        Value = entity.GetValue(x)?.ToString()
                    }).Concat(new[] {
                            new Filter()
                            {
                                Compare="!=",
                                Value=entity.Id,
                                Name="Id",
                            }
                        })
                }).AnyAsync();
                if (exist) throw new UniqueException(typeof(T), names);
            }
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.GetCustomAttribute<UniqueAttribute>() is UniqueAttribute uniqueAttribute)
                {
                    var value = entity.GetValue(prop.Name)?.ToString();
                    if (!value.IsNullOrWhiteSpace())
                    {
                        var any = await Queryable.Where($"{prop.Name}=@0 and Id!=@1", value, entity.Id).AnyAsync();
                        if (any)
                            throw new UniqueException(typeof(T), new string[] { prop.Name });
                    }
                }
            }
            /*验证关联性*/


            /*检测树状的循环引用*/

        }

        /// <summary>
        /// 删除
        /// </summary> 
        public virtual async Task DeleteAsync(T entity)
        {
            if (entity is IHasManage hasManage)
            {
                hasManage.Modify_User_Id = currUser?.Id;
                hasManage.ModifyTime = DateTime.Now;
            }
            if (entity is IHasSoftDelete)
            {
                context.Entry(entity).Property("isdeleted").CurrentValue = true;
                context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                context.Entry(entity).State = EntityState.Deleted;
            }

            await eventBus.TriggerAsync(new EntityDeleteing<T>(entity));
        }

        /// <summary>
        /// 删除
        /// </summary> 
        public virtual async Task DeleteAsync(string id)
        {
            var entity = await Queryable.FirstOrDefaultAsync(x => x.Id == id);
            await DeleteAsync(entity);
        }

        /// <summary>
        /// 更新数据
        /// </summary> 
        public virtual async Task<T> UpdateAsync(T entity)
        {
            /*需要验证唯一性和关联性*/
            await Verification(entity);

            if (entity is IHasManage hasManage)
            {
                hasManage.ModifyTime = DateTime.Now;
                hasManage.Modify_User_Id = currUser?.Id;
            }

            var entityEntry = context.Entry(entity);
            entityEntry.State = EntityState.Modified;

            await eventBus.TriggerAsync(new EntityUpdateing<T>(entityEntry.Entity));
            return entityEntry.Entity;
        }

        /// <summary>
        /// 获取单条数据
        /// </summary> 
        public virtual async Task<T> GetAsync(string id)
        {
            return await Queryable.Where("Id=@0", id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询表达式
        /// </summary> 
        public virtual IQueryable<T> Queryable
        {
            get
            {
                // return context.Set<T>();
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
