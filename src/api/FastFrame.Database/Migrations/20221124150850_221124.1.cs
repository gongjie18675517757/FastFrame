using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFrame.Database.Migrations
{
    /// <inheritdoc />
    public partial class _2211241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "encode",
                table: "basis_role");

            migrationBuilder.AddColumn<string>(
                name: "super_id",
                table: "basis_role",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "上级角色")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "treecode",
                table: "basis_role",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "编码")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Super_Id",
                table: "basis_role",
                column: "super_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Role_Super_Id",
                table: "basis_role");

            migrationBuilder.DropColumn(
                name: "super_id",
                table: "basis_role");

            migrationBuilder.DropColumn(
                name: "treecode",
                table: "basis_role");

            migrationBuilder.AddColumn<string>(
                name: "encode",
                table: "basis_role",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "编码")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
