using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "basis_dept",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    encode = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    supervisor_id = table.Column<string>(maxLength: 25, nullable: true),
                    super_id = table.Column<string>(maxLength: 25, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_dept", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_deptmember",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    dept_id = table.Column<string>(maxLength: 25, nullable: false),
                    user_id = table.Column<string>(maxLength: 25, nullable: false),
                    ismanager = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_deptmember", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_enumitem",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    key = table.Column<string>(maxLength: 50, nullable: false),
                    value = table.Column<string>(maxLength: 150, nullable: false),
                    super_id = table.Column<string>(maxLength: 25, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_enumitem", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_meidia",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    super_id = table.Column<string>(maxLength: 25, nullable: true),
                    href = table.Column<string>(maxLength: 200, nullable: true),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    resource_id = table.Column<string>(maxLength: 25, nullable: true),
                    contenttype = table.Column<string>(maxLength: 50, nullable: true),
                    isfolder = table.Column<bool>(nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_meidia", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_notify",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    type_id = table.Column<string>(maxLength: 25, nullable: true),
                    publush_id = table.Column<string>(maxLength: 25, nullable: true),
                    resource_id = table.Column<string>(maxLength: 25, nullable: true),
                    content = table.Column<string>(maxLength: 8000, nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_notify", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_permission",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    encode = table.Column<string>(maxLength: 50, nullable: false),
                    areaname = table.Column<string>(maxLength: 50, nullable: false),
                    super_id = table.Column<string>(maxLength: 25, nullable: true),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_resource",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    name = table.Column<string>(maxLength: 150, nullable: true),
                    size = table.Column<long>(nullable: false),
                    path = table.Column<string>(maxLength: 150, nullable: true),
                    contenttype = table.Column<string>(maxLength: 50, nullable: true),
                    md5 = table.Column<string>(nullable: true),
                    uploader_id = table.Column<string>(maxLength: 25, nullable: true),
                    uploadtime = table.Column<DateTime>(nullable: false),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_resource", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_role",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    encode = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_rolemember",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    role_id = table.Column<string>(maxLength: 25, nullable: true),
                    user_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_rolemember", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_rolepermission",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    role_id = table.Column<string>(maxLength: 25, nullable: true),
                    permission_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_rolepermission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_tenant",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    fullname = table.Column<string>(maxLength: 50, nullable: false),
                    shortname = table.Column<string>(maxLength: 50, nullable: false),
                    urlmark = table.Column<string>(maxLength: 50, nullable: true),
                    super_id = table.Column<string>(maxLength: 25, nullable: true),
                    canhavechildren = table.Column<bool>(nullable: false),
                    handicon_id = table.Column<string>(maxLength: 25, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_tenant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_tenanthost",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    host = table.Column<string>(maxLength: 200, nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_tenanthost", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_user",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    account = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(maxLength: 50, nullable: false),
                    encryptionkey = table.Column<string>(maxLength: 36, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: true),
                    phonenumber = table.Column<string>(maxLength: 20, nullable: true),
                    handicon_id = table.Column<string>(maxLength: 25, nullable: true),
                    isadmin = table.Column<bool>(nullable: false),
                    isroot = table.Column<bool>(nullable: false),
                    isdisabled = table.Column<bool>(nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_email",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    replay_email_id = table.Column<string>(maxLength: 25, nullable: true),
                    fromuser_id = table.Column<string>(maxLength: 25, nullable: true),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_email", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_emailannex",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    email_id = table.Column<string>(maxLength: 25, nullable: true),
                    resource_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_emailannex", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_emailcontent",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    email_id = table.Column<string>(maxLength: 25, nullable: true),
                    content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_emailcontent", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_emailtarget",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    to_id = table.Column<string>(maxLength: 25, nullable: true),
                    haveread = table.Column<bool>(nullable: false),
                    email_id = table.Column<string>(maxLength: 25, nullable: true),
                    category = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_emailtarget", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_friendmessage",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    content = table.Column<string>(maxLength: 500, nullable: true),
                    category = table.Column<string>(maxLength: 50, nullable: false),
                    resource_id = table.Column<string>(maxLength: 25, nullable: true),
                    from_id = table.Column<string>(maxLength: 25, nullable: true),
                    messagetime = table.Column<DateTime>(nullable: false),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_friendmessage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_group",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    lorduser_id = table.Column<string>(maxLength: 25, nullable: true),
                    handicon_id = table.Column<string>(maxLength: 25, nullable: true),
                    summary = table.Column<string>(maxLength: 200, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_groupmanager",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    group_id = table.Column<string>(maxLength: 25, nullable: true),
                    user_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_groupmanager", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_groupmessage",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    content = table.Column<string>(maxLength: 500, nullable: true),
                    category = table.Column<string>(maxLength: 50, nullable: false),
                    resource_id = table.Column<string>(maxLength: 25, nullable: true),
                    from_id = table.Column<string>(maxLength: 25, nullable: true),
                    messagetime = table.Column<DateTime>(nullable: false),
                    group_id = table.Column<string>(maxLength: 25, nullable: true),
                    tenant_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_groupmessage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_messagetarget",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    to_id = table.Column<string>(maxLength: 25, nullable: true),
                    haveread = table.Column<bool>(nullable: false),
                    message_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_messagetarget", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "basis_tenant",
                columns: new[] { "id", "canhavechildren", "fullname", "handicon_id", "shortname", "super_id", "urlmark", "isdeleted" },
                values: new object[] { "00fm5yfgzpgp93ylkuxshsc73", true, "默认组织", null, "default", null, null, false });

            migrationBuilder.InsertData(
                table: "basis_tenanthost",
                columns: new[] { "id", "host", "tenant_id" },
                values: new object[,]
                {
                    { "00fm5yfh942593ylkueadrwah", "*", "00fm5yfgzpgp93ylkuxshsc73" },
                    { "00fm5yfhhpy393ylku9dk1u5b", "192.168.1.100:8081", "00fm5yfgzpgp93ylkuxshsc73" },
                    { "00fm5yfhqq1h93ylkumx3gm53", "192.168.1.100:82", "00fm5yfgzpgp93ylkuxshsc73" }
                });

            migrationBuilder.InsertData(
                table: "basis_user",
                columns: new[] { "id", "account", "createtime", "create_user_id", "email", "encryptionkey", "handicon_id", "isadmin", "isdisabled", "isroot", "modifytime", "modify_user_id", "name", "password", "phonenumber", "isdeleted", "tenant_id" },
                values: new object[] { "00fm5yfgq3q893ylku6uzb57i", "admin", new DateTime(2019, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "00fm5yfgq3q893ylku6uzb57i", "gongjie@qq.com", "0ee3dcf0e832334f63876a30b45fdece", null, true, false, true, new DateTime(2019, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "00fm5yfgq3q893ylku6uzb57i", "管理员", "000000", "18675517757", false, "00fm5yfgzpgp93ylkuxshsc73" });

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Create_User_Id",
                table: "basis_dept",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Modify_User_Id",
                table: "basis_dept",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Super_Id",
                table: "basis_dept",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Supervisor_Id",
                table: "basis_dept",
                column: "supervisor_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_isdeleted",
                table: "basis_dept",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_tenant_id",
                table: "basis_dept",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_Dept_Id",
                table: "basis_deptmember",
                column: "dept_id");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_User_Id",
                table: "basis_deptmember",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Create_User_Id",
                table: "basis_enumitem",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Modify_User_Id",
                table: "basis_enumitem",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Super_Id",
                table: "basis_enumitem",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_isdeleted",
                table: "basis_enumitem",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_tenant_id",
                table: "basis_enumitem",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Create_User_Id",
                table: "basis_meidia",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Modify_User_Id",
                table: "basis_meidia",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Resource_Id",
                table: "basis_meidia",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Super_Id",
                table: "basis_meidia",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_isdeleted",
                table: "basis_meidia",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_tenant_id",
                table: "basis_meidia",
                column: "tenant_id");

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
                name: "Index_Notify_Resource_Id",
                table: "basis_notify",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Type_Id",
                table: "basis_notify",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_isdeleted",
                table: "basis_notify",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_tenant_id",
                table: "basis_notify",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Permission_Super_Id",
                table: "basis_permission",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_Permission_tenant_id",
                table: "basis_permission",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Resource_Uploader_Id",
                table: "basis_resource",
                column: "uploader_id");

            migrationBuilder.CreateIndex(
                name: "Index_Resource_tenant_id",
                table: "basis_resource",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Create_User_Id",
                table: "basis_role",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Modify_User_Id",
                table: "basis_role",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_isdeleted",
                table: "basis_role",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Role_tenant_id",
                table: "basis_role",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_Role_Id",
                table: "basis_rolemember",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_User_Id",
                table: "basis_rolemember",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Permission_Id",
                table: "basis_rolepermission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Role_Id",
                table: "basis_rolepermission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_HandIcon_Id",
                table: "basis_tenant",
                column: "handicon_id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_Super_Id",
                table: "basis_tenant",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_isdeleted",
                table: "basis_tenant",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_TenantHost_Tenant_Id",
                table: "basis_tenanthost",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_User_Create_User_Id",
                table: "basis_user",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_User_HandIcon_Id",
                table: "basis_user",
                column: "handicon_id");

            migrationBuilder.CreateIndex(
                name: "Index_User_Modify_User_Id",
                table: "basis_user",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_User_isdeleted",
                table: "basis_user",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_User_tenant_id",
                table: "basis_user",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Email_FromUser_Id",
                table: "chat_email",
                column: "fromuser_id");

            migrationBuilder.CreateIndex(
                name: "Index_Email_Replay_Email_Id",
                table: "chat_email",
                column: "replay_email_id");

            migrationBuilder.CreateIndex(
                name: "Index_Email_tenant_id",
                table: "chat_email",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailAnnex_Email_Id",
                table: "chat_emailannex",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailAnnex_Resource_Id",
                table: "chat_emailannex",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailContent_Email_Id",
                table: "chat_emailcontent",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_Email_Id",
                table: "chat_emailtarget",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_To_Id",
                table: "chat_emailtarget",
                column: "to_id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_From_Id",
                table: "chat_friendmessage",
                column: "from_id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_Resource_Id",
                table: "chat_friendmessage",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_tenant_id",
                table: "chat_friendmessage",
                column: "tenant_id");

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
                name: "Index_Group_isdeleted",
                table: "chat_group",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Group_tenant_id",
                table: "chat_group",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupManager_Group_Id",
                table: "chat_groupmanager",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupManager_User_Id",
                table: "chat_groupmanager",
                column: "user_id");

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
                name: "Index_GroupMessage_tenant_id",
                table: "chat_groupmessage",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_Message_Id",
                table: "chat_messagetarget",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_To_Id",
                table: "chat_messagetarget",
                column: "to_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_dept");

            migrationBuilder.DropTable(
                name: "basis_deptmember");

            migrationBuilder.DropTable(
                name: "basis_enumitem");

            migrationBuilder.DropTable(
                name: "basis_meidia");

            migrationBuilder.DropTable(
                name: "basis_notify");

            migrationBuilder.DropTable(
                name: "basis_permission");

            migrationBuilder.DropTable(
                name: "basis_resource");

            migrationBuilder.DropTable(
                name: "basis_role");

            migrationBuilder.DropTable(
                name: "basis_rolemember");

            migrationBuilder.DropTable(
                name: "basis_rolepermission");

            migrationBuilder.DropTable(
                name: "basis_tenant");

            migrationBuilder.DropTable(
                name: "basis_tenanthost");

            migrationBuilder.DropTable(
                name: "basis_user");

            migrationBuilder.DropTable(
                name: "chat_email");

            migrationBuilder.DropTable(
                name: "chat_emailannex");

            migrationBuilder.DropTable(
                name: "chat_emailcontent");

            migrationBuilder.DropTable(
                name: "chat_emailtarget");

            migrationBuilder.DropTable(
                name: "chat_friendmessage");

            migrationBuilder.DropTable(
                name: "chat_group");

            migrationBuilder.DropTable(
                name: "chat_groupmanager");

            migrationBuilder.DropTable(
                name: "chat_groupmessage");

            migrationBuilder.DropTable(
                name: "chat_messagetarget");
        }
    }
}
