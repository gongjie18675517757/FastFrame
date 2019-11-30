using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1907051 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enumname",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "enumvalue",
                table: "basis_enumitem");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "basis_enumitem",
                newName: "super_id");

            migrationBuilder.RenameIndex(
                name: "Index_EnumItem_Parent_Id",
                table: "basis_enumitem",
                newName: "Index_EnumItem_Super_Id");

            migrationBuilder.AddColumn<string>(
                name: "type_id",
                table: "basis_notify",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "key",
                table: "basis_enumitem",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "basis_enumitem",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Type_Id",
                table: "basis_notify",
                column: "type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Notify_Type_Id",
                table: "basis_notify");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "basis_notify");

            migrationBuilder.DropColumn(
                name: "key",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "value",
                table: "basis_enumitem");

            migrationBuilder.RenameColumn(
                name: "super_id",
                table: "basis_enumitem",
                newName: "parent_id");

            migrationBuilder.RenameIndex(
                name: "Index_EnumItem_Super_Id",
                table: "basis_enumitem",
                newName: "Index_EnumItem_Parent_Id");

            migrationBuilder.AddColumn<string>(
                name: "enumname",
                table: "basis_enumitem",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "enumvalue",
                table: "basis_enumitem",
                maxLength: 200,
                nullable: true);
        }
    }
}
