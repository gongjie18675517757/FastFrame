using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Basis_Dept",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Parent_Id = table.Column<string>(maxLength: 25, nullable: true),
                    EnCode = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Supervisor_Id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Dept", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_Employee",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EnCode = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    User_Id = table.Column<string>(maxLength: 25, nullable: true),
                    Dept_Id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_Foreign",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EntityId = table.Column<string>(maxLength: 25, nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 25, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 25, nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Foreign", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_Menu",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Parent_Id = table.Column<string>(maxLength: 25, nullable: true),
                    EnCode = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    Path = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_Organize",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Organize", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_QueryProgram",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModuleName = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IsPublic = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_QueryProgram", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_QueryProgramDetail",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SearchProgram_Id = table.Column<string>(maxLength: 25, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Compare = table.Column<string>(maxLength: 50, nullable: false),
                    Value = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_QueryProgramDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_User",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Account = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    EncryptionKey = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    HandIconId = table.Column<string>(maxLength: 25, nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsRoot = table.Column<bool>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Basis_User",
                columns: new[] { "Id", "Account", "Email", "EncryptionKey", "HandIconId", "IsAdmin", "IsDeleted", "IsDisabled", "IsRoot", "Name", "OrganizeId", "Password", "PhoneNumber" },
                values: new object[] { "001_root", "root", "gongjie@qq.com", "b0f095510a6028742d4ecae3f2fba96b", null, true, false, false, true, "超级管理员", "root", "72f16c05ccdec6ae3579294fd91fa7d7", "18675517757" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_Dept",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_Employee",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_EntityId",
                table: "Basis_Foreign",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_Foreign",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_Menu",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_Organize",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_QueryProgram",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_QueryProgramDetail",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_User",
                column: "OrganizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basis_Dept");

            migrationBuilder.DropTable(
                name: "Basis_Employee");

            migrationBuilder.DropTable(
                name: "Basis_Foreign");

            migrationBuilder.DropTable(
                name: "Basis_Menu");

            migrationBuilder.DropTable(
                name: "Basis_Organize");

            migrationBuilder.DropTable(
                name: "Basis_QueryProgram");

            migrationBuilder.DropTable(
                name: "Basis_QueryProgramDetail");

            migrationBuilder.DropTable(
                name: "Basis_User");
        }
    }
}
