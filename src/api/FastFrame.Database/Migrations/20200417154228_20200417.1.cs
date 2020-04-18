using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _202004171 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contenttype",
                table: "basis_meidia");

            migrationBuilder.DropColumn(
                name: "href",
                table: "basis_meidia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "contenttype",
                table: "basis_meidia",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "href",
                table: "basis_meidia",
                type: "varchar(200) CHARACTER SET utf8mb4",
                maxLength: 200,
                nullable: true);
        }
    }
}
