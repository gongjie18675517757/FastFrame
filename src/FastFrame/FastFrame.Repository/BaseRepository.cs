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
        public async Task<T> AddAsync(T entity)
        {
            /*验证唯一性+关联性*/
            entity.Id = Infrastructure.IdGenerate.NetId();
            entity.OrganizeId = currUser.OrganizeId;
            await Verification(entity);
            await context.Set<Entity.System.Foreign>().AddAsync(new Entity.System.Foreign()
            {
                EntityId = entity.Id,
                Id = Infrastructure.IdGenerate.NetId(),
                CreateTime = DateTime.Now,
                CreateUserId = currUser.Id,
                OrganizeId = currUser.OrganizeId,
            });

            var entityEntry = context.Entry(entity);
            entityEntry.State = EntityState.Added;
            return entityEntry.Entity;
        }

        public async Task Verification(T entity)
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
        public async Task Delete(T entity)
        {
            //context.Set<T>().Remove(entity);
            entity.IsDeleted = true;
            var foreign = await context.Set<Entity.System.Foreign>().FirstOrDefaultAsync(x => x.EntityId == entity.Id);
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
        public async Task DeleteAsync(string id)
        {
            var entity = await Queryable.FirstOrDefaultAsync(x => x.Id == id);
            await Delete(entity);
        } 

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(string id)
        {
            return await Queryable.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// 获取查询表达式
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Queryable => context.Set<T>().Where(x => x.OrganizeId == currUser.OrganizeId);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> Update(T entity)
        {
            /*需要验证唯一性和关联性*/
            var foreign = await context.Set<Entity.System.Foreign>().FirstOrDefaultAsync(x => x.EntityId == entity.Id);

            foreign.ModifyUserId = currUser.Id;
            foreign.ModifyTime = DateTime.Now;
            context.Entry(foreign).State = EntityState.Modified;
            var entityEntry = context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            return entityEntry.Entity;
        }

    }
}
