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
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    isdeleted = table.Column<bool>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    modifytime = table.Column<DateTime>(nullable: false),
                    encode = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    parent_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    supervisor_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_dept", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_employee",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    isdeleted = table.Column<bool>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    modifytime = table.Column<DateTime>(nullable: false),
                    encode = table.Column<string>(maxLength: 20, nullable: false),
                    name = table.Column<string>(maxLength: 20, nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: true),
                    phonenumber = table.Column<string>(maxLength: 20, nullable: true),
                    gender = table.Column<string>(maxLength: 100, nullable: false, defaultValue: "Man"),
                    user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    dept_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_employee", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_meidia",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    isdeleted = table.Column<bool>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    modifytime = table.Column<DateTime>(nullable: false),
                    parent_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    href = table.Column<string>(maxLength: 200, nullable: true),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    resource_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    contenttype = table.Column<string>(maxLength: 50, nullable: true),
                    isfolder = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_meidia", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_notifytarget",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    to_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    haveread = table.Column<bool>(nullable: false),
                    notify_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_notifytarget", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_permission",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    parent_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    encode = table.Column<string>(maxLength: 50, nullable: false),
                    areaname = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_resource",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    name = table.Column<string>(maxLength: 150, nullable: true),
                    size = table.Column<long>(nullable: false),
                    path = table.Column<string>(maxLength: 150, nullable: true),
                    contenttype = table.Column<string>(maxLength: 50, nullable: true),
                    md5 = table.Column<string>(maxLength: 200, nullable: true),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_resource", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_role",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    isdeleted = table.Column<bool>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    modifytime = table.Column<DateTime>(nullable: false),
                    encode = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_rolemember",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    isdeleted = table.Column<bool>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    modifytime = table.Column<DateTime>(nullable: false),
                    role_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_rolemember", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_rolepermission",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    isdeleted = table.Column<bool>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    modifytime = table.Column<DateTime>(nullable: false),
                    role_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    permission_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_rolepermission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_tenant",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    fullname = table.Column<string>(maxLength: 50, nullable: false),
                    shortname = table.Column<string>(maxLength: 50, nullable: false),
                    urlmark = table.Column<string>(maxLength: 50, nullable: true),
                    parent_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    canhavechildren = table.Column<bool>(nullable: false),
                    handicon_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
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
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    host = table.Column<string>(maxLength: 200, nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_tenanthost", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_user",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    isdeleted = table.Column<bool>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    modifytime = table.Column<DateTime>(nullable: false),
                    account = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(maxLength: 50, nullable: false),
                    encryptionkey = table.Column<string>(maxLength: 36, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: true),
                    phonenumber = table.Column<string>(maxLength: 20, nullable: true),
                    handicon_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    isadmin = table.Column<bool>(nullable: false),
                    isroot = table.Column<bool>(nullable: false),
                    isdisabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_email",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    replay_email_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    fromuser_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_email", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_emailannex",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    email_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    resource_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_emailannex", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_emailcontent",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    email_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
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
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    to_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    haveread = table.Column<bool>(nullable: false),
                    email_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    category = table.Column<string>(maxLength: 100, nullable: false, defaultValue: "To")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_emailtarget", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_friendmessage",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    content = table.Column<string>(maxLength: 500, nullable: true),
                    category = table.Column<string>(maxLength: 100, nullable: false, defaultValue: "Text"),
                    resource_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    from_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    messagetime = table.Column<DateTime>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_friendmessage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_group",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    isdeleted = table.Column<bool>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    modifytime = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    lorduser_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    handicon_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    summary = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_groupmanager",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    group_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_groupmanager", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_groupmessage",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    content = table.Column<string>(maxLength: 500, nullable: true),
                    category = table.Column<string>(maxLength: 100, nullable: false, defaultValue: "Text"),
                    resource_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    from_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    messagetime = table.Column<DateTime>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    group_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_groupmessage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_messagetarget",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    to_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    haveread = table.Column<bool>(nullable: false),
                    message_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_messagetarget", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_notify",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false, defaultValue: ""),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    isdeleted = table.Column<bool>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: ""),
                    modifytime = table.Column<DateTime>(nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    content = table.Column<string>(nullable: false),
                    publush_id = table.Column<string>(maxLength: 25, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_notify", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "basis_tenant",
                columns: new[] { "id", "canhavechildren", "fullname", "isdeleted", "parent_id", "shortname", "urlmark" },
                values: new object[] { "00F6P5G2VC2SAP1UJV7HTBYGU", true, "默认组织", false, "", "default", "" });

            migrationBuilder.InsertData(
                table: "basis_tenanthost",
                columns: new[] { "id", "host", "tenant_id" },
                values: new object[,]
                {
                    { "00F6P5G2VC2SAP1UJV7HTBYGB", "192.168.1.100:8081", "00F6P5G2VC2SAP1UJV7HTBYGU" },
                    { "00F6P5G2VC2SAP1UJV7HTBYGc", "192.168.1.100:82", "00F6P5G2VC2SAP1UJV7HTBYGU" }
                });

            migrationBuilder.InsertData(
                table: "basis_user",
                columns: new[] { "id", "account", "createtime", "email", "encryptionkey", "isadmin", "isdeleted", "isdisabled", "isroot", "modifytime", "name", "password", "phonenumber", "tenant_id" },
                values: new object[] { "00F6P5G2VC2SAP1UJV7HTBYGA", "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "gongjie@qq.com", "0796597ef9ab8e60e8de8f747358f38b", true, false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "超级管理员", "5bd0f30d0fb4d72e595070db5021628f", "18675517757", "00F6P5G2VC2SAP1UJV7HTBYGU" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_dept",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_employee",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_meidia",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_permission",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_resource",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_role",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_rolemember",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_rolepermission",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_tenanthost",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_user",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "chat_email",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "chat_friendmessage",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "chat_group",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "chat_groupmessage",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "chat_notify",
                column: "tenant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_dept");

            migrationBuilder.DropTable(
                name: "basis_employee");

            migrationBuilder.DropTable(
                name: "basis_meidia");

            migrationBuilder.DropTable(
                name: "basis_notifytarget");

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

            migrationBuilder.DropTable(
                name: "chat_notify");
        }
    }
}
