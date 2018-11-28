﻿// <auto-generated />
using System;
using FastFrame.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastFrame.Database.Migrations
{
    [DbContext(typeof(DataBase))]
    [Migration("20181128145134_181128.1")]
    partial class _1811281
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Parent_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Supervisor_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
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

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("User_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
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

                    b.Property<DateTime>("ModifyTime");

                    b.Property<string>("ModifyUserId")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("EntityId")
                        .HasName("Index_EntityId");

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

                    b.Property<string>("Parent_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Path")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_Menu");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.Permission", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("EnCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Parent_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_Permission");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.QueryProgram", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<bool>("IsPublic");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("User_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("QueryProgram_Id")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

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

                    b.Property<string>("Path")
                        .HasMaxLength(150)
                        .IsUnicode(true);

                    b.Property<long>("Size");

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_Resource");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.Role", b =>
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

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_Role");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.RoleMember", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Role_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("User_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_RoleMember");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.RolePermission", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Permission_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Role_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_RolePermission");
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.Tenant", b =>
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

                    b.HasKey("Id");

                    b.ToTable("Basis_Tenant");

                    b.HasData(
                        new { Id = "00F6P5G2VC2SAP1UJV7HTBYGU", EnCode = "default", IsDeleted = false, Name = "默认组织" }
                    );
                });

            modelBuilder.Entity("FastFrame.Entity.Basis.TenantHost", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_TenantHost");

                    b.HasData(
                        new { Id = "00F6P5G2VC2SAP1UJV7HTBYGB", Host = "192.168.1.100:8081", Tenant_Id = "00F6P5G2VC2SAP1UJV7HTBYGU" },
                        new { Id = "00F6P5G2VC2SAP1UJV7HTBYGc", Host = "192.168.1.100:82", Tenant_Id = "00F6P5G2VC2SAP1UJV7HTBYGU" }
                    );
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

                    b.Property<string>("Dept_Id")
                        .HasMaxLength(25)
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

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("Basis_User");

                    b.HasData(
                        new { Id = "00F6P5G2VC2SAP1UJV7HTBYGA", Account = "admin", Email = "gongjie@qq.com", EncryptionKey = "350c4281aeee46b7981d6a0d2bc9a7c1", IsAdmin = true, IsDeleted = false, IsDisabled = false, IsRoot = true, Name = "超级管理员", Password = "159e48092577ed41d493467026a63ed9", PhoneNumber = "18675517757", Tenant_Id = "00F6P5G2VC2SAP1UJV7HTBYGU" }
                    );
                });

            modelBuilder.Entity("FastFrame.Entity.CMS.Article", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("ArticleCategory_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("ArticleContent_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsRelease");

                    b.Property<int>("LookCount");

                    b.Property<string>("Summarize")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Thumbnail_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("CMS_Article");
                });

            modelBuilder.Entity("FastFrame.Entity.CMS.ArticleCategory", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Parent_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Url")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("CMS_ArticleCategory");
                });

            modelBuilder.Entity("FastFrame.Entity.CMS.ArticleContent", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Content")
                        .IsRequired()
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("CMS_ArticleContent");
                });

            modelBuilder.Entity("FastFrame.Entity.CMS.Meidia", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Href")
                        .HasMaxLength(200)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsFolder");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Parent_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Resource_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<string>("Tenant_Id")
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id")
                        .HasName("Index_OrganizeId");

                    b.ToTable("CMS_Meidia");
                });
#pragma warning restore 612, 618
        }
    }
}
