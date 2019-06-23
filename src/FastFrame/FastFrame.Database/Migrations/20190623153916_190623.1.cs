using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1906231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Group_Tenant_Id",
                table: "chat_group");

            migrationBuilder.DropIndex(
                name: "Index_User_Tenant_Id",
                table: "basis_user");

            migrationBuilder.DropIndex(
                name: "Index_RolePermission_Create_User_Id",
                table: "basis_rolepermission");

            migrationBuilder.DropIndex(
                name: "Index_RolePermission_Modify_User_Id",
                table: "basis_rolepermission");

            migrationBuilder.DropIndex(
                name: "Index_RolePermission_Tenant_Id",
                table: "basis_rolepermission");

            migrationBuilder.DropIndex(
                name: "Index_RoleMember_Create_User_Id",
                table: "basis_rolemember");

            migrationBuilder.DropIndex(
                name: "Index_RoleMember_Modify_User_Id",
                table: "basis_rolemember");

            migrationBuilder.DropIndex(
                name: "Index_RoleMember_Tenant_Id",
                table: "basis_rolemember");

            migrationBuilder.DropIndex(
                name: "Index_Role_Tenant_Id",
                table: "basis_role");

            migrationBuilder.DropIndex(
                name: "Index_Resource_Tenant_Id",
                table: "basis_resource");

            migrationBuilder.DropIndex(
                name: "Index_Permission_Tenant_Id",
                table: "basis_permission");

            migrationBuilder.DropIndex(
                name: "Index_Notify_Tenant_Id",
                table: "basis_notify");

            migrationBuilder.DropIndex(
                name: "Index_Meidia_Tenant_Id",
                table: "basis_meidia");

            migrationBuilder.DropIndex(
                name: "Index_EnumItem_Tenant_Id",
                table: "basis_enumitem");

            migrationBuilder.DropIndex(
                name: "Index_Dept_Tenant_Id",
                table: "basis_dept");

            migrationBuilder.DropColumn(
                name: "createtime",
                table: "basis_rolepermission");

            migrationBuilder.DropColumn(
                name: "create_user_id",
                table: "basis_rolepermission");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "basis_rolepermission");

            migrationBuilder.DropColumn(
                name: "modifytime",
                table: "basis_rolepermission");

            migrationBuilder.DropColumn(
                name: "modify_user_id",
                table: "basis_rolepermission");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "basis_rolepermission");

            migrationBuilder.DropColumn(
                name: "createtime",
                table: "basis_rolemember");

            migrationBuilder.DropColumn(
                name: "create_user_id",
                table: "basis_rolemember");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "basis_rolemember");

            migrationBuilder.DropColumn(
                name: "modifytime",
                table: "basis_rolemember");

            migrationBuilder.DropColumn(
                name: "modify_user_id",
                table: "basis_rolemember");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "basis_rolemember");

            migrationBuilder.AddColumn<string>(
                name: "GroupMessage_tenant_id",
                table: "chat_groupmessage",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_group",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendMessage_tenant_id",
                table: "chat_friendmessage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email_tenant_id",
                table: "chat_email",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_user",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_role",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_resource",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_permission",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_notify",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_meidia",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_enumitem",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "enumname",
                table: "basis_enumitem",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createtime",
                table: "basis_enumitem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "create_user_id",
                table: "basis_enumitem",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modifytime",
                table: "basis_enumitem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "modify_user_id",
                table: "basis_enumitem",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_dept",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "basis_deptmember",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    user_id = table.Column<string>(maxLength: 25, nullable: false),
                    ismanager = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_deptmember", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "basis_tenant",
                keyColumn: "id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGU",
                column: "urlmark",
                value: null);

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "createtime", "create_user_id", "encryptionkey", "modifytime", "modify_user_id", "password" },
                values: new object[] { new DateTime(2019, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "00F6P5G2VC2SAP1UJV7HTBYGA", "0ee3dcf0e832334f63876a30b45fdece", new DateTime(2019, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "00F6P5G2VC2SAP1UJV7HTBYGA", "123456" });

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Create_User_Id",
                table: "basis_enumitem",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Modify_User_Id",
                table: "basis_enumitem",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_User_Id",
                table: "basis_deptmember",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_deptmember");

            migrationBuilder.DropIndex(
                name: "Index_EnumItem_Create_User_Id",
                table: "basis_enumitem");

            migrationBuilder.DropIndex(
                name: "Index_EnumItem_Modify_User_Id",
                table: "basis_enumitem");

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
                name: "createtime",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "create_user_id",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "modifytime",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "modify_user_id",
                table: "basis_enumitem");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createtime",
                table: "basis_rolepermission",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "create_user_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "basis_rolepermission",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "modifytime",
                table: "basis_rolepermission",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "modify_user_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createtime",
                table: "basis_rolemember",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "create_user_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "basis_rolemember",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "modifytime",
                table: "basis_rolemember",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "modify_user_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_role",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_resource",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_permission",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_notify",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_enumitem",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "enumname",
                table: "basis_enumitem",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "basis_tenant",
                keyColumn: "id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGU",
                column: "urlmark",
                value: "");

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "createtime", "create_user_id", "encryptionkey", "modifytime", "modify_user_id", "password" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "0756803045b3dd96ace39af25ab0d83a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "77f688a672ed34adb885444b93ddc77e" });

            migrationBuilder.CreateIndex(
                name: "Index_Group_Tenant_Id",
                table: "chat_group",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_User_Tenant_Id",
                table: "basis_user",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Create_User_Id",
                table: "basis_rolepermission",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Modify_User_Id",
                table: "basis_rolepermission",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Tenant_Id",
                table: "basis_rolepermission",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_Create_User_Id",
                table: "basis_rolemember",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_Modify_User_Id",
                table: "basis_rolemember",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_Tenant_Id",
                table: "basis_rolemember",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Tenant_Id",
                table: "basis_role",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Resource_Tenant_Id",
                table: "basis_resource",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Permission_Tenant_Id",
                table: "basis_permission",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Tenant_Id",
                table: "basis_notify",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Tenant_Id",
                table: "basis_meidia",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Tenant_Id",
                table: "basis_enumitem",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Tenant_Id",
                table: "basis_dept",
                column: "tenant_id");
        }
    }
}
