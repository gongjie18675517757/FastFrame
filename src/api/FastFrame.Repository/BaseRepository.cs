using FastFrame.Database;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Repository
{
    internal class BaseRepository<T> : BaseQueryable<T>, IRepository<T> where T : class, IEntity
    {
        public BaseRepository(DataBase context, IApplicationSession appSession) : base(context, appSession)
        {
        }


        /// <summary>
        /// 添加
        /// </summary> 
        public virtual async Task<T> AddAsync(T entity)
        {
            /*验证唯一性+关联性*/
            entity.Id = IdGenerate.NetId();
            if (entity is IHasTenant)
                context.Entry(entity).Property<string>("tenant_id").CurrentValue = AppSession?.Tenant_Id;

            var entityEntry = context.Entry(entity);
            entityEntry.State = EntityState.Added;

            await Task.CompletedTask;

            return entityEntry.Entity;
        }

        /// <summary>
        /// 删除
        /// </summary> 
        public virtual async Task DeleteAsync(T entity)
        {
            if (entity is IHasManage hasManage)
            {
                hasManage.Modify_User_Id = CurrUser?.Id;
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

            await Task.CompletedTask;
        }

        /// <summary>
        /// 删除
        /// </summary> 
        public virtual async Task DeleteAsync(string id)
        {
            var entity = await Queryable.FirstOrDefaultAsync(x => x.Id == id);
            await DeleteAsync(entity);

            //if (IsTransactionOpening)
            //    await context.SaveChangesAsync();
        }

        /// <summary>
        /// 更新数据
        /// </summary> 
        public virtual async Task<T> UpdateAsync(T entity)
        {
            await Task.CompletedTask;

            var entityEntry = context.Entry(entity);
            entityEntry.State = EntityState.Modified;

            //await EventBus.TriggerEventAsync(new EntityUpdateing<T>(entityEntry.Entity));

            //if (IsTransactionOpening)
            //    await context.SaveChangesAsync();

            return entityEntry.Entity;
        }

        /// <summary>
        /// 获取单条数据
        /// </summary> 
        public virtual async Task<T> GetAsync(string id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public Task<int> CommmitAsync()
        {
            return context.SaveChangesAsync();
        }

        public Task<int> ExecSqlAsync(string sql)
        {
            throw new NotImplementedException();
        }

        public string GetDbTableName()
        {

            var entityType = context.Set<T>().EntityType;
            return string.Join(".", new[] { entityType.GetSchema(), entityType.GetTableName() }.Where(v => !v.IsNullOrWhiteSpace()));
        }

        public string GetDbColumnName(string propName)
        {
            return context.Set<T>().EntityType.FindProperty(propName).GetColumnBaseName();
        }
    }
}
