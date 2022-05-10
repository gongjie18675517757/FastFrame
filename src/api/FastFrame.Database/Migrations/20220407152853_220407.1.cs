using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFrame.Database.Migrations
{
    public partial class _2204071 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "basis_apirequestlog",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ipaddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "请求人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "请求人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    requesttime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "请求时间"),
                    path = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "请求地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    milliseconds = table.Column<long>(type: "bigint", nullable: false, comment: "耗时数(毫秒)"),
                    statuscode = table.Column<int>(type: "int", nullable: false, comment: "响应状态码")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_apirequestlog", x => x.id);
                },
                comment: "api请求记录")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfform",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodule_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModule")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulename = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "关联:DFModule")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    number = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "单据编号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    create_user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modify_user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenant_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfform", x => x.id);
                },
                comment: "动态表单")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfformdate",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<DateOnly>(type: "date", nullable: true, comment: "值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfformdate", x => x.id);
                },
                comment: "动态表单值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfformdatetime",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfformdatetime", x => x.id);
                },
                comment: "动态表单值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfformdecimal",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true, comment: "值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfformdecimal", x => x.id);
                },
                comment: "动态表单值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfformint32",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<int>(type: "int", nullable: true, comment: "值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfformint32", x => x.id);
                },
                comment: "动态表单值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfformrelate",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联键")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfformrelate", x => x.id);
                },
                comment: "动态表单值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfformrichtext",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nvarcharmax = table.Column<string>(name: "nvarchar(max)", type: "varchar(4000)", maxLength: 4000, nullable: true, comment: "值")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfformrichtext", x => x.id);
                },
                comment: "动态表单值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfformrow",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfformrow", x => x.id);
                },
                comment: "动态表单值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfformtext",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "值")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfformtext", x => x.id);
                },
                comment: "动态表单值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfformtextarea",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true, comment: "值")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfformtextarea", x => x.id);
                },
                comment: "动态表单值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfformtime",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<TimeOnly>(type: "time(6)", nullable: true, comment: "值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfformtime", x => x.id);
                },
                comment: "动态表单值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfmodule",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    version = table.Column<int>(type: "int", nullable: false, comment: "版本"),
                    isenabled = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    havecheck = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否需要审核"),
                    havenumber = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否需要编号"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    create_user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modify_user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenant_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfmodule", x => x.id);
                },
                comment: "动态表单模块")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfmodulefield",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodule_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "所属模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulegroup_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "所属分组")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    super_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fieldtype = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "字段类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isrelatefield = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否为摘要字段"),
                    showlist = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "列表展示"),
                    isrequired = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "必填"),
                    @readonly = table.Column<string>(name: "readonly", type: "varchar(50)", maxLength: 50, nullable: true, comment: "只读标记")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    relate = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "关联模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enumvalues = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "枚举项(\";\"分隔)")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enumitemname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "数据字典项")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfmodulefield", x => x.id);
                },
                comment: "动态表单字段")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfmodulefieldrule",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodule_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "所属模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefield_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联字段")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ruleenum = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "验证规则")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    funccontent = table.Column<string>(type: "longtext", nullable: true, comment: "自定义验证规则")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfmodulefieldrule", x => x.id);
                },
                comment: "字段验证规则")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfmodulegroup",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodule_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "所属模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    groupname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "标题")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_dfmodulegroup", x => x.id);
                },
                comment: "动态表单分组")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_ApiRequestLog_User_Id",
                table: "basis_apirequestlog",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFForm_Create_User_Id",
                table: "flow_dfform",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFForm_DFModule_Id",
                table: "flow_dfform",
                column: "dfmodule_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFForm_isdeleted",
                table: "flow_dfform",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_DFForm_Modify_User_Id",
                table: "flow_dfform",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFForm_Tenant_Id",
                table: "flow_dfform",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormDate_DFForm_Id",
                table: "flow_dfformdate",
                column: "dfform_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormDate_DFModuleField_Id",
                table: "flow_dfformdate",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormDate_Row_Id",
                table: "flow_dfformdate",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormDateTime_DFForm_Id",
                table: "flow_dfformdatetime",
                column: "dfform_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormDateTime_DFModuleField_Id",
                table: "flow_dfformdatetime",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormDateTime_Row_Id",
                table: "flow_dfformdatetime",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormDecimal_DFForm_Id",
                table: "flow_dfformdecimal",
                column: "dfform_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormDecimal_DFModuleField_Id",
                table: "flow_dfformdecimal",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormDecimal_Row_Id",
                table: "flow_dfformdecimal",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormInt32_DFForm_Id",
                table: "flow_dfformint32",
                column: "dfform_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormInt32_DFModuleField_Id",
                table: "flow_dfformint32",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormInt32_Row_Id",
                table: "flow_dfformint32",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormRelate_DFForm_Id",
                table: "flow_dfformrelate",
                column: "dfform_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormRelate_DFModuleField_Id",
                table: "flow_dfformrelate",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormRelate_Row_Id",
                table: "flow_dfformrelate",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormRelate_Value_Id",
                table: "flow_dfformrelate",
                column: "value_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormRichtext_DFForm_Id",
                table: "flow_dfformrichtext",
                column: "dfform_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormRichtext_DFModuleField_Id",
                table: "flow_dfformrichtext",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormRichtext_Row_Id",
                table: "flow_dfformrichtext",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormRow_DFForm_Id",
                table: "flow_dfformrow",
                column: "dfform_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormRow_DFModuleField_Id",
                table: "flow_dfformrow",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormText_DFForm_Id",
                table: "flow_dfformtext",
                column: "dfform_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormText_DFModuleField_Id",
                table: "flow_dfformtext",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormText_Row_Id",
                table: "flow_dfformtext",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormTextarea_DFForm_Id",
                table: "flow_dfformtextarea",
                column: "dfform_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormTextarea_DFModuleField_Id",
                table: "flow_dfformtextarea",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormTextarea_Row_Id",
                table: "flow_dfformtextarea",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormTime_DFForm_Id",
                table: "flow_dfformtime",
                column: "dfform_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormTime_DFModuleField_Id",
                table: "flow_dfformtime",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFFormTime_Row_Id",
                table: "flow_dfformtime",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFModule_Create_User_Id",
                table: "flow_dfmodule",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFModule_HaveCheck",
                table: "flow_dfmodule",
                column: "havecheck");

            migrationBuilder.CreateIndex(
                name: "Index_DFModule_HaveNumber",
                table: "flow_dfmodule",
                column: "havenumber");

            migrationBuilder.CreateIndex(
                name: "Index_DFModule_isdeleted",
                table: "flow_dfmodule",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_DFModule_IsEnabled",
                table: "flow_dfmodule",
                column: "isenabled");

            migrationBuilder.CreateIndex(
                name: "Index_DFModule_Modify_User_Id",
                table: "flow_dfmodule",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFModule_Tenant_Id",
                table: "flow_dfmodule",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleField_DFModule_Id",
                table: "flow_dfmodulefield",
                column: "dfmodule_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleField_DFModuleGroup_Id",
                table: "flow_dfmodulefield",
                column: "dfmodulegroup_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleField_FieldType",
                table: "flow_dfmodulefield",
                column: "fieldtype");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleField_IsRelateField",
                table: "flow_dfmodulefield",
                column: "isrelatefield");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleField_IsRequired",
                table: "flow_dfmodulefield",
                column: "isrequired");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleField_Readonly",
                table: "flow_dfmodulefield",
                column: "readonly");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleField_ShowList",
                table: "flow_dfmodulefield",
                column: "showlist");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleField_Super_Id",
                table: "flow_dfmodulefield",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleFieldRule_DFModule_Id",
                table: "flow_dfmodulefieldrule",
                column: "dfmodule_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleFieldRule_DFModuleField_Id",
                table: "flow_dfmodulefieldrule",
                column: "dfmodulefield_id");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleFieldRule_RuleEnum",
                table: "flow_dfmodulefieldrule",
                column: "ruleenum");

            migrationBuilder.CreateIndex(
                name: "Index_DFModuleGroup_DFModule_Id",
                table: "flow_dfmodulegroup",
                column: "dfmodule_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_apirequestlog");

            migrationBuilder.DropTable(
                name: "flow_dfform");

            migrationBuilder.DropTable(
                name: "flow_dfformdate");

            migrationBuilder.DropTable(
                name: "flow_dfformdatetime");

            migrationBuilder.DropTable(
                name: "flow_dfformdecimal");

            migrationBuilder.DropTable(
                name: "flow_dfformint32");

            migrationBuilder.DropTable(
                name: "flow_dfformrelate");

            migrationBuilder.DropTable(
                name: "flow_dfformrichtext");

            migrationBuilder.DropTable(
                name: "flow_dfformrow");

            migrationBuilder.DropTable(
                name: "flow_dfformtext");

            migrationBuilder.DropTable(
                name: "flow_dfformtextarea");

            migrationBuilder.DropTable(
                name: "flow_dfformtime");

            migrationBuilder.DropTable(
                name: "flow_dfmodule");

            migrationBuilder.DropTable(
                name: "flow_dfmodulefield");

            migrationBuilder.DropTable(
                name: "flow_dfmodulefieldrule");

            migrationBuilder.DropTable(
                name: "flow_dfmodulegroup");
        }
    }
}
