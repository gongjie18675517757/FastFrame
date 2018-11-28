using FastFrame.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FastFrame.Database
{
    public abstract class BaseMapping<T> : IMapping<T> where T : class, IEntity
    {
        public virtual void ModelCreating(ModelBuilder modelBuilder)
        {
            var entityType = typeof(T);
            var entity = modelBuilder.Entity<T>();
            var currNameSpace = string.Join(",", entityType.Namespace.Split(new char[] { '.' }).Skip(2));
            entity.ToTable($"{currNameSpace}_{entityType.Name}");
            /*指定主键*/
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedNever();

            /*索引租户ID*/
            if (typeof(IHasTenant).IsAssignableFrom(entityType))
                entity.HasIndex("Tenant_Id").HasName("Index_OrganizeId");

            /*过滤掉软删除的*/
            if (typeof(IHasSoftDelete).IsAssignableFrom(entityType))
            {
                var parameterExpression= Expression.Parameter(typeof(T));
                var memberExpression = Expression.Property(parameterExpression,nameof(IHasSoftDelete.IsDeleted));
                var unaryExpression= Expression.Not(memberExpression);
                var expression= Expression.Lambda<Func<T, bool>>(unaryExpression, parameterExpression);
                entity.HasQueryFilter(expression);
            }

            foreach (var item in typeof(T).GetProperties())
            {
                if (item.GetCustomAttribute<NotMappedAttribute>() != null)
                    continue;

                var prop = modelBuilder.Entity<T>().Property(item.Name);
                if (item.PropertyType == typeof(string))
                {
                    /*所有字符串,指定为unicode*/
                    prop.IsUnicode();

                    /*所有ID,指定长度为36*/
                    if (item.Name.EndsWith("Id"))
                        prop.HasMaxLength(25);

                    /*非ID字段，且没有指定长度的，默认200*/
                    else if (item.GetCustomAttribute<StringLengthAttribute>() == null && item.Name != "Content")
                    {
                        prop.HasMaxLength(200);
                    }
                }

                if (item.PropertyType.IsEnum)
                {
                    prop.HasConversion<string>();
                }
            }
        }
    }
}
