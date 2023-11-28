using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FastFrame.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FastFrame.Database
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class DataBase(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*循环添加DbSet*/
            var entityType = typeof(IQuery);
            var types = entityType.Assembly.GetTypes().Where(x => entityType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
            foreach (var item in types)
                modelBuilder.Model.AddEntityType(item);

            /*循环添加Mapping*/
            var type = typeof(IEntityMapping);
            types = type.Assembly.GetTypes().Where(x => type.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
            foreach (var item in types)
            {
                var mapping = (IEntityMapping)item.Assembly.CreateInstance(item.FullName);

                mapping.ModelCreating(modelBuilder);
            }


            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.EnableDetailedErrors(true);
        //    optionsBuilder.EnableSensitiveDataLogging(true);  
        //}
    }


    //public class SQLBuild<T> where T : class
    //{
    //    private readonly DataBase db;

    //    public SQLBuild(DataBase db)
    //    {
    //        this.db = db;
    //    }

    //    private IEntityType GetEntityType()
    //    {
    //        return db.Model.FindEntityType(typeof(T));
    //    }



    //    public string BuildInsertSql()
    //    {
    //        IEntityType entityType = GetEntityType();
    //        var table_name = entityType.GetTableName();
    //        var schema_name = entityType.GetSchema();
    //        var properties = entityType.GetProperties();

    //        var storeObjectIdentifier = StoreObjectIdentifier.Table(table_name, schema_name);

    //        foreach (var prop in properties)
    //        {
    //            var colName = prop.GetColumnName(in storeObjectIdentifier);
    //        } 


    //    }
    //}
}
