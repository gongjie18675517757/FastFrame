using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1903302 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "resource_id",
                table: "basis_notify",
                maxLength: 25,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "encryptionkey", "password" },
                values: new object[] { "0756803045b3dd96ace39af25ab0d83a", "77f688a672ed34adb885444b93ddc77e" });

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Resource_Id",
                table: "basis_notify",
                column: "resource_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Notify_Resource_Id",
                table: "basis_notify");

            migrationBuilder.DropColumn(
                name: "resource_id",
                table: "basis_notify");

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "encryptionkey", "password" },
                values: new object[] { "d84fbfd1b6709f99d600cedc23579891", "0f9b3cbb1734efe2018bdd562f13f0a2" });
        }
    }
}
