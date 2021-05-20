using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FastFrame.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FastFrame.Database
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class DataBase : DbContext
    {
        public DataBase(DbContextOptions options) : base(options)
        {
               
        } 
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
    }
}
