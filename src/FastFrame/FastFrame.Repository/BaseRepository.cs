using FastFrame.Database;
using FastFrame.Entity;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EF.Core.Expansion.Dynamic;
using FastFrame.Infrastructure;
using FastFrame.Entity.Basis;

namespace FastFrame.Repository
{
    public abstract class BaseRepository<T> : BaseUnitOrWork, IRepository<T> where T : class, IEntity
    {
        private readonly DataBase context;
        private readonly ICurrentUserProvider currentUserProvider;
        private readonly ICurrUser currUser;

        public BaseRepository(DataBase context, ICurrentUserProvider currentUserProvider) : base(context)
        {
            this.context = context;
            this.currentUserProvider = currentUserProvider;
            currUser = currentUserProvider.GetCurrUser();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            /*验证唯一性+关联性*/
            entity.Id = IdGenerate.NetId();
            entity.OrganizeId = currentUserProvider.GetCurrOrganizeId();
            await Verification(entity);
            await context.Set<Foreign>().AddAsync(new Foreign()
            {
                EntityId = entity.Id,
                Id = IdGenerate.NetId(),
                CreateTime = DateTime.Now,
                CreateUserId = currUser.Id,
                OrganizeId = currentUserProvider.GetCurrOrganizeId(),
            });

            var entityEntry = context.Entry(entity);
            entityEntry.State = EntityState.Added;
            return entityEntry.Entity;
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
                    Filter = new Filter()
                    {
                        CompareConditions = names.Select(x => new CompareCondition()
                        {
                            Compare = "==",
                            Name = x,
                            Value = entity.GetValue(x)?.ToString()
                        }).Concat(new[] {
                            new CompareCondition()
                            {
                                Compare="!=",
                                Value=entity.Id,
                                Name="Id",
                            }
                        })
                    }
                }).AnyAsync();
                if (exist) throw new Exception("重复");
            }


            /*验证关联性*/

            await Task.CompletedTask;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task DeleteAsync(T entity)
        {
            //context.Set<T>().Remove(entity);
            entity.IsDeleted = true;
            var foreign = await context.Set<Foreign>().FirstOrDefaultAsync(x => x.EntityId == entity.Id);
            foreign.ModifyUserId = currUser.Id;
            foreign.ModifyTime = DateTime.Now;
            context.Entry(foreign).State = EntityState.Modified;
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(string id)
        {
            var entity = await Queryable.FirstOrDefaultAsync(x => x.Id == id);
            await DeleteAsync(entity);
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(string id)
        {
            return await Queryable.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// 查询表达式
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> Queryable
        {
            get
            {
                if (currUser != null && currUser.IsRoot)
                    return context.Set<T>();
                else
                {
                    var organizeId = currentUserProvider.GetCurrOrganizeId();
                    return context.Set<T>().Where(x => x.OrganizeId == organizeId);
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<T> UpdateAsync(T entity)
        {
            /*需要验证唯一性和关联性*/
            var foreign = await context.Set<Foreign>().FirstOrDefaultAsync(x => x.EntityId == entity.Id);

            foreign.ModifyUserId = currUser.Id;
            foreign.ModifyTime = DateTime.Now;
            context.Entry(foreign).State = EntityState.Modified;
            var entityEntry = context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            return entityEntry.Entity;
        }

    }
}
