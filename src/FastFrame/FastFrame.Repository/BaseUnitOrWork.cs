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
            try
            {
                int count = context.SaveChanges();
                //bt.Commit();
                return count;
            }
            catch (Exception ex)
            {
                //bt.Rollback();
                throw ex;
            }

            //using (var bt = context.Database.BeginTransaction())
            //{
                
            //}
        }

        /// <summary>
        /// 事务保存
        /// </summary>
        /// <returns></returns>
        public async ValueTask<int> CommmitAsync()
        {
            try
            {
                int count = await context.SaveChangesAsync();
                //bt.Commit();
                return count;
            }
            catch (Exception ex)
            {
                //bt.Rollback();
                throw ex;
            }
            //using (var bt = await context.Database.BeginTransactionAsync())
            //{

            //}
        }
    }
}
