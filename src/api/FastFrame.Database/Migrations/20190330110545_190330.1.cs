using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1903301 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_employee");

            migrationBuilder.DropTable(
                name: "chat_notify");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "chat_groupmessage",
                newName: "Index_GroupMessage_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "chat_group",
                newName: "Index_Group_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "chat_friendmessage",
                newName: "Index_FriendMessage_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "chat_email",
                newName: "Index_Email_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "basis_user",
                newName: "Index_User_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "basis_tenanthost",
                newName: "Index_TenantHost_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "basis_rolepermission",
                newName: "Index_RolePermission_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "basis_rolemember",
                newName: "Index_RoleMember_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "basis_role",
                newName: "Index_Role_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "basis_resource",
                newName: "Index_Resource_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "basis_permission",
                newName: "Index_Permission_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "basis_meidia",
                newName: "Index_Meidia_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "basis_enumitem",
                newName: "Index_EnumItem_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_OrganizeId",
                table: "basis_dept",
                newName: "Index_Dept_Tenant_Id");

            migrationBuilder.AddColumn<string>(
                name: "dept_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "basis_notify",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    content = table.Column<string>(nullable: false),
                    publush_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_notify", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "encryptionkey", "password" },
                values: new object[] { "d84fbfd1b6709f99d600cedc23579891", "0f9b3cbb1734efe2018bdd562f13f0a2" });

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_Message_Id",
                table: "chat_messagetarget",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_To_Id",
                table: "chat_messagetarget",
                column: "to_id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupMessage_From_Id",
                table: "chat_groupmessage",
                column: "from_id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupMessage_Group_Id",
                table: "chat_groupmessage",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupMessage_Resource_Id",
                table: "chat_groupmessage",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupManager_Group_Id",
                table: "chat_groupmanager",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupManager_User_Id",
                table: "chat_groupmanager",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_Create_User_Id",
                table: "chat_group",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_HandIcon_Id",
                table: "chat_group",
                column: "handicon_id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_LordUser_Id",
                table: "chat_group",
                column: "lorduser_id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_Modify_User_Id",
                table: "chat_group",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_From_Id",
                table: "chat_friendmessage",
                column: "from_id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_Resource_Id",
                table: "chat_friendmessage",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_Email_Id",
                table: "chat_emailtarget",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_To_Id",
                table: "chat_emailtarget",
                column: "to_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailContent_Email_Id",
                table: "chat_emailcontent",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailAnnex_Email_Id",
                table: "chat_emailannex",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailAnnex_Resource_Id",
                table: "chat_emailannex",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "Index_Email_FromUser_Id",
                table: "chat_email",
                column: "fromuser_id");

            migrationBuilder.CreateIndex(
                name: "Index_Email_Replay_Email_Id",
                table: "chat_email",
                column: "replay_email_id");

            migrationBuilder.CreateIndex(
                name: "Index_User_Create_User_Id",
                table: "basis_user",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_User_Dept_Id",
                table: "basis_user",
                column: "dept_id");

            migrationBuilder.CreateIndex(
                name: "Index_User_HandIcon_Id",
                table: "basis_user",
                column: "handicon_id");

            migrationBuilder.CreateIndex(
                name: "Index_User_Modify_User_Id",
                table: "basis_user",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_HandIcon_Id",
                table: "basis_tenant",
                column: "handicon_id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_Parent_Id",
                table: "basis_tenant",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Create_User_Id",
                table: "basis_rolepermission",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Modify_User_Id",
                table: "basis_rolepermission",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Permission_Id",
                table: "basis_rolepermission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Role_Id",
                table: "basis_rolepermission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_Create_User_Id",
                table: "basis_rolemember",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_Modify_User_Id",
                table: "basis_rolemember",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_Role_Id",
                table: "basis_rolemember",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_User_Id",
                table: "basis_rolemember",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Create_User_Id",
                table: "basis_role",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Modify_User_Id",
                table: "basis_role",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Permission_Parent_Id",
                table: "basis_permission",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "Index_NotifyTarget_Notify_Id",
                table: "basis_notifytarget",
                column: "notify_id");

            migrationBuilder.CreateIndex(
                name: "Index_NotifyTarget_To_Id",
                table: "basis_notifytarget",
                column: "to_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Create_User_Id",
                table: "basis_meidia",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Modify_User_Id",
                table: "basis_meidia",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Parent_Id",
                table: "basis_meidia",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Resource_Id",
                table: "basis_meidia",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Parent_Id",
                table: "basis_enumitem",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Create_User_Id",
                table: "basis_dept",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Modify_User_Id",
                table: "basis_dept",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Parent_Id",
                table: "basis_dept",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Supervisor_Id",
                table: "basis_dept",
                column: "supervisor_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Create_User_Id",
                table: "basis_notify",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Modify_User_Id",
                table: "basis_notify",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Publush_Id",
                table: "basis_notify",
                column: "publush_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Tenant_Id",
                table: "basis_notify",
                column: "tenant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_notify");

            migrationBuilder.DropIndex(
                name: "Index_MessageTarget_Message_Id",
                table: "chat_messagetarget");

            migrationBuilder.DropIndex(
                name: "Index_MessageTarget_To_Id",
                table: "chat_messagetarget");

            migrationBuilder.DropIndex(
                name: "Index_GroupMessage_From_Id",
                table: "chat_groupmessage");

            migrationBuilder.DropIndex(
                name: "Index_GroupMessage_Group_Id",
                table: "chat_groupmessage");

            migrationBuilder.DropIndex(
                name: "Index_GroupMessage_Resource_Id",
                table: "chat_groupmessage");

            migrationBuilder.DropIndex(
                name: "Index_GroupManager_Group_Id",
                table: "chat_groupmanager");

            migrationBuilder.DropIndex(
                name: "Index_GroupManager_User_Id",
                table: "chat_groupmanager");

            migrationBuilder.DropIndex(
                name: "Index_Group_Create_User_Id",
                table: "chat_group");

            migrationBuilder.DropIndex(
                name: "Index_Group_HandIcon_Id",
                table: "chat_group");

            migrationBuilder.DropIndex(
                name: "Index_Group_LordUser_Id",
                table: "chat_group");

            migrationBuilder.DropIndex(
                name: "Index_Group_Modify_User_Id",
                table: "chat_group");

            migrationBuilder.DropIndex(
                name: "Index_FriendMessage_From_Id",
                table: "chat_friendmessage");

            migrationBuilder.DropIndex(
                name: "Index_FriendMessage_Resource_Id",
                table: "chat_friendmessage");

            migrationBuilder.DropIndex(
                name: "Index_EmailTarget_Email_Id",
                table: "chat_emailtarget");

            migrationBuilder.DropIndex(
                name: "Index_EmailTarget_To_Id",
                table: "chat_emailtarget");

            migrationBuilder.DropIndex(
                name: "Index_EmailContent_Email_Id",
                table: "chat_emailcontent");

            migrationBuilder.DropIndex(
                name: "Index_EmailAnnex_Email_Id",
                table: "chat_emailannex");

            migrationBuilder.DropIndex(
                name: "Index_EmailAnnex_Resource_Id",
                table: "chat_emailannex");

            migrationBuilder.DropIndex(
                name: "Index_Email_FromUser_Id",
                table: "chat_email");

            migrationBuilder.DropIndex(
                name: "Index_Email_Replay_Email_Id",
                table: "chat_email");

            migrationBuilder.DropIndex(
                name: "Index_User_Create_User_Id",
                table: "basis_user");

            migrationBuilder.DropIndex(
                name: "Index_User_Dept_Id",
                table: "basis_user");

            migrationBuilder.DropIndex(
                name: "Index_User_HandIcon_Id",
                table: "basis_user");

            migrationBuilder.DropIndex(
                name: "Index_User_Modify_User_Id",
                table: "basis_user");

            migrationBuilder.DropIndex(
                name: "Index_Tenant_HandIcon_Id",
                table: "basis_tenant");

            migrationBuilder.DropIndex(
                name: "Index_Tenant_Parent_Id",
                table: "basis_tenant");

            migrationBuilder.DropIndex(
                name: "Index_RolePermission_Create_User_Id",
                table: "basis_rolepermission");

            migrationBuilder.DropIndex(
                name: "Index_RolePermission_Modify_User_Id",
                table: "basis_rolepermission");

            migrationBuilder.DropIndex(
                name: "Index_RolePermission_Permission_Id",
                table: "basis_rolepermission");

            migrationBuilder.DropIndex(
                name: "Index_RolePermission_Role_Id",
                table: "basis_rolepermission");

            migrationBuilder.DropIndex(
                name: "Index_RoleMember_Create_User_Id",
                table: "basis_rolemember");

            migrationBuilder.DropIndex(
                name: "Index_RoleMember_Modify_User_Id",
                table: "basis_rolemember");

            migrationBuilder.DropIndex(
                name: "Index_RoleMember_Role_Id",
                table: "basis_rolemember");

            migrationBuilder.DropIndex(
                name: "Index_RoleMember_User_Id",
                table: "basis_rolemember");

            migrationBuilder.DropIndex(
                name: "Index_Role_Create_User_Id",
                table: "basis_role");

            migrationBuilder.DropIndex(
                name: "Index_Role_Modify_User_Id",
                table: "basis_role");

            migrationBuilder.DropIndex(
                name: "Index_Permission_Parent_Id",
                table: "basis_permission");

            migrationBuilder.DropIndex(
                name: "Index_NotifyTarget_Notify_Id",
                table: "basis_notifytarget");

            migrationBuilder.DropIndex(
                name: "Index_NotifyTarget_To_Id",
                table: "basis_notifytarget");

            migrationBuilder.DropIndex(
                name: "Index_Meidia_Create_User_Id",
                table: "basis_meidia");

            migrationBuilder.DropIndex(
                name: "Index_Meidia_Modify_User_Id",
                table: "basis_meidia");

            migrationBuilder.DropIndex(
                name: "Index_Meidia_Parent_Id",
                table: "basis_meidia");

            migrationBuilder.DropIndex(
                name: "Index_Meidia_Resource_Id",
                table: "basis_meidia");

            migrationBuilder.DropIndex(
                name: "Index_EnumItem_Parent_Id",
                table: "basis_enumitem");

            migrationBuilder.DropIndex(
                name: "Index_Dept_Create_User_Id",
                table: "basis_dept");

            migrationBuilder.DropIndex(
                name: "Index_Dept_Modify_User_Id",
                table: "basis_dept");

            migrationBuilder.DropIndex(
                name: "Index_Dept_Parent_Id",
                table: "basis_dept");

            migrationBuilder.DropIndex(
                name: "Index_Dept_Supervisor_Id",
                table: "basis_dept");

            migrationBuilder.DropColumn(
                name: "dept_id",
                table: "basis_user");

            migrationBuilder.RenameIndex(
                name: "Index_GroupMessage_Tenant_Id",
                table: "chat_groupmessage",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_Group_Tenant_Id",
                table: "chat_group",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_FriendMessage_Tenant_Id",
                table: "chat_friendmessage",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_Email_Tenant_Id",
                table: "chat_email",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_User_Tenant_Id",
                table: "basis_user",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_TenantHost_Tenant_Id",
                table: "basis_tenanthost",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_RolePermission_Tenant_Id",
                table: "basis_rolepermission",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_RoleMember_Tenant_Id",
                table: "basis_rolemember",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_Role_Tenant_Id",
                table: "basis_role",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_Resource_Tenant_Id",
                table: "basis_resource",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_Permission_Tenant_Id",
                table: "basis_permission",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_Meidia_Tenant_Id",
                table: "basis_meidia",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_EnumItem_Tenant_Id",
                table: "basis_enumitem",
                newName: "Index_OrganizeId");

            migrationBuilder.RenameIndex(
                name: "Index_Dept_Tenant_Id",
                table: "basis_dept",
                newName: "Index_OrganizeId");

            migrationBuilder.CreateTable(
                name: "basis_employee",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    createtime = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    dept_id = table.Column<string>(maxLength: 25, nullable: true),
                    email = table.Column<string>(maxLength: 50, nullable: true),
                    encode = table.Column<string>(maxLength: 20, nullable: false),
                    gender = table.Column<string>(maxLength: 100, nullable: false, defaultValue: "Man"),
                    isdeleted = table.Column<bool>(nullable: false),
                    modifytime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    name = table.Column<string>(maxLength: 20, nullable: false),
                    phonenumber = table.Column<string>(maxLength: 20, nullable: true),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true),
                    user_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_employee", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_notify",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    content = table.Column<string>(nullable: false),
                    createtime = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    modifytime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    publush_id = table.Column<string>(maxLength: 25, nullable: true),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true),
                    title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_notify", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "encryptionkey", "password" },
                values: new object[] { "26de805da1bc47adea2894e8eec0e027", "03c44f54f87841ec4175d82ce4ccf161" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_employee",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "chat_notify",
                column: "tenant_id");
        }
    }
}
