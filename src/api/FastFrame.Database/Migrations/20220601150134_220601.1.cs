using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFrame.Database.Migrations
{
    public partial class _2206011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "proxy_proxyclient",
                comment: "内网穿透隧道",
                oldComment: "内网穿透服务")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "originmark",
                table: "proxy_proxytarget",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "域名标识",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldComment: "域名标识")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "lasttime",
                table: "basis_loginlog",
                type: "datetime(6)",
                nullable: true,
                comment: "最后刷新时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "最后刷新时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expiredtime",
                table: "basis_loginlog",
                type: "datetime(6)",
                nullable: true,
                comment: "过期时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "过期时间");

            migrationBuilder.AddColumn<string>(
                name: "failreason",
                table: "basis_loginlog",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "失败原因")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "issuccessful",
                table: "basis_loginlog",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "登陆成功");

            migrationBuilder.AddColumn<long>(
                name: "requestlength",
                table: "basis_apirequestlog",
                type: "bigint",
                nullable: true,
                comment: "请求大写");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_IsSuccessful",
                table: "basis_loginlog",
                column: "issuccessful");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_LoginLog_IsSuccessful",
                table: "basis_loginlog");

            migrationBuilder.DropColumn(
                name: "failreason",
                table: "basis_loginlog");

            migrationBuilder.DropColumn(
                name: "issuccessful",
                table: "basis_loginlog");

            migrationBuilder.DropColumn(
                name: "requestlength",
                table: "basis_apirequestlog");

            migrationBuilder.AlterTable(
                name: "proxy_proxyclient",
                comment: "内网穿透服务",
                oldComment: "内网穿透隧道")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "proxy_proxytarget",
                keyColumn: "originmark",
                keyValue: null,
                column: "originmark",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "originmark",
                table: "proxy_proxytarget",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                comment: "域名标识",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "域名标识")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "lasttime",
                table: "basis_loginlog",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "最后刷新时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldComment: "最后刷新时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expiredtime",
                table: "basis_loginlog",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "过期时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldComment: "过期时间");
        }
    }
}
