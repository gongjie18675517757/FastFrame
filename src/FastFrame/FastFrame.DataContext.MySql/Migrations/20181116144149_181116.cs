using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _181116 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "Account", "EncryptionKey", "OrganizeId", "Password" },
                values: new object[] { "admin", "a8ddba3bfd03736639d2af50ec03b2a6", "00F6P5G2VC2SAP1UJV7HTBYGU", "b40424eb4e24df54aa41d9783ec94d1c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "Account", "EncryptionKey", "OrganizeId", "Password" },
                values: new object[] { "root", "58e18a858ca6354da1fada8bcda65a11", "root", "0f14175a4502f6419a1ee3473da352d2" });
        }
    }
}
