using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1811041 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Basis_Resource",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Size = table.Column<long>(nullable: false),
                    Path = table.Column<string>(maxLength: 150, nullable: true),
                    ContentType = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Resource", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "001_root",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "2d3585e33839fd2e2cff6d7bdf2ef265", "28bb188ee8eba450ca8b2955a6ec92f3" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_Resource",
                column: "OrganizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basis_Resource");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "001_root",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "b0f095510a6028742d4ecae3f2fba96b", "72f16c05ccdec6ae3579294fd91fa7d7" });
        }
    }
}
