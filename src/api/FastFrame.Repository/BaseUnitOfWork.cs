using FastFrame.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading.Tasks;

namespace FastFrame.Repository
{
    //internal class BaseUnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    //{
    //    private readonly DataBase dataBase;
    //    private IDbContextTransaction transaction;

    //    public BaseUnitOfWork(DataBase dataBase)
    //    {
    //        this.dataBase = dataBase;
    //        Console.WriteLine(Guid.NewGuid().ToString());
    //    }

    //    public bool IsTransactionOpening => transaction != null;

    //    public void BeginTransaction(IsolationLevel level)
    //    {
    //        if (transaction != null)
    //            throw new Exception("同时只允许开启一次数据库事务");

    //        transaction = dataBase.Database.BeginTransaction(level);
    //    }

    //    public async Task BeginTransactionAsync(IsolationLevel level)
    //    {
    //        if (transaction != null)
    //            throw new Exception("同时只允许开启一次数据库事务");

    //        transaction = await dataBase.Database.BeginTransactionAsync(level);
    //    }

    //    public int Commmit()
    //    {
    //        var count = dataBase.SaveChanges();
    //        if (transaction != null)
    //        {
    //            try
    //            {
    //                transaction.Commit();
    //            }
    //            catch (Exception ex)
    //            {
    //                transaction.Rollback();
    //                throw ex;
    //            }
    //            finally
    //            {
    //                transaction = null;
    //            }
    //        }

    //        return count;
    //    }

    //    public async Task<int> CommmitAsync()
    //    {
    //        var count = await dataBase.SaveChangesAsync();

    //        if (transaction != null)
    //        {
    //            try
    //            {
    //                await transaction.CommitAsync();
    //            }
    //            catch (Exception ex)
    //            {
    //                await transaction.RollbackAsync();
    //                throw ex;
    //            }
    //            finally
    //            {
    //                transaction = null;
    //            }
    //        }

    //        return count;
    //    }

    //    public void Dispose()
    //    {
    //        if (transaction != null)
    //        {
    //            transaction.Rollback();
    //            transaction.Dispose();
    //            transaction = null;
    //        }
    //    }

    //    public async ValueTask DisposeAsync()
    //    {
    //        if (transaction != null)
    //        {
    //            await transaction.RollbackAsync();
    //            await transaction.DisposeAsync();
    //            transaction = null;
    //        }
    //    }
    //}
}
