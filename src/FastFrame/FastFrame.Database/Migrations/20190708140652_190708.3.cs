using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1907083 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "genderarray",
                table: "basis_dept");

            migrationBuilder.DropColumn(
                name: "typeidarray",
                table: "basis_dept");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "genderarray",
                table: "basis_dept",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "typeidarray",
                table: "basis_dept",
                maxLength: 500,
                nullable: true);
        }
    }
}
