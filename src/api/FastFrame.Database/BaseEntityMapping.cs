using FastFrame.Entity;
using FastFrame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FastFrame.Database
{
    public abstract class BaseEntityMapping<T> : IEntityMapping<T> where T : class, IQuery
    {
        public virtual void ModelCreating(ModelBuilder modelBuilder)
        {
            ModelEntityCreating(modelBuilder.Entity<T>());
        }

        public virtual void ModelEntityCreating(EntityTypeBuilder<T> entityTypeBuilder)
        {
            var entityType = typeof(T);
            var currNameSpace = string.Join(",", entityType.Namespace.Split(new char[] { '.' }).Skip(2)); 

            if (typeof(IEntity).IsAssignableFrom(entityType))
            {
                /*指定主键*/
                entityTypeBuilder.HasKey("Id");
                entityTypeBuilder.Property("Id").ValueGeneratedNever();
                entityTypeBuilder.ToTable($"{currNameSpace}_{entityType.Name}".ToLower());
            }
            else
            {
                entityTypeBuilder
                    .HasNoKey()
                    .ToView($"{currNameSpace}_{entityType.Name}".ToLower());
            }


            /*过滤掉软删除的*/
            if (typeof(IHasSoftDelete).IsAssignableFrom(entityType))
            {
                entityTypeBuilder.Property<bool>("isdeleted");
                entityTypeBuilder.HasQueryFilter(v => !EF.Property<bool>(v, "isdeleted"));
                entityTypeBuilder.HasIndex("isdeleted").HasName($"Index_{entityType.Name}_isdeleted");
            }

            if (typeof(IHasTenant).IsAssignableFrom(entityType))
            {
                entityTypeBuilder.Property<string>("tenant_id").HasMaxLength(25);
                entityTypeBuilder.HasIndex("tenant_id").HasName($"Index_{entityType.Name}_tenant_id");
            }

            foreach (var item in typeof(T).GetProperties())
            {
                if (item.GetCustomAttribute<NotMappedAttribute>() != null)
                    continue;

                var propType = T4Help.GetNullableType(item.PropertyType);

                var prop = entityTypeBuilder
                              .Property(item.Name)
                              .HasColumnName(item.Name.ToLower());

                /*索引ID*/
                if (typeof(IEntity).IsAssignableFrom(entityType))
                {
                    if (item.Name.EndsWith("Id") && item.Name != "Id")
                        entityTypeBuilder.HasIndex(item.Name).HasName($"Index_{entityType.Name}_{item.Name}");

                    if (propType == typeof(string))
                    {
                        /*所有字符串,指定为unicode*/
                        prop.IsUnicode();

                        /*所有ID,指定长度为25*/
                        if (item.Name.EndsWith("Id"))
                        {
                            prop.HasMaxLength(25);
                        }
                    }
                }
                if (propType == typeof(decimal))
                {
                    prop.HasColumnType("decimal(10,2)");
                }

                else if (propType.IsEnum)
                {
                    prop.HasConversion<string>().HasMaxLength(50);
                }
            }
        }

        public void ConvertEnumArray<TEnum>(EntityTypeBuilder<T> typeBuilder, string name)
        {
            var converter = new ValueConverter<TEnum[], string>(
                   v => string.Join(",", v ?? Array.Empty<TEnum>()),
                   v => (v ?? "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(r => (TEnum)Enum.Parse(typeof(TEnum), r))
                             .ToArray()
                 );

            typeBuilder
                .Property(name)
                .HasMaxLength(500)
                .IsUnicode()
                .HasConversion(converter);
        }


        public void ConvertStringArray(EntityTypeBuilder<T> typeBuilder, string name)
        {
            var converter = new ValueConverter<string[], string>(
                   v => string.Join(",", v ?? Array.Empty<string>()),
                   v => (v ?? "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                             .ToArray()
                 );

            typeBuilder
                .Property(name)
                .HasMaxLength(500)
                .IsUnicode()
                .HasConversion(converter);
        }
    }
}
