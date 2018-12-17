using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1812162 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Parent_Id",
                table: "Basis_Tenant",
                maxLength: 25,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Basis_Tenant",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGU",
                columns: new[] { "CanHaveChildren", "Parent_Id" },
                values: new object[] { true, "" });

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "7e363ac77b87dce8d86a36c411816904", "9e20855150f82e62e42bb3cffc66b705" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parent_Id",
                table: "Basis_Tenant");

            migrationBuilder.UpdateData(
                table: "Basis_Tenant",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGU",
                column: "CanHaveChildren",
                value: false);

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "f77bd12eaa5c3a79a391a6c1d718802b", "d2fb917f9ae0cc1e0ca1c80b9d12e6d7" });
        }
    }
}
