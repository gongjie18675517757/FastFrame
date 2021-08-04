using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2108031 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "basis_enumitem",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "编码")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "basis_enumitem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "排序");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "order",
                table: "basis_enumitem");
        }
    }
}
