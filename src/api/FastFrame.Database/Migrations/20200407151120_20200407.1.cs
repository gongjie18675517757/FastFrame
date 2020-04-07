using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _202004071 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Dept_Supervisor_Id",
                table: "basis_dept");

            migrationBuilder.DropColumn(
                name: "supervisor_id",
                table: "basis_dept");

            migrationBuilder.AddColumn<bool>(
                name: "isadmin",
                table: "basis_role",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdefault",
                table: "basis_role",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "basis_role",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00fm5yfgq3q893ylku6uzb57i",
                columns: new[] { "encryptionkey", "password" },
                values: new object[] { "7d9d7edd6727912ce10b976818dd2856", "9557847e0632e2f167a143b7ab3d668a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isadmin",
                table: "basis_role");

            migrationBuilder.DropColumn(
                name: "isdefault",
                table: "basis_role");

            migrationBuilder.DropColumn(
                name: "remarks",
                table: "basis_role");

            migrationBuilder.AddColumn<string>(
                name: "supervisor_id",
                table: "basis_dept",
                type: "varchar(25) CHARACTER SET utf8mb4",
                maxLength: 25,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00fm5yfgq3q893ylku6uzb57i",
                columns: new[] { "encryptionkey", "password" },
                values: new object[] { "0ee3dcf0e832334f63876a30b45fdece", "000000" });

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Supervisor_Id",
                table: "basis_dept",
                column: "supervisor_id");
        }
    }
}
