using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1906242 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "chat_groupmessage",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldDefaultValue: "Text");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "chat_friendmessage",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldDefaultValue: "Text");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "chat_emailtarget",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldDefaultValue: "To");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "chat_groupmessage",
                maxLength: 100,
                nullable: false,
                defaultValue: "Text",
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "chat_friendmessage",
                maxLength: 100,
                nullable: false,
                defaultValue: "Text",
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "chat_emailtarget",
                maxLength: 100,
                nullable: false,
                defaultValue: "To",
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
