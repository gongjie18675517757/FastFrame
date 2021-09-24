using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2109241 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "groupindex",
                table: "flow_flownodecond",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "条件组号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "groupindex",
                table: "flow_flownodecond");
        }
    }
}
