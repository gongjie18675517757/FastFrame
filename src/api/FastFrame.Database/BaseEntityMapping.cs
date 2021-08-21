using FastFrame.Entity;
using FastFrame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
            //Debugger.Launch();

            var entityType = typeof(T);
            var entityBaseType = entityType.BaseType;

            /*是否继承表*/
            var isInheritTable = entityBaseType.IsClass && !entityBaseType.IsAbstract && typeof(IEntity).IsAssignableFrom(entityBaseType);

            /*算命名空间*/
            var currNameSpace = string.Join(",", entityType.Namespace.Split(new char[] { '.' }).Skip(2));


            if (!isInheritTable)
            {
                /*映射到表*/
                if (typeof(IEntity).IsAssignableFrom(entityType))
                {
                    /*指定主键*/
                    entityTypeBuilder.HasKey("Id");
                    entityTypeBuilder.Property("Id").ValueGeneratedNever();
                    entityTypeBuilder.ToTable($"{currNameSpace}_{entityType.Name}".ToLower());
                    entityTypeBuilder.HasComment(T4Help.GetClassSummary(entityType, AppDomain.CurrentDomain.BaseDirectory));
                }
                /*映射到视图*/
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
                    entityTypeBuilder.HasIndex("isdeleted").HasDatabaseName($"Index_{entityType.Name}_isdeleted");
                }

                /*多租户标记*/
                if (typeof(IHasTenant).IsAssignableFrom(entityType))
                {
                    entityTypeBuilder.Property<string>("tenant_id").HasMaxLength(25);
                    entityTypeBuilder.HasIndex("tenant_id").HasDatabaseName($"Index_{entityType.Name}_tenant_id");
                }
            }


            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetCustomAttribute<NotMappedAttribute>() != null)
                    continue;

                var propertyBuilder = entityTypeBuilder
                             .Property(property.Name)
                             .HasColumnName(property.Name.ToLower());

                /*如果父类型有，则跳出*/
                if (isInheritTable && entityBaseType.GetProperty(property.Name) != null)
                    continue;

                /*类型擦除Nullable<>*/
                var propType = T4Help.GetNullableType(property.PropertyType);

                /*为数据库添加注释*/
                propertyBuilder.HasComment(T4Help.GetPropertySummary(property, AppDomain.CurrentDomain.BaseDirectory));


                if (typeof(IEntity).IsAssignableFrom(entityType))
                {
                    /*索引ID*/
                    if (property.Name.EndsWith("Id") && property.Name != "Id")
                        entityTypeBuilder.HasIndex(property.Name).HasDatabaseName($"Index_{entityType.Name}_{property.Name}");

                    if (propType == typeof(string))
                    {
                        /*所有字符串,指定为unicode*/
                        propertyBuilder.IsUnicode();

                        /*所有ID,指定长度为25*/
                        if (property.Name.EndsWith("Id"))
                        {
                            propertyBuilder.HasMaxLength(25);
                        }
                        /*未指定长度时默认设置为200(约定Content结尾的为富文本)*/
                        else if (property.GetCustomAttribute<StringLengthAttribute>() == null &&
                                 property.GetCustomAttribute<MaxLengthAttribute>() == null &&
                                 !property.Name.EndsWith("Content"))
                        {
                            propertyBuilder.HasMaxLength(200);
                        }

                        /*长度超过4000时，改为不限长*/
                        if (propertyBuilder.Metadata.GetMaxLength() >= 4000)
                        {
                            propertyBuilder.HasColumnName("nvarchar(max)");
                        }
                    }
                }

                /*指定精度*/
                if (propType == typeof(decimal))
                {
                    propertyBuilder.HasPrecision(10, 2);
                }
                /*转换枚举*/
                else if (propType.IsEnum)
                {
                    propertyBuilder.HasConversion<string>().HasMaxLength(50);
                }
            }

            /*处理继续承的关键字*/
            if (!isInheritTable && entityTypeBuilder.Metadata.GetProperties().Any(v => v.Name == "Discriminator"))
            {
                entityTypeBuilder
                   .Property("Discriminator")
                   .HasMaxLength(50);

                entityTypeBuilder.HasIndex("Discriminator").HasDatabaseName($"Index_{entityType.Name}_Discriminator");
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
