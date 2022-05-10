using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFrame.Database.Migrations
{
    public partial class _2204231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "proxy_proxyclient",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clienttoken = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "ClientId")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "详细描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    create_user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modify_user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenant_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proxy_proxyclient", x => x.id);
                },
                comment: "内网穿透服务")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "proxy_proxytarget",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    originmark = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, comment: "域名标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    proxyclient_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:ProxyClient")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    host = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "目标地址URL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    targetenum = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "代理方式")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proxy_proxytarget", x => x.id);
                },
                comment: "代理目标")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_ProxyClient_Create_User_Id",
                table: "proxy_proxyclient",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_ProxyClient_isdeleted",
                table: "proxy_proxyclient",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_ProxyClient_Modify_User_Id",
                table: "proxy_proxyclient",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_ProxyClient_Tenant_Id",
                table: "proxy_proxyclient",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_ProxyTarget_ProxyClient_Id",
                table: "proxy_proxytarget",
                column: "proxyclient_id");

            migrationBuilder.CreateIndex(
                name: "Index_ProxyTarget_TargetEnum",
                table: "proxy_proxytarget",
                column: "targetenum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "proxy_proxyclient");

            migrationBuilder.DropTable(
                name: "proxy_proxytarget");
        }
    }
}
