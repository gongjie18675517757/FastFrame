using FastFrame.Entity;
using FastFrame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FastFrame.Database
{
    public abstract class BaseEntityMapping<T> : IEntityMapping<T> where T : class, IEntity
    { 
        public virtual void ModelCreating(ModelBuilder modelBuilder)
        {
            var entityType = typeof(T);
         
            var Entity = modelBuilder.Entity<T>();
            var currNameSpace = string.Join(",", entityType.Namespace.Split(new char[] { '.' }).Skip(2));
            Entity.ToTable($"{currNameSpace}_{entityType.Name}".ToLower());

            /*指定主键*/
            Entity.HasKey(x => x.Id);
            Entity.Property(x => x.Id).ValueGeneratedNever();

            /*过滤掉软删除的*/
            if (typeof(IHasSoftDelete).IsAssignableFrom(entityType))
            { 
                Entity.Property<bool>("isdeleted");
                Entity.HasQueryFilter(v => !EF.Property<bool>(v, "isdeleted"));
            }

           
            if (typeof(IHasTenant).IsAssignableFrom(entityType))
            {
                Entity.Property<string>("tenant_id"); 
            }

            foreach (var item in typeof(T).GetProperties())
            {
                if (item.GetCustomAttribute<NotMappedAttribute>() != null)
                    continue;

                /*索引ID*/
                if (item.Name.EndsWith("Id") && item.Name != "Id")
                    Entity.HasIndex(item.Name).HasName($"Index_{entityType.Name}_{item.Name}");

                var propType = T4Help.GetNullableType(item.PropertyType);

                var prop = modelBuilder.Entity<T>().Property(item.Name).HasColumnName(item.Name.ToLower());
                if (propType == typeof(string))
                {
                    /*所有字符串,指定为unicode*/
                    prop.IsUnicode();

                    /*所有ID,指定长度为25*/
                    if (item.Name.EndsWith("Id"))
                    {
                        prop.HasMaxLength(25);
                    }

                    /*非ID字段，且没有指定长度的，默认200*/
                    else if (item.GetCustomAttribute<StringLengthAttribute>() == null && item.Name != "Content")
                    {
                        prop.HasMaxLength(200);
                    }
                     
                }

                if (propType.IsEnum)
                {
                    prop.HasConversion<string>().HasMaxLength(100);
                    var names = Enum.GetNames(propType);
                    if (names.Any())
                    {
                        var val = Enum.Parse(propType, names.First());
                        prop.HasDefaultValue(val);
                    }

                }

                if (propType == typeof(int))
                    prop.HasDefaultValue(0);

                if (propType == typeof(decimal))
                    prop.HasDefaultValue(0.0m); 
            }
        }
    }
}
