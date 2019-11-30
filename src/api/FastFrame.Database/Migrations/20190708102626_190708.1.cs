using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1907081 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "basis_permission",
                newName: "super_id");

            migrationBuilder.RenameIndex(
                name: "Index_Permission_Parent_Id",
                table: "basis_permission",
                newName: "Index_Permission_Super_Id");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "basis_dept",
                newName: "super_id");

            migrationBuilder.RenameIndex(
                name: "Index_Dept_Parent_Id",
                table: "basis_dept",
                newName: "Index_Dept_Super_Id");

            migrationBuilder.AddColumn<string>(
                name: "genderarray",
                table: "basis_dept",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "typeidarray",
                table: "basis_dept",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "genderarray",
                table: "basis_dept");

            migrationBuilder.DropColumn(
                name: "typeidarray",
                table: "basis_dept");

            migrationBuilder.RenameColumn(
                name: "super_id",
                table: "basis_permission",
                newName: "parent_id");

            migrationBuilder.RenameIndex(
                name: "Index_Permission_Super_Id",
                table: "basis_permission",
                newName: "Index_Permission_Parent_Id");

            migrationBuilder.RenameColumn(
                name: "super_id",
                table: "basis_dept",
                newName: "parent_id");

            migrationBuilder.RenameIndex(
                name: "Index_Dept_Super_Id",
                table: "basis_dept",
                newName: "Index_Dept_Parent_Id");
        }
    }
}
