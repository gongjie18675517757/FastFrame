using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1812161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Basis_Tenant",
                newName: "FullName");

            migrationBuilder.AddColumn<bool>(
                name: "CanHaveChildren",
                table: "Basis_Tenant",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UrlMark",
                table: "Basis_Tenant",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Basis_Tenant",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGU",
                column: "UrlMark",
                value: "");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "f77bd12eaa5c3a79a391a6c1d718802b", "d2fb917f9ae0cc1e0ca1c80b9d12e6d7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanHaveChildren",
                table: "Basis_Tenant");

            migrationBuilder.DropColumn(
                name: "UrlMark",
                table: "Basis_Tenant");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Basis_Tenant",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "af2dd6afec52cfa781a03057f182e316", "e383510c3d87d18360c7a950ab325f50" });
        }
    }
}
