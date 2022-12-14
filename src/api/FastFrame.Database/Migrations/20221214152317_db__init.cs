using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFrame.Database.Migrations
{
    /// <inheritdoc />
    public partial class dbinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_apirequestlog",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ipaddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userid = table.Column<string>(name: "user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "请求人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "请求人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    requesttime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "请求时间"),
                    path = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "请求地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    milliseconds = table.Column<long>(type: "bigint", nullable: false, comment: "耗时数(毫秒)"),
                    statuscode = table.Column<int>(type: "int", nullable: false, comment: "响应状态码"),
                    requestlength = table.Column<long>(type: "bigint", nullable: true, comment: "请求大小")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_apirequestlog", x => x.id);
                },
                comment: "api请求记录")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_billbedept",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    billid = table.Column<string>(name: "bill_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：Bill")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bedeptid = table.Column<string>(name: "bedept_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "归属部门")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_billbedept", x => x.id);
                },
                comment: "单据归属部门")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_dept",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    superid = table.Column<string>(name: "super_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    encode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "部门代码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "部门名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    remarks = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    treecode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "树状码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_dept", x => x.id);
                },
                comment: "部门")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_deptmember",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deptid = table.Column<string>(name: "dept_id", type: "varchar(25)", maxLength: 25, nullable: false, comment: "部门")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userid = table.Column<string>(name: "user_id", type: "varchar(25)", maxLength: 25, nullable: false, comment: "用户")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ismanager = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否管理")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_deptmember", x => x.id);
                },
                comment: "部门成员")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_enumitem",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    issystemenum = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否系统枚举"),
                    keyenum = table.Column<int>(type: "int", nullable: false, comment: "字典类别"),
                    superid = table.Column<string>(name: "super_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    intkey = table.Column<int>(type: "int", nullable: false, comment: "字典数字值"),
                    textvalue = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, comment: "字典文本值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sortval = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    treecode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "树状码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_enumitem", x => x.id);
                },
                comment: "数字字典")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_loginlog",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userid = table.Column<string>(name: "user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：User")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    logintime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "登陆时间"),
                    issuccessful = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "登陆成功"),
                    failreason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "失败原因")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lasttime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后刷新时间"),
                    expiredtime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "过期时间"),
                    ipaddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isenabled = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否有效"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_loginlog", x => x.id);
                },
                comment: "登陆Log")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_meidia",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    superid = table.Column<string>(name: "super_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resourceid = table.Column<string>(name: "resource_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "资源")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isfolder = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否文件夹"),
                    treecode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "树状码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_meidia", x => x.id);
                },
                comment: "图片库")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_notify",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    typeid = table.Column<int>(name: "type_id", type: "int", nullable: true, comment: "类型"),
                    publushid = table.Column<string>(name: "publush_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "发布人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resourceid = table.Column<string>(name: "resource_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "附件")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "longtext", nullable: false, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_notify", x => x.id);
                },
                comment: "通知")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_numberoption",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bemodule = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "模块名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prefix = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, comment: "前缀")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    taskdate = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否取日期"),
                    seriallength = table.Column<int>(type: "int", nullable: false, comment: "流水号长度"),
                    suffix = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, comment: "后缀")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    datefield = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "取日期字段")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    datefieldtext = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "日期字段名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fmtdate = table.Column<int>(type: "int", nullable: true, comment: "日期格式方法"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_numberoption", x => x.id);
                },
                comment: "编号设置")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_numberrecord",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bemodule = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "模块名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    year = table.Column<int>(type: "int", nullable: false, comment: "年"),
                    month = table.Column<int>(type: "int", nullable: false, comment: "月"),
                    day = table.Column<int>(type: "int", nullable: false, comment: "日"),
                    serial = table.Column<int>(type: "int", nullable: false, comment: "流水"),
                    prevserial = table.Column<int>(type: "int", nullable: false, comment: "上一期流水"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_numberrecord", x => x.id);
                },
                comment: "编号记录")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_resource",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, comment: "资源名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    size = table.Column<long>(type: "bigint", nullable: false, comment: "资源大小"),
                    path = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, comment: "相对路径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contenttype = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, comment: "资源标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    md5 = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "MD5摘要")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uploaderid = table.Column<string>(name: "uploader_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上传人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uploadtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "上传时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_resource", x => x.id);
                },
                comment: "资源")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_role",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    encode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    superid = table.Column<string>(name: "super_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级角色")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdefault = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "缺省角色"),
                    isadmin = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "管理员角色"),
                    remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    treecode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "树状码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_role", x => x.id);
                },
                comment: "角色")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_rolepermission",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    roleid = table.Column<string>(name: "role_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "角色ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    permissionkey = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "权限标记")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_rolepermission", x => x.id);
                },
                comment: "角色权限")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_setting",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_setting", x => x.id);
                },
                comment: "系统设置")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_tablemap",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fkeyid = table.Column<string>(name: "fkey_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "外键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    keyname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "键名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    valueid = table.Column<string>(name: "value_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "键值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discriminator = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_tablemap", x => x.id);
                },
                comment: "表映射")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_tenant",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fullname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "全称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    shortname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "简称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    urlmark = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "URL标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    superid = table.Column<string>(name: "super_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户标记")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    handiconid = table.Column<string>(name: "handicon_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "Logo头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    treecode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "树状码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_tenant", x => x.id);
                },
                comment: "多租户信息")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_tenanthost",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    host = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "域名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: false, comment: "租户")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_tenanthost", x => x.id);
                },
                comment: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_user",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "帐号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    encryptionkey = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, comment: "密钥")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phonenumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "手机号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    handiconid = table.Column<string>(name: "handicon_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isadmin = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否管理员"),
                    enable = table.Column<int>(type: "int", nullable: false, comment: "启用状态"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_user", x => x.id);
                },
                comment: "用户")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_email",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    replayemailid = table.Column<string>(name: "replay_email_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "回复自")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fromuserid = table.Column<string>(name: "fromuser_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "发件人")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_email", x => x.id);
                },
                comment: "邮件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_emailannex",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    emailid = table.Column<string>(name: "email_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "邮件ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resourceid = table.Column<string>(name: "resource_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "文件ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_emailannex", x => x.id);
                },
                comment: "邮件附件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_emailcontent",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    emailid = table.Column<string>(name: "email_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "邮件ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "longtext", nullable: true, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_emailcontent", x => x.id);
                },
                comment: "邮件正文")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_emailtarget",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    emailid = table.Column<string>(name: "email_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "邮件ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    toid = table.Column<string>(name: "to_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "接收人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    haveread = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "已读")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_emailtarget", x => x.id);
                },
                comment: "邮件收件人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_friendmessage",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "消息类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resourceid = table.Column<string>(name: "resource_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "图片?附件ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fromid = table.Column<string>(name: "from_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "发送人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    messagetime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "消息时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_friendmessage", x => x.id);
                },
                comment: "好友消息")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_group",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "群组名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lorduserid = table.Column<string>(name: "lorduser_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "群主")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    handiconid = table.Column<string>(name: "handicon_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    summary = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "简介")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_group", x => x.id);
                },
                comment: "群组")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_groupmanager",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    groupid = table.Column<string>(name: "group_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "群组")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userid = table.Column<string>(name: "user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "管理员")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_groupmanager", x => x.id);
                },
                comment: "群组管理员")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_groupmessage",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    groupid = table.Column<string>(name: "group_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "群组ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "消息类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resourceid = table.Column<string>(name: "resource_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "图片?附件ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fromid = table.Column<string>(name: "from_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "发送人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    messagetime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "消息时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_groupmessage", x => x.id);
                },
                comment: "群组消息")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_messagetarget",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    messageid = table.Column<string>(name: "message_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "消息ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    toid = table.Column<string>(name: "to_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "接收人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    haveread = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "已读")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_messagetarget", x => x.id);
                },
                comment: "消息接收人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_dfform",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmoduleid = table.Column<string>(name: "dfmodule_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModule")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulename = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "关联:DFModule")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    number = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "单据编号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
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
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfformid = table.Column<string>(name: "dfform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rowid = table.Column<string>(name: "row_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
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
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfformid = table.Column<string>(name: "dfform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rowid = table.Column<string>(name: "row_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
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
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfformid = table.Column<string>(name: "dfform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rowid = table.Column<string>(name: "row_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
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
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfformid = table.Column<string>(name: "dfform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rowid = table.Column<string>(name: "row_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
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
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfformid = table.Column<string>(name: "dfform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rowid = table.Column<string>(name: "row_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    valueid = table.Column<string>(name: "value_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联键")
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
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfformid = table.Column<string>(name: "dfform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rowid = table.Column<string>(name: "row_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
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
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfformid = table.Column<string>(name: "dfform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
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
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfformid = table.Column<string>(name: "dfform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rowid = table.Column<string>(name: "row_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
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
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfformid = table.Column<string>(name: "dfform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rowid = table.Column<string>(name: "row_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
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
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFModuleField")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfformid = table.Column<string>(name: "dfform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:DFForm")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rowid = table.Column<string>(name: "row_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
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
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
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
                    dfmoduleid = table.Column<string>(name: "dfmodule_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "所属模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulegroupid = table.Column<string>(name: "dfmodulegroup_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "所属分组")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    superid = table.Column<string>(name: "super_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级字段：为从表时")
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
                    dfmoduleid = table.Column<string>(name: "dfmodule_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "所属模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dfmodulefieldid = table.Column<string>(name: "dfmodulefield_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联字段")
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
                    dfmoduleid = table.Column<string>(name: "dfmodule_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "所属模块")
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

            migrationBuilder.CreateTable(
                name: "flow_flowinstance",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bemodulename = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "归属模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bemoduletext = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "单据名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    billid = table.Column<string>(name: "bill_id", type: "varchar(25)", maxLength: 25, nullable: false, comment: "单据ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    billnumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "单据编号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    billdes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "单据摘要")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "流程状态"),
                    workflowid = table.Column<string>(name: "workflow_id", type: "varchar(25)", maxLength: 25, nullable: false, comment: "关联流程")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    currnodeid = table.Column<string>(name: "currnode_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    currnodename = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "当前节点")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sponsorname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "流程发起人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sponsorid = table.Column<string>(name: "sponsor_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    starttime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "发起时间"),
                    iscomlete = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否已完结"),
                    completetime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "完结时间"),
                    lastcheckername = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "最后审批人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastcheckerid = table.Column<string>(name: "lastchecker_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastchecktime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后审批时间"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowinstance", x => x.id);
                },
                comment: "流程实例")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flownextchecker",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowstepid = table.Column<string>(name: "flowstep_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowStep")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    checkerid = table.Column<string>(name: "checker_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:User")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownextchecker", x => x.id);
                },
                comment: "审批时指定的下一步审核人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flownode",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    workflowid = table.Column<string>(name: "workflow_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    orderval = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nodeenum = table.Column<int>(type: "int", nullable: false, comment: "节点类型"),
                    superid = table.Column<string>(name: "super_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    weight = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true, comment: "条件权重(为分支时)"),
                    isdefault = table.Column<bool>(type: "tinyint(1)", nullable: true, comment: "缺省分支(为分支时)"),
                    checkenum = table.Column<int>(type: "int", nullable: true, comment: "审批方式(多人时)"),
                    votescale = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true, comment: "通过比例(多人审批且审批方式为投票时)"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownode", x => x.id);
                },
                comment: "流程节点")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flownodechecker",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    workflowid = table.Column<string>(name: "workflow_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownodeid = table.Column<string>(name: "flownode_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    checkerenum = table.Column<int>(type: "int", nullable: false, comment: "审核人类型"),
                    checkerid = table.Column<string>(name: "checker_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "审核人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    checkername = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "审核人名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownodechecker", x => x.id);
                },
                comment: "流程节点审核人/抄送人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flownodecond",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    workflowid = table.Column<string>(name: "workflow_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownodeid = table.Column<string>(name: "flownode_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fieldname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "字段名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    groupindex = table.Column<int>(type: "int", nullable: false, comment: "条件组号"),
                    compareenum = table.Column<int>(type: "int", nullable: false, comment: "条件"),
                    valueenum = table.Column<int>(type: "int", nullable: false, comment: "值类型"),
                    valueid = table.Column<string>(name: "value_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "值的ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    valuetext = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "值的文本")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownodecond", x => x.id);
                },
                comment: "流程节点条件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flownodeevent",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    workflowid = table.Column<string>(name: "workflow_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownodeid = table.Column<string>(name: "flownode_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    eventtrigger = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "触发方式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    eventnotify = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "通知方式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    eventtarget = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "通知目标")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownodeevent", x => x.id);
                },
                comment: "节点事件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flowstep",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowinstanceid = table.Column<string>(name: "flowinstance_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowInstance")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownodeid = table.Column<string>(name: "flownode_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    beformid = table.Column<string>(name: "beform_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联表单的ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownodename = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "节点名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action = table.Column<int>(type: "int", nullable: true, comment: "动作"),
                    isfinished = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否已办理"),
                    operaterid = table.Column<string>(name: "operater_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "审批人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    operatername = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "审批人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    operatetime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "时间"),
                    desc = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "审批意见")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowstep", x => x.id);
                },
                comment: "审批步骤")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flowstepchecker",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowstepid = table.Column<string>(name: "flowstep_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowStep")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userid = table.Column<string>(name: "user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：User")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    billid = table.Column<string>(name: "bill_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "单据ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowinstanceid = table.Column<string>(name: "flowinstance_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowInstance")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowstepchecker", x => x.id);
                },
                comment: "流程步骤审核人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_workflow",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bemodule = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "适用模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bemodulename = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, comment: "模块名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    version = table.Column<int>(type: "int", nullable: false, comment: "版本"),
                    enabled = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_workflow", x => x.id);
                },
                comment: "工作流")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "oa_oaleave",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "请假单号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "申请时间"),
                    createuserid = table.Column<string>(name: "create_user_id", type: "varchar(25)", maxLength: 25, nullable: false, comment: "申请人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    jobid = table.Column<int>(name: "job_id", type: "int", nullable: false, comment: "岗位"),
                    deptid = table.Column<string>(name: "dept_id", type: "varchar(25)", maxLength: 25, nullable: false, comment: "部门")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    leavecategory = table.Column<int>(type: "int", nullable: false, comment: "请假类型"),
                    agentid = table.Column<string>(name: "agent_id", type: "varchar(25)", maxLength: 25, nullable: false, comment: "工作代理人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    starttime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "开始时间"),
                    endtime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "结束时间"),
                    days = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true, comment: "请假天数"),
                    reasons = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "申请事由")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowstatus = table.Column<int>(type: "int", nullable: false, comment: "审批状态"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    modifyuserid = table.Column<string>(name: "modify_user_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modifytime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    tenantid = table.Column<string>(name: "tenant_id", type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oa_oaleave", x => x.id);
                },
                comment: "请假单")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "basis_tenant",
                columns: new[] { "id", "fullname", "handicon_id", "shortname", "super_id", "tenant_id", "treecode", "urlmark", "isdeleted" },
                values: new object[] { "00fm5yfgzpgp93ylkuxshsc73", "默认组织", null, "default", null, null, null, "*", false });

            migrationBuilder.InsertData(
                table: "basis_user",
                columns: new[] { "id", "account", "createtime", "create_user_id", "email", "enable", "encryptionkey", "handicon_id", "isadmin", "modifytime", "modify_user_id", "name", "password", "phonenumber", "tenant_id", "isdeleted" },
                values: new object[] { "00fm5yfgq3q893ylku6uzb57i", "admin", new DateTime(2019, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "00fm5yfgq3q893ylku6uzb57i", "gongjie@qq.com", 0, "7d9d7edd6727912ce10b976818dd2856", null, true, new DateTime(2019, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "00fm5yfgq3q893ylku6uzb57i", "管理员", "9557847e0632e2f167a143b7ab3d668a", "18675517757", null, false });

            migrationBuilder.CreateIndex(
                name: "Index_ApiRequestLog_User_Id",
                table: "basis_apirequestlog",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_BillBeDept_BeDept_Id",
                table: "basis_billbedept",
                column: "bedept_id");

            migrationBuilder.CreateIndex(
                name: "Index_BillBeDept_Bill_Id",
                table: "basis_billbedept",
                column: "bill_id");

            migrationBuilder.CreateIndex(
                name: "Index_BillBeDept_isdeleted",
                table: "basis_billbedept",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Create_User_Id",
                table: "basis_dept",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_isdeleted",
                table: "basis_dept",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Modify_User_Id",
                table: "basis_dept",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Super_Id",
                table: "basis_dept",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Tenant_Id",
                table: "basis_dept",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_Dept_Id",
                table: "basis_deptmember",
                column: "dept_id");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_IsManager",
                table: "basis_deptmember",
                column: "ismanager");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_User_Id",
                table: "basis_deptmember",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Create_User_Id",
                table: "basis_enumitem",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_isdeleted",
                table: "basis_enumitem",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_IsSystemEnum",
                table: "basis_enumitem",
                column: "issystemenum");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Modify_User_Id",
                table: "basis_enumitem",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Super_Id",
                table: "basis_enumitem",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Tenant_Id",
                table: "basis_enumitem",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_IsEnabled",
                table: "basis_loginlog",
                column: "isenabled");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_IsSuccessful",
                table: "basis_loginlog",
                column: "issuccessful");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_Tenant_Id",
                table: "basis_loginlog",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_User_Id",
                table: "basis_loginlog",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Create_User_Id",
                table: "basis_meidia",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_isdeleted",
                table: "basis_meidia",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_IsFolder",
                table: "basis_meidia",
                column: "isfolder");

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
                name: "Index_Meidia_Tenant_Id",
                table: "basis_meidia",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Create_User_Id",
                table: "basis_notify",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_isdeleted",
                table: "basis_notify",
                column: "isdeleted");

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
                name: "Index_Notify_Tenant_Id",
                table: "basis_notify",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Type_Id",
                table: "basis_notify",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_Create_User_Id",
                table: "basis_numberoption",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_isdeleted",
                table: "basis_numberoption",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_Modify_User_Id",
                table: "basis_numberoption",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_TaskDate",
                table: "basis_numberoption",
                column: "taskdate");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_Tenant_Id",
                table: "basis_numberoption",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberRecord_isdeleted",
                table: "basis_numberrecord",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_NumberRecord_Tenant_Id",
                table: "basis_numberrecord",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Resource_Uploader_Id",
                table: "basis_resource",
                column: "uploader_id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Create_User_Id",
                table: "basis_role",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_IsAdmin",
                table: "basis_role",
                column: "isadmin");

            migrationBuilder.CreateIndex(
                name: "Index_Role_IsDefault",
                table: "basis_role",
                column: "isdefault");

            migrationBuilder.CreateIndex(
                name: "Index_Role_isdeleted",
                table: "basis_role",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Modify_User_Id",
                table: "basis_role",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Super_Id",
                table: "basis_role",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Tenant_Id",
                table: "basis_role",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Role_Id",
                table: "basis_rolepermission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_basis_rolepermission_permissionkey",
                table: "basis_rolepermission",
                column: "permissionkey");

            migrationBuilder.CreateIndex(
                name: "Index_TableMap_Discriminator",
                table: "basis_tablemap",
                column: "Discriminator");

            migrationBuilder.CreateIndex(
                name: "Index_TableMap_FKey_Id",
                table: "basis_tablemap",
                column: "fkey_id");

            migrationBuilder.CreateIndex(
                name: "Index_TableMap_Value_Id",
                table: "basis_tablemap",
                column: "value_id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_HandIcon_Id",
                table: "basis_tenant",
                column: "handicon_id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_isdeleted",
                table: "basis_tenant",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_Super_Id",
                table: "basis_tenant",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_Tenant_Id",
                table: "basis_tenant",
                column: "tenant_id");

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
                name: "Index_User_IsAdmin",
                table: "basis_user",
                column: "isadmin");

            migrationBuilder.CreateIndex(
                name: "Index_User_isdeleted",
                table: "basis_user",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_User_Modify_User_Id",
                table: "basis_user",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_User_Tenant_Id",
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
                name: "Index_EmailTarget_Category",
                table: "chat_emailtarget",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_Email_Id",
                table: "chat_emailtarget",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_HaveRead",
                table: "chat_emailtarget",
                column: "haveread");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_To_Id",
                table: "chat_emailtarget",
                column: "to_id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_Category",
                table: "chat_friendmessage",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_From_Id",
                table: "chat_friendmessage",
                column: "from_id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_Resource_Id",
                table: "chat_friendmessage",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_Tenant_Id",
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
                name: "Index_Group_isdeleted",
                table: "chat_group",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Group_LordUser_Id",
                table: "chat_group",
                column: "lorduser_id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_Modify_User_Id",
                table: "chat_group",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_Tenant_Id",
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
                name: "Index_GroupMessage_Category",
                table: "chat_groupmessage",
                column: "category");

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
                name: "Index_GroupMessage_Tenant_Id",
                table: "chat_groupmessage",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_HaveRead",
                table: "chat_messagetarget",
                column: "haveread");

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_Message_Id",
                table: "chat_messagetarget",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_To_Id",
                table: "chat_messagetarget",
                column: "to_id");

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

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_Bill_Id",
                table: "flow_flowinstance",
                column: "bill_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_CurrNode_Id",
                table: "flow_flowinstance",
                column: "currnode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_IsComlete",
                table: "flow_flowinstance",
                column: "iscomlete");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_isdeleted",
                table: "flow_flowinstance",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_LastChecker_Id",
                table: "flow_flowinstance",
                column: "lastchecker_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_Sponsor_Id",
                table: "flow_flowinstance",
                column: "sponsor_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_WorkFlow_Id",
                table: "flow_flowinstance",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNextChecker_Checker_Id",
                table: "flow_flownextchecker",
                column: "checker_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNextChecker_FlowStep_Id",
                table: "flow_flownextchecker",
                column: "flowstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_IsDefault",
                table: "flow_flownode",
                column: "isdefault");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_isdeleted",
                table: "flow_flownode",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_Super_Id",
                table: "flow_flownode",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_WorkFlow_Id",
                table: "flow_flownode",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeChecker_Checker_Id",
                table: "flow_flownodechecker",
                column: "checker_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeChecker_FlowNode_Id",
                table: "flow_flownodechecker",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeChecker_isdeleted",
                table: "flow_flownodechecker",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeChecker_WorkFlow_Id",
                table: "flow_flownodechecker",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeCond_FlowNode_Id",
                table: "flow_flownodecond",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeCond_isdeleted",
                table: "flow_flownodecond",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeCond_Value_Id",
                table: "flow_flownodecond",
                column: "value_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeCond_WorkFlow_Id",
                table: "flow_flownodecond",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeEvent_EventNotify",
                table: "flow_flownodeevent",
                column: "eventnotify");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeEvent_EventTarget",
                table: "flow_flownodeevent",
                column: "eventtarget");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeEvent_EventTrigger",
                table: "flow_flownodeevent",
                column: "eventtrigger");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeEvent_FlowNode_Id",
                table: "flow_flownodeevent",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeEvent_WorkFlow_Id",
                table: "flow_flownodeevent",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_BeForm_Id",
                table: "flow_flowstep",
                column: "beform_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_FlowInstance_Id",
                table: "flow_flowstep",
                column: "flowinstance_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_FlowNode_Id",
                table: "flow_flowstep",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_isdeleted",
                table: "flow_flowstep",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_IsFinished",
                table: "flow_flowstep",
                column: "isfinished");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_Operater_Id",
                table: "flow_flowstep",
                column: "operater_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepChecker_Bill_Id",
                table: "flow_flowstepchecker",
                column: "bill_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepChecker_FlowInstance_Id",
                table: "flow_flowstepchecker",
                column: "flowinstance_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepChecker_FlowStep_Id",
                table: "flow_flowstepchecker",
                column: "flowstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepChecker_isdeleted",
                table: "flow_flowstepchecker",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepChecker_User_Id",
                table: "flow_flowstepchecker",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_Create_User_Id",
                table: "flow_workflow",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_isdeleted",
                table: "flow_workflow",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_Modify_User_Id",
                table: "flow_workflow",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_Tenant_Id",
                table: "flow_workflow",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Agent_Id",
                table: "oa_oaleave",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Create_User_Id",
                table: "oa_oaleave",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Dept_Id",
                table: "oa_oaleave",
                column: "dept_id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_isdeleted",
                table: "oa_oaleave",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Job_Id",
                table: "oa_oaleave",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Modify_User_Id",
                table: "oa_oaleave",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Tenant_Id",
                table: "oa_oaleave",
                column: "tenant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_apirequestlog");

            migrationBuilder.DropTable(
                name: "basis_billbedept");

            migrationBuilder.DropTable(
                name: "basis_dept");

            migrationBuilder.DropTable(
                name: "basis_deptmember");

            migrationBuilder.DropTable(
                name: "basis_enumitem");

            migrationBuilder.DropTable(
                name: "basis_loginlog");

            migrationBuilder.DropTable(
                name: "basis_meidia");

            migrationBuilder.DropTable(
                name: "basis_notify");

            migrationBuilder.DropTable(
                name: "basis_numberoption");

            migrationBuilder.DropTable(
                name: "basis_numberrecord");

            migrationBuilder.DropTable(
                name: "basis_resource");

            migrationBuilder.DropTable(
                name: "basis_role");

            migrationBuilder.DropTable(
                name: "basis_rolepermission");

            migrationBuilder.DropTable(
                name: "basis_setting");

            migrationBuilder.DropTable(
                name: "basis_tablemap");

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

            migrationBuilder.DropTable(
                name: "flow_flowinstance");

            migrationBuilder.DropTable(
                name: "flow_flownextchecker");

            migrationBuilder.DropTable(
                name: "flow_flownode");

            migrationBuilder.DropTable(
                name: "flow_flownodechecker");

            migrationBuilder.DropTable(
                name: "flow_flownodecond");

            migrationBuilder.DropTable(
                name: "flow_flownodeevent");

            migrationBuilder.DropTable(
                name: "flow_flowstep");

            migrationBuilder.DropTable(
                name: "flow_flowstepchecker");

            migrationBuilder.DropTable(
                name: "flow_workflow");

            migrationBuilder.DropTable(
                name: "oa_oaleave");
        }
    }
}
