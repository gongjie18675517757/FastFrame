using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1906241 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_User_Dept_Id",
                table: "basis_user");

            migrationBuilder.DropColumn(
                name: "dept_id",
                table: "basis_user");

            migrationBuilder.AddColumn<string>(
                name: "dept_id",
                table: "basis_deptmember",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_Dept_Id",
                table: "basis_deptmember",
                column: "dept_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_DeptMember_Dept_Id",
                table: "basis_deptmember");

            migrationBuilder.DropColumn(
                name: "dept_id",
                table: "basis_deptmember");

            migrationBuilder.AddColumn<string>(
                name: "dept_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "Index_User_Dept_Id",
                table: "basis_user",
                column: "dept_id");
        }
    }
}
