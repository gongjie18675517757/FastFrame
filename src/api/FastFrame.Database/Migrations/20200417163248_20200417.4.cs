using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _202004174 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "contenttype",
                table: "basis_resource",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50) CHARACTER SET utf8mb4",
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "contenttype",
                table: "basis_resource",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
