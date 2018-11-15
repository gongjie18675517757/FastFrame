﻿// <auto-generated />
using System;
using FastFrame.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastFrame.Database.Migrations
{
    [DbContext(typeof(DataBase))]
    partial class DataBaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FastFrame.Entity.Basis.Dept", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("EnCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("OrganizeId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Parent_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Supervisor_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("OrganizeId")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_Dept");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Dept_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("EnCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.Property<string>("OrganizeId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.Property<string>("User_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("OrganizeId")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_Employee");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.Foreign", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreateUserId")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("EntityId")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("ModifyTime");

                    b.Property<string>("ModifyUserId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("OrganizeId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("EntityId")
                        .HasName("Index_EntityId");

                    b.HasIndex("OrganizeId")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_Foreign");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.Menu", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("EnCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Icon")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("OrganizeId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Parent_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Path")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("OrganizeId")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_Menu");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.Organize", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("EnCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("OrganizeId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("OrganizeId")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_Organize");

                    b.HasData(
                        new { Id = "00F6P5G2VC2SAP1UJV7HTBYGU", EnCode = "default", Host = "192.168.1.100:8081", IsDeleted = false, Name = "默认组织", OrganizeId = "00F6P5G2VC2SAP1UJV7HTBYGU" }
                    );
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.QueryProgram", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPublic");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("OrganizeId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("OrganizeId")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_QueryProgram");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.QueryProgramDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Compare")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("OrganizeId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("SearchProgram_Id")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("OrganizeId")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_QueryProgramDetail");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.Resource", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("ContentType")
                        .HasMaxLength(200)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .IsUnicode(true);

                    b.Property<string>("OrganizeId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Path")
                        .HasMaxLength(150)
                        .IsUnicode(true);

                    b.Property<long>("Size");

                    b.HasKey("Id");

                    b.HasIndex("OrganizeId")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_Resource");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("EncryptionKey")
                        .IsRequired()
                        .HasMaxLength(36)
                        .IsUnicode(true);

                    b.Property<string>("HandIconId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsDisabled");

                    b.Property<bool>("IsRoot");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("OrganizeId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("OrganizeId")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_User");

                    b.HasData(
                        new { Id = "00F6P5G2VC2SAP1UJV7HTBYGA", Account = "root", Email = "gongjie@qq.com", EncryptionKey = "58e18a858ca6354da1fada8bcda65a11", IsAdmin = true, IsDeleted = false, IsDisabled = false, IsRoot = true, Name = "超级管理员", OrganizeId = "root", Password = "0f14175a4502f6419a1ee3473da352d2", PhoneNumber = "18675517757" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
