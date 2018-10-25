using FastFrame.Database;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FastFrame.Repository
{
    public  class BaseUnitOrWork : IUnitOfWork
    {
        private readonly DataBase context;

        public BaseUnitOrWork(DataBase context)
        {
            this.context = context;
        }

        /// <summary>
        /// 事务保存
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            using (var bt = context.Database.BeginTransaction())
            {
                try
                {
                    int count = context.SaveChanges();
                    bt.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    bt.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 事务保存
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommmitAsync()
        {
            using (var bt = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    int count = await context.SaveChangesAsync();
                    bt.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    bt.Rollback();
                    throw ex;
                }
            }
        }
    }
}
