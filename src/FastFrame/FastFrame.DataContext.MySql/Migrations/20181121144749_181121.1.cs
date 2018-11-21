using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1811211 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dept_Id",
                table: "Basis_User",
                maxLength: 25,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "198d53127062d93f425c3358bf674dc3", "fbe30e729fd8204dda84508da6ddfebf" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dept_Id",
                table: "Basis_User");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "f804218b4ca32469efb17665d27f475d", "96e5cd4bb1c9a060018be6f14b437329" });
        }
    }
}
