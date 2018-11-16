using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1811161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Host",
                table: "Basis_Organize");

            migrationBuilder.CreateTable(
                name: "Basis_OrganizeHost",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Host = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_OrganizeHost", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Basis_OrganizeHost",
                columns: new[] { "Id", "Host", "IsDeleted", "OrganizeId" },
                values: new object[] { "00F6P5G2VC2SAP1UJV7HTBYGB", "192.168.1.100:8081", false, "00F6P5G2VC2SAP1UJV7HTBYGU" });

            migrationBuilder.InsertData(
                table: "Basis_OrganizeHost",
                columns: new[] { "Id", "Host", "IsDeleted", "OrganizeId" },
                values: new object[] { "00F6P5G2VC2SAP1UJV7HTBYGc", "192.168.1.100:82", false, "00F6P5G2VC2SAP1UJV7HTBYGU" });

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "f2ddd9711d0c6c633acc2dbcc6f5aef3", "05ab47835362dc6509373865e21f54e8" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_OrganizeHost",
                column: "OrganizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basis_OrganizeHost");

            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "Basis_Organize",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Basis_Organize",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGU",
                column: "Host",
                value: "192.168.1.100:8081");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "a8ddba3bfd03736639d2af50ec03b2a6", "b40424eb4e24df54aa41d9783ec94d1c" });
        }
    }
}
