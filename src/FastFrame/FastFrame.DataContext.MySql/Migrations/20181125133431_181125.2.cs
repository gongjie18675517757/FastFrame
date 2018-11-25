using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1811252 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFolder",
                table: "CMS_Meidia",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Parent_Id",
                table: "CMS_Meidia",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resource_Id",
                table: "CMS_Meidia",
                maxLength: 25,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "1e27ea4fb296b2378d01da8b3fbddb4b", "20d5af8275530d76e0ef048944ed94e7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFolder",
                table: "CMS_Meidia");

            migrationBuilder.DropColumn(
                name: "Parent_Id",
                table: "CMS_Meidia");

            migrationBuilder.DropColumn(
                name: "Resource_Id",
                table: "CMS_Meidia");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "6274d890dd46fdcb5ae31bc85e2f5221", "20bda9db5c100d25e228ad732c8443b4" });
        }
    }
}
