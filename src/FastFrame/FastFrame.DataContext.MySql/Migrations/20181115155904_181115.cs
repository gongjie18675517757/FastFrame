using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _181115 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "001_root");

            migrationBuilder.AddColumn<string>(
                name: "EnCode",
                table: "Basis_Organize",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "Basis_Organize",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Basis_Organize",
                columns: new[] { "Id", "EnCode", "Host", "IsDeleted", "Name", "OrganizeId" },
                values: new object[] { "00F6P5G2VC2SAP1UJV7HTBYGU", "default", "192.168.1.100:8081", false, "默认组织", "00F6P5G2VC2SAP1UJV7HTBYGU" });

            migrationBuilder.InsertData(
                table: "Basis_User",
                columns: new[] { "Id", "Account", "Email", "EncryptionKey", "HandIconId", "IsAdmin", "IsDeleted", "IsDisabled", "IsRoot", "Name", "OrganizeId", "Password", "PhoneNumber" },
                values: new object[] { "00F6P5G2VC2SAP1UJV7HTBYGA", "root", "gongjie@qq.com", "58e18a858ca6354da1fada8bcda65a11", null, true, false, false, true, "超级管理员", "root", "0f14175a4502f6419a1ee3473da352d2", "18675517757" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Basis_Organize",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGU");

            migrationBuilder.DeleteData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA");

            migrationBuilder.DropColumn(
                name: "EnCode",
                table: "Basis_Organize");

            migrationBuilder.DropColumn(
                name: "Host",
                table: "Basis_Organize");

            migrationBuilder.InsertData(
                table: "Basis_User",
                columns: new[] { "Id", "Account", "Email", "EncryptionKey", "HandIconId", "IsAdmin", "IsDeleted", "IsDisabled", "IsRoot", "Name", "OrganizeId", "Password", "PhoneNumber" },
                values: new object[] { "001_root", "root", "gongjie@qq.com", "2d3585e33839fd2e2cff6d7bdf2ef265", null, true, false, false, true, "超级管理员", "root", "28bb188ee8eba450ca8b2955a6ec92f3", "18675517757" });
        }
    }
}
