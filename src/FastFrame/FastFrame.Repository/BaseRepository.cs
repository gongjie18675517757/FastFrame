using FastFrame.Database;
using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;

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
                CreateUserId = currUser?.Id,
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
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            //entity.IsDeleted = true;
            var foreign = await context.Set<Foreign>().FirstOrDefaultAsync(x => x.EntityId == entity.Id);
            //foreign.ModifyUserId = currUser?.Id;
            //foreign.ModifyTime = DateTime.Now;
            //context.Entry(foreign).State = EntityState.Modified;
            //context.Entry(entity).State = EntityState.Modified;
            context.Set<Foreign>().Remove(foreign);
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
            if (foreign != null)
            {
                foreign.ModifyUserId = currUser?.Id;
                foreign.ModifyTime = DateTime.Now;
                context.Entry(foreign).State = EntityState.Modified;
            }
            var entityEntry = context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            return entityEntry.Entity;
        }

    }
}
