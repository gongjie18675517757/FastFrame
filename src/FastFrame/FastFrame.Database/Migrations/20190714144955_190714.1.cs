using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1907141 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_GroupMessage_Tenant_Id",
                table: "chat_groupmessage");

            migrationBuilder.DropIndex(
                name: "Index_FriendMessage_Tenant_Id",
                table: "chat_friendmessage");

            migrationBuilder.DropIndex(
                name: "Index_Email_Tenant_Id",
                table: "chat_email");

            migrationBuilder.DropIndex(
                name: "Index_Notify_Type_Id",
                table: "basis_notify");

            migrationBuilder.DropColumn(
                name: "GroupMessage_tenant_id",
                table: "chat_groupmessage");

            migrationBuilder.DropColumn(
                name: "FriendMessage_tenant_id",
                table: "chat_friendmessage");

            migrationBuilder.DropColumn(
                name: "Email_tenant_id",
                table: "chat_email");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "basis_notify");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_groupmessage",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_friendmessage",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_email",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type_value",
                table: "basis_notify",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type_value",
                table: "basis_notify");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupMessage_tenant_id",
                table: "chat_groupmessage",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_friendmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendMessage_tenant_id",
                table: "chat_friendmessage",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_email",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email_tenant_id",
                table: "chat_email",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type_id",
                table: "basis_notify",
                maxLength: 25,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "Index_GroupMessage_Tenant_Id",
                table: "chat_groupmessage",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_Tenant_Id",
                table: "chat_friendmessage",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Email_Tenant_Id",
                table: "chat_email",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Type_Id",
                table: "basis_notify",
                column: "type_id");
        }
    }
}
