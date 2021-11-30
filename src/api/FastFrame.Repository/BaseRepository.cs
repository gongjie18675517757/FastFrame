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
        private const string TranCommitSuccessEventKey = "TranCommitSuccessEventKey";
        private readonly EventListenManger eventListenManger;
        private readonly IServiceProvider loader;

        public BaseRepository(DataBase context,
                              EventListenManger eventListenManger,
                              IServiceProvider loader,
                              IApplicationSession appSession) : base(context, appSession)
        {
            this.eventListenManger = eventListenManger;
            this.loader = loader;
        }

        /// <summary>
        /// 添加事务提交后的事件
        /// 注意:此处的回调中一定要处理异常
        /// </summary>
        /// <param name="func"></param>
        public void AddCommitEventListen(Func<IServiceProvider, Task> func)
        {
            eventListenManger.AddEventListen(TranCommitSuccessEventKey, func);
        }

        /// <summary>
        /// 添加
        /// </summary> 
        public virtual async Task<T> AddAsync(T entity)
        {
            /*自动生成主键*/
            if (!(entity is INotGenerateableKey) || entity.Id.IsNullOrWhiteSpace())
                entity.Id = IdGenerate.NetId();

            /*自动填充租户ID*/
            var tenant_id = AppSession?.Tenant_Id;
            if (entity is IHasTenant hasTenant && tenant_id != null)
                hasTenant.Tenant_Id = tenant_id;

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

            /*如果附加过了，则不重新附加*/
            if (context.ChangeTracker.Entries<T>().Any(v => v.State == EntityState.Detached && v.Entity == entity))
                return entity;

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

        public async Task<int> CommmitAsync()
        {
            var count = await context.SaveChangesAsync();

            /*触发事务提交完成的事件*/
            foreach (var item in eventListenManger[TranCommitSuccessEventKey])
                await item?.Invoke(loader);

            return count;
        }

        public Task<int> ExecSqlAsync(string sql)
        {
            return context.Database.ExecuteSqlRawAsync(sql);
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
