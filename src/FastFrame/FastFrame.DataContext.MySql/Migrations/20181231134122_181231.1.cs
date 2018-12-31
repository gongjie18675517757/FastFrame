using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1812311 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basis_Foreign");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "CMS_Meidia",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "CMS_Meidia",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "CMS_Meidia",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "CMS_Meidia",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "CMS_ArticleCategory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "CMS_ArticleCategory",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "CMS_ArticleCategory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "CMS_ArticleCategory",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "CMS_Article",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "CMS_Article",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "CMS_Article",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "CMS_Article",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Chat_Group",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "Chat_Group",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Chat_Group",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "Chat_Group",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Basis_User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "Basis_User",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Basis_User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "Basis_User",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Basis_RolePermission",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "Basis_RolePermission",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Basis_RolePermission",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "Basis_RolePermission",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Basis_RoleMember",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "Basis_RoleMember",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Basis_RoleMember",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "Basis_RoleMember",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Basis_Role",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "Basis_Role",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Basis_Role",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "Basis_Role",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Basis_Resource",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "Basis_Resource",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Basis_Resource",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "Basis_Resource",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Basis_Menu",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "Basis_Menu",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Basis_Menu",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "Basis_Menu",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Basis_Employee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "Basis_Employee",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Basis_Employee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "Basis_Employee",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Basis_Dept",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Create_User_Id",
                table: "Basis_Dept",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Basis_Dept",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Modify_User_Id",
                table: "Basis_Dept",
                maxLength: 25,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "f0cf2a0cba6b28493e1bee52a79dca22", "c133c477f7729b68df0acdbd7f51aff1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "CMS_Meidia");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "CMS_Meidia");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "CMS_Meidia");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "CMS_Meidia");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "CMS_ArticleCategory");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "CMS_ArticleCategory");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "CMS_ArticleCategory");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "CMS_ArticleCategory");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "CMS_Article");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "CMS_Article");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "CMS_Article");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "CMS_Article");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Chat_Group");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "Chat_Group");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Chat_Group");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "Chat_Group");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Basis_User");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "Basis_User");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Basis_User");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "Basis_User");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Basis_RolePermission");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "Basis_RolePermission");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Basis_RolePermission");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "Basis_RolePermission");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Basis_RoleMember");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "Basis_RoleMember");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Basis_RoleMember");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "Basis_RoleMember");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Basis_Role");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "Basis_Role");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Basis_Role");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "Basis_Role");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Basis_Resource");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "Basis_Resource");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Basis_Resource");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "Basis_Resource");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Basis_Menu");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "Basis_Menu");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Basis_Menu");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "Basis_Menu");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Basis_Employee");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "Basis_Employee");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Basis_Employee");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "Basis_Employee");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Basis_Dept");

            migrationBuilder.DropColumn(
                name: "Create_User_Id",
                table: "Basis_Dept");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Basis_Dept");

            migrationBuilder.DropColumn(
                name: "Modify_User_Id",
                table: "Basis_Dept");

            migrationBuilder.CreateTable(
                name: "Basis_Foreign",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 25, nullable: true),
                    EntityId = table.Column<string>(maxLength: 25, nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Foreign", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "7e363ac77b87dce8d86a36c411816904", "9e20855150f82e62e42bb3cffc66b705" });

            migrationBuilder.CreateIndex(
                name: "Index_EntityId",
                table: "Basis_Foreign",
                column: "EntityId");
        }
    }
}
