using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFrame.Database.Migrations
{
    public partial class _2201161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_treechild");

            migrationBuilder.DropTable(
                name: "proxy_proxyclient");

            migrationBuilder.DropTable(
                name: "proxy_proxytarget");

            migrationBuilder.DropColumn(
                name: "code",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "encode",
                table: "basis_dept");

            migrationBuilder.RenameColumn(
                name: "order",
                table: "basis_enumitem",
                newName: "sortval");

            migrationBuilder.AlterTable(
                name: "basis_tenant",
                comment: "多租户信息",
                oldComment: "组织信息")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "treecode",
                table: "basis_tenant",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "树状码")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "treecode",
                table: "basis_meidia",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "树状码")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "basis_enumitem",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                comment: "字典值",
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldComment: "值")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "super_id",
                table: "basis_enumitem",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "上级值",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "上级")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "key",
                table: "basis_enumitem",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "字段类别",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "键")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "intkey",
                table: "basis_enumitem",
                type: "int",
                nullable: true,
                comment: "字典键");

            migrationBuilder.AddColumn<string>(
                name: "treecode",
                table: "basis_enumitem",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "树状码")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "basis_dept",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "部门名称",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "basis_dept",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "treecode",
                table: "basis_dept",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "部门代码")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "requestlength",
                table: "basis_apirequestlog",
                type: "bigint",
                nullable: true,
                comment: "请求大小",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "请求大写");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "treecode",
                table: "basis_tenant");

            migrationBuilder.DropColumn(
                name: "treecode",
                table: "basis_meidia");

            migrationBuilder.DropColumn(
                name: "intkey",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "treecode",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "remarks",
                table: "basis_dept");

            migrationBuilder.DropColumn(
                name: "treecode",
                table: "basis_dept");

            migrationBuilder.RenameColumn(
                name: "sortval",
                table: "basis_enumitem",
                newName: "order");

            migrationBuilder.AlterTable(
                name: "basis_tenant",
                comment: "组织信息",
                oldComment: "多租户信息")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "basis_enumitem",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                comment: "值",
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldComment: "字典值")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "super_id",
                table: "basis_enumitem",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "上级",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "上级值")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "key",
                table: "basis_enumitem",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "键",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "字段类别")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "basis_enumitem",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "编码")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "basis_dept",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "部门名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "encode",
                table: "basis_dept",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "编码")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "requestlength",
                table: "basis_apirequestlog",
                type: "bigint",
                nullable: true,
                comment: "请求大写",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "请求大小");

            migrationBuilder.CreateTable(
                name: "basis_treechild",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    child_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "下级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    super_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    treename = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "树名称")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_treechild", x => x.id);
                },
                comment: "树的递归下级")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "proxy_proxyclient",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clienttoken = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "ClientId")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    create_user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "详细描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    modify_user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proxy_proxyclient", x => x.id);
                },
                comment: "内网穿透隧道")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "proxy_proxytarget",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    host = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "目标地址URL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    originmark = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, comment: "域名标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    proxyclient_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:ProxyClient")
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
                name: "Index_TreeChild_Child_Id",
                table: "basis_treechild",
                column: "child_id");

            migrationBuilder.CreateIndex(
                name: "Index_TreeChild_Super_Id",
                table: "basis_treechild",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_TreeChild_TreeName",
                table: "basis_treechild",
                column: "treename");

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
    }
}
