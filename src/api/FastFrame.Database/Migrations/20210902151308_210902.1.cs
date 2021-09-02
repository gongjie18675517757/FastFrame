using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2109021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "childcount",
                table: "basis_tenant");

            migrationBuilder.DropColumn(
                name: "childcount",
                table: "basis_meidia");

            migrationBuilder.DropColumn(
                name: "childcount",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "childcount",
                table: "basis_dept");

            migrationBuilder.AddColumn<string>(
                name: "ipaddress",
                table: "basis_loginlog",
                type: "varchar(45)",
                nullable: true,
                comment: "IP")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ipaddress",
                table: "basis_loginlog");

            migrationBuilder.AddColumn<int>(
                name: "childcount",
                table: "basis_tenant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "子节点数量");

            migrationBuilder.AddColumn<int>(
                name: "childcount",
                table: "basis_meidia",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "子节点数量");

            migrationBuilder.AddColumn<int>(
                name: "childcount",
                table: "basis_enumitem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "子节点数量");

            migrationBuilder.AddColumn<int>(
                name: "childcount",
                table: "basis_dept",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "子节点数量");
        }
    }
}
