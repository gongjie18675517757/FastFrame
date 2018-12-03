using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1812022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotifyTarget",
                table: "NotifyTarget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notify",
                table: "Notify");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageTarget",
                table: "MessageTarget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMessage",
                table: "GroupMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupManager",
                table: "GroupManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendMessage",
                table: "FriendMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTarget",
                table: "EmailTarget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailContent",
                table: "EmailContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailAnnex",
                table: "EmailAnnex");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Email",
                table: "Email");

            migrationBuilder.RenameTable(
                name: "NotifyTarget",
                newName: "Chat_NotifyTarget");

            migrationBuilder.RenameTable(
                name: "Notify",
                newName: "Chat_Notify");

            migrationBuilder.RenameTable(
                name: "MessageTarget",
                newName: "Chat_MessageTarget");

            migrationBuilder.RenameTable(
                name: "GroupMessage",
                newName: "Chat_GroupMessage");

            migrationBuilder.RenameTable(
                name: "GroupManager",
                newName: "Chat_GroupManager");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Chat_Group");

            migrationBuilder.RenameTable(
                name: "FriendMessage",
                newName: "Chat_FriendMessage");

            migrationBuilder.RenameTable(
                name: "EmailTarget",
                newName: "Chat_EmailTarget");

            migrationBuilder.RenameTable(
                name: "EmailContent",
                newName: "Chat_EmailContent");

            migrationBuilder.RenameTable(
                name: "EmailAnnex",
                newName: "Chat_EmailAnnex");

            migrationBuilder.RenameTable(
                name: "Email",
                newName: "Chat_Email");

            migrationBuilder.AlterColumn<string>(
                name: "To_Id",
                table: "Chat_NotifyTarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notify_Id",
                table: "Chat_NotifyTarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_NotifyTarget",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Id",
                table: "Chat_Notify",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_Notify",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "To_Id",
                table: "Chat_MessageTarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message_Id",
                table: "Chat_MessageTarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_MessageTarget",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Id",
                table: "Chat_GroupMessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Resource_Id",
                table: "Chat_GroupMessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Group_Id",
                table: "Chat_GroupMessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "From_Id",
                table: "Chat_GroupMessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Chat_GroupMessage",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Chat_GroupMessage",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_GroupMessage",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "User_Id",
                table: "Chat_GroupManager",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Group_Id",
                table: "Chat_GroupManager",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_GroupManager",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Id",
                table: "Chat_Group",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LordUser_Id",
                table: "Chat_Group",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HandIcon_Id",
                table: "Chat_Group",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_Group",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Id",
                table: "Chat_FriendMessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Resource_Id",
                table: "Chat_FriendMessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "From_Id",
                table: "Chat_FriendMessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Chat_FriendMessage",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Chat_FriendMessage",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_FriendMessage",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "To_Id",
                table: "Chat_EmailTarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email_Id",
                table: "Chat_EmailTarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Chat_EmailTarget",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_EmailTarget",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email_Id",
                table: "Chat_EmailContent",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_EmailContent",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Resource_Id",
                table: "Chat_EmailAnnex",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email_Id",
                table: "Chat_EmailAnnex",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_EmailAnnex",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Id",
                table: "Chat_Email",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Replay_Email_Id",
                table: "Chat_Email",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FromUser_Id",
                table: "Chat_Email",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chat_Email",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_NotifyTarget",
                table: "Chat_NotifyTarget",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_Notify",
                table: "Chat_Notify",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_MessageTarget",
                table: "Chat_MessageTarget",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_GroupMessage",
                table: "Chat_GroupMessage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_GroupManager",
                table: "Chat_GroupManager",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_Group",
                table: "Chat_Group",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_FriendMessage",
                table: "Chat_FriendMessage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_EmailTarget",
                table: "Chat_EmailTarget",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_EmailContent",
                table: "Chat_EmailContent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_EmailAnnex",
                table: "Chat_EmailAnnex",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat_Email",
                table: "Chat_Email",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "15b9e245754a720667d3fb1f939345a9", "90e028f3a5854186fbe858daf9e02072" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Chat_Notify",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Chat_GroupMessage",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Chat_Group",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Chat_FriendMessage",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Chat_Email",
                column: "Tenant_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_NotifyTarget",
                table: "Chat_NotifyTarget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_Notify",
                table: "Chat_Notify");

            migrationBuilder.DropIndex(
                name: "Index_OrganizeId",
                table: "Chat_Notify");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_MessageTarget",
                table: "Chat_MessageTarget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_GroupMessage",
                table: "Chat_GroupMessage");

            migrationBuilder.DropIndex(
                name: "Index_OrganizeId",
                table: "Chat_GroupMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_GroupManager",
                table: "Chat_GroupManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_Group",
                table: "Chat_Group");

            migrationBuilder.DropIndex(
                name: "Index_OrganizeId",
                table: "Chat_Group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_FriendMessage",
                table: "Chat_FriendMessage");

            migrationBuilder.DropIndex(
                name: "Index_OrganizeId",
                table: "Chat_FriendMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_EmailTarget",
                table: "Chat_EmailTarget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_EmailContent",
                table: "Chat_EmailContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_EmailAnnex",
                table: "Chat_EmailAnnex");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat_Email",
                table: "Chat_Email");

            migrationBuilder.DropIndex(
                name: "Index_OrganizeId",
                table: "Chat_Email");

            migrationBuilder.RenameTable(
                name: "Chat_NotifyTarget",
                newName: "NotifyTarget");

            migrationBuilder.RenameTable(
                name: "Chat_Notify",
                newName: "Notify");

            migrationBuilder.RenameTable(
                name: "Chat_MessageTarget",
                newName: "MessageTarget");

            migrationBuilder.RenameTable(
                name: "Chat_GroupMessage",
                newName: "GroupMessage");

            migrationBuilder.RenameTable(
                name: "Chat_GroupManager",
                newName: "GroupManager");

            migrationBuilder.RenameTable(
                name: "Chat_Group",
                newName: "Group");

            migrationBuilder.RenameTable(
                name: "Chat_FriendMessage",
                newName: "FriendMessage");

            migrationBuilder.RenameTable(
                name: "Chat_EmailTarget",
                newName: "EmailTarget");

            migrationBuilder.RenameTable(
                name: "Chat_EmailContent",
                newName: "EmailContent");

            migrationBuilder.RenameTable(
                name: "Chat_EmailAnnex",
                newName: "EmailAnnex");

            migrationBuilder.RenameTable(
                name: "Chat_Email",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "To_Id",
                table: "NotifyTarget",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notify_Id",
                table: "NotifyTarget",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "NotifyTarget",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Id",
                table: "Notify",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Notify",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "To_Id",
                table: "MessageTarget",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message_Id",
                table: "MessageTarget",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "MessageTarget",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Id",
                table: "GroupMessage",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Resource_Id",
                table: "GroupMessage",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Group_Id",
                table: "GroupMessage",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "From_Id",
                table: "GroupMessage",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "GroupMessage",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "GroupMessage",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "GroupMessage",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "User_Id",
                table: "GroupManager",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Group_Id",
                table: "GroupManager",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "GroupManager",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Id",
                table: "Group",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LordUser_Id",
                table: "Group",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HandIcon_Id",
                table: "Group",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Group",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Id",
                table: "FriendMessage",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Resource_Id",
                table: "FriendMessage",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "From_Id",
                table: "FriendMessage",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "FriendMessage",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "FriendMessage",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "FriendMessage",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "To_Id",
                table: "EmailTarget",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email_Id",
                table: "EmailTarget",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "EmailTarget",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "EmailTarget",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Email_Id",
                table: "EmailContent",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "EmailContent",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Resource_Id",
                table: "EmailAnnex",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email_Id",
                table: "EmailAnnex",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "EmailAnnex",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Id",
                table: "Email",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Replay_Email_Id",
                table: "Email",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FromUser_Id",
                table: "Email",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Email",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotifyTarget",
                table: "NotifyTarget",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notify",
                table: "Notify",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageTarget",
                table: "MessageTarget",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMessage",
                table: "GroupMessage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupManager",
                table: "GroupManager",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendMessage",
                table: "FriendMessage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTarget",
                table: "EmailTarget",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailContent",
                table: "EmailContent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailAnnex",
                table: "EmailAnnex",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Email",
                table: "Email",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "f4890afc86cb0cf57721be771e434223", "3d420c79ec0077d03c3f52151b106507" });
        }
    }
}
