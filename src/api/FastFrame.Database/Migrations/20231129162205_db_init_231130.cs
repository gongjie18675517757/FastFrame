using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFrame.Database.Migrations
{
    /// <inheritdoc />
    public partial class db_init_231130 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_ApiRequestLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IPAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "请求人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "请求人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RequestTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "请求时间"),
                    Path = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "请求地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Milliseconds = table.Column<long>(type: "bigint", nullable: false, comment: "耗时数(毫秒)"),
                    StatusCode = table.Column<int>(type: "int", nullable: false, comment: "响应状态码"),
                    RequestLength = table.Column<long>(type: "bigint", nullable: true, comment: "请求大小")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_ApiRequestLog", x => x.Id);
                },
                comment: "api请求记录")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_BillBeDept",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bill_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：Bill")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BeDept_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "归属部门")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_BillBeDept", x => x.Id);
                },
                comment: "单据归属部门")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_Dept",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Super_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "部门代码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "部门名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remarks = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TreeCode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "树状码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Create_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    Modify_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Dept", x => x.Id);
                },
                comment: "部门")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_DeptMember",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dept_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "部门")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "用户")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsManager = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否管理")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_DeptMember", x => x.Id);
                },
                comment: "部门成员")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_EnumItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSystemEnum = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否系统枚举"),
                    Enabled = table.Column<int>(type: "int", nullable: false, comment: "启用状态"),
                    KeyEnum = table.Column<int>(type: "int", nullable: false, comment: "字典类别"),
                    Super_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IntKey = table.Column<int>(type: "int", nullable: false, comment: "字典数字值"),
                    TextValue = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, comment: "字典文本值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SortVal = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    TreeCode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "树状码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Create_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    Modify_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_EnumItem", x => x.Id);
                },
                comment: "数字字典")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_LoginLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：User")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "登陆时间"),
                    IsSuccessful = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "登陆成功"),
                    FailReason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "失败原因")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastTime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后刷新时间"),
                    ExpiredTime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "过期时间"),
                    IPAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否有效"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_LoginLog", x => x.Id);
                },
                comment: "登陆Log")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_Meidia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Super_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resource_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "资源")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsFolder = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否文件夹"),
                    TreeCode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "树状码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Create_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    Modify_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Meidia", x => x.Id);
                },
                comment: "图片库")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_Notify",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type_Id = table.Column<int>(type: "int", nullable: true, comment: "类型"),
                    Publush_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "发布人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resource_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "附件")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: false, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Create_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    Modify_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Notify", x => x.Id);
                },
                comment: "通知")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_NumberOption",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BeModule = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "模块名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prefix = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, comment: "前缀")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskDate = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否取日期"),
                    SerialLength = table.Column<int>(type: "int", nullable: false, comment: "流水号长度"),
                    Suffix = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, comment: "后缀")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateField = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "取日期字段")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateFieldText = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "日期字段名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FmtDate = table.Column<int>(type: "int", nullable: true, comment: "日期格式方法"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Create_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    Modify_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_NumberOption", x => x.Id);
                },
                comment: "编号设置")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_NumberRecord",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BeModule = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "模块名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年"),
                    Month = table.Column<int>(type: "int", nullable: false, comment: "月"),
                    Day = table.Column<int>(type: "int", nullable: false, comment: "日"),
                    Serial = table.Column<int>(type: "int", nullable: false, comment: "流水"),
                    PrevSerial = table.Column<int>(type: "int", nullable: false, comment: "上一期流水"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_NumberRecord", x => x.Id);
                },
                comment: "编号记录")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_Resource",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, comment: "资源名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Size = table.Column<long>(type: "bigint", nullable: false, comment: "资源大小"),
                    Path = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, comment: "相对路径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContentType = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, comment: "资源标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MD5 = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "MD5摘要")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Uploader_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上传人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UploadTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "上传时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Resource", x => x.Id);
                },
                comment: "资源")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Super_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级角色")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "缺省角色"),
                    IsAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "管理员角色"),
                    Remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TreeCode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "树状码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Create_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    Modify_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Role", x => x.Id);
                },
                comment: "角色")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_RolePermission",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "角色ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PermissionKey = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "权限标记")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_RolePermission", x => x.Id);
                },
                comment: "角色权限")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_Setting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Setting", x => x.Id);
                },
                comment: "系统设置")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_TableMap",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FKey_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "外键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KeyName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "键名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "键值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discriminator = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_TableMap", x => x.Id);
                },
                comment: "表映射")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_Tenant",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "全称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "简称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrlMark = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "URL标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Super_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户标记")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HandIcon_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "Logo头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TreeCode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "树状码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Tenant", x => x.Id);
                },
                comment: "多租户信息")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_TenantHost",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Host = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "域名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "租户")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_TenantHost", x => x.Id);
                },
                comment: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Basis_User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Account = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "帐号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EncryptionKey = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, comment: "密钥")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "手机号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HandIcon_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否管理员"),
                    Enable = table.Column<int>(type: "int", nullable: false, comment: "启用状态"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Create_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    Modify_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_User", x => x.Id);
                },
                comment: "用户")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chat_Email",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Replay_Email_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "回复自")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FromUser_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "发件人")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_Email", x => x.Id);
                },
                comment: "邮件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chat_EmailAnnex",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "邮件ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resource_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "文件ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_EmailAnnex", x => x.Id);
                },
                comment: "邮件附件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chat_EmailContent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "邮件ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: true, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_EmailContent", x => x.Id);
                },
                comment: "邮件正文")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chat_EmailTarget",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "邮件ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    To_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "接收人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HaveRead = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "已读")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_EmailTarget", x => x.Id);
                },
                comment: "邮件收件人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chat_FriendMessage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "消息类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resource_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "图片?附件ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    From_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "发送人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MessageTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "消息时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_FriendMessage", x => x.Id);
                },
                comment: "好友消息")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chat_Group",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "群组名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LordUser_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "群主")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HandIcon_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Summary = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "简介")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Create_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    Modify_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_Group", x => x.Id);
                },
                comment: "群组")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chat_GroupManager",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Group_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "群组")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "管理员")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_GroupManager", x => x.Id);
                },
                comment: "群组管理员")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chat_GroupMessage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Group_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "群组ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "消息类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resource_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "图片?附件ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    From_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "发送人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MessageTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "消息时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_GroupMessage", x => x.Id);
                },
                comment: "群组消息")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chat_MessageTarget",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "消息ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    To_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "接收人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HaveRead = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "已读")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_MessageTarget", x => x.Id);
                },
                comment: "消息接收人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flow_FlowInstance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BeModuleName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "归属模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BeModuleText = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "单据名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bill_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "单据ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BillNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "单据编号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BillDes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "单据摘要")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "流程状态"),
                    WorkFlow_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "关联流程")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrNode_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrNodeName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "当前节点")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SponsorName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "流程发起人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sponsor_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "发起时间"),
                    IsComlete = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否已完结"),
                    CompleteTime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "完结时间"),
                    LastCheckerName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "最后审批人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastChecker_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastCheckTime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后审批时间"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow_FlowInstance", x => x.Id);
                },
                comment: "流程实例")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flow_FlowNextChecker",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowStep_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowStep")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Checker_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:User")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow_FlowNextChecker", x => x.Id);
                },
                comment: "审批时指定的下一步审核人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flow_FlowNode",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlow_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderVal = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NodeEnum = table.Column<int>(type: "int", nullable: false, comment: "节点类型"),
                    Super_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true, comment: "条件权重(为分支时)"),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: true, comment: "缺省分支(为分支时)"),
                    CheckEnum = table.Column<int>(type: "int", nullable: true, comment: "审批方式(多人时)"),
                    VoteScale = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true, comment: "通过比例(多人审批且审批方式为投票时)"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow_FlowNode", x => x.Id);
                },
                comment: "流程节点")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flow_FlowNodeChecker",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlow_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowNode_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CheckerEnum = table.Column<int>(type: "int", nullable: false, comment: "审核人类型"),
                    Checker_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "审核人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CheckerName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "审核人名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow_FlowNodeChecker", x => x.Id);
                },
                comment: "流程节点审核人/抄送人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flow_FlowNodeCond",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlow_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowNode_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FieldName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "字段名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GroupIndex = table.Column<int>(type: "int", nullable: false, comment: "条件组号"),
                    CompareEnum = table.Column<int>(type: "int", nullable: false, comment: "条件"),
                    ValueEnum = table.Column<int>(type: "int", nullable: false, comment: "值类型"),
                    Value_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "值的ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValueText = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "值的文本")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow_FlowNodeCond", x => x.Id);
                },
                comment: "流程节点条件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flow_FlowNodeEvent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlow_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowNode_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventTrigger = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "触发方式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventNotify = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "通知方式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventTarget = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "通知目标")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow_FlowNodeEvent", x => x.Id);
                },
                comment: "节点事件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flow_FlowStep",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowInstance_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowInstance")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowNode_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BeForm_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联表单的ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowNodeName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "节点名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Action = table.Column<int>(type: "int", nullable: true, comment: "动作"),
                    IsFinished = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否已办理"),
                    Operater_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "审批人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperaterName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "审批人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "时间"),
                    Desc = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "审批意见")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow_FlowStep", x => x.Id);
                },
                comment: "审批步骤")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flow_FlowStepChecker",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowStep_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowStep")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：User")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bill_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "单据ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowInstance_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowInstance")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow_FlowStepChecker", x => x.Id);
                },
                comment: "流程步骤审核人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Flow_WorkFlow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BeModule = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "适用模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BeModuleName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, comment: "模块名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Version = table.Column<int>(type: "int", nullable: false, comment: "版本"),
                    Enabled = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    Remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Create_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    Modify_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow_WorkFlow", x => x.Id);
                },
                comment: "工作流")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OA_OaLeave",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "请假单号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "申请时间"),
                    Create_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "申请人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Job_Id = table.Column<int>(type: "int", nullable: false, comment: "岗位"),
                    Dept_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "部门")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LeaveCategory = table.Column<int>(type: "int", nullable: false, comment: "请假类型"),
                    Agent_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "工作代理人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "开始时间"),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "结束时间"),
                    Days = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true, comment: "请假天数"),
                    Reasons = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "申请事由")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowStatus = table.Column<int>(type: "int", nullable: false, comment: "审批状态"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Modify_User_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    Tenant_Id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "租户ID")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OA_OaLeave", x => x.Id);
                },
                comment: "请假单")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Basis_Tenant",
                columns: new[] { "Id", "FullName", "HandIcon_Id", "ShortName", "Super_Id", "Tenant_Id", "TreeCode", "UrlMark", "isdeleted" },
                values: new object[] { "00fm5yfgzpgp93ylkuxshsc73", "默认组织", null, "default", null, null, null, "*", false });

            migrationBuilder.InsertData(
                table: "Basis_User",
                columns: new[] { "Id", "Account", "CreateTime", "Create_User_Id", "Email", "Enable", "EncryptionKey", "HandIcon_Id", "IsAdmin", "ModifyTime", "Modify_User_Id", "Name", "Password", "PhoneNumber", "Tenant_Id", "isdeleted" },
                values: new object[] { "00fm5yfgq3q893ylku6uzb57i", "admin", new DateTime(2019, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "00fm5yfgq3q893ylku6uzb57i", "gongjie@qq.com", 0, "7d9d7edd6727912ce10b976818dd2856", null, true, new DateTime(2019, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "00fm5yfgq3q893ylku6uzb57i", "管理员", "9557847e0632e2f167a143b7ab3d668a", "18675517757", null, false });

            migrationBuilder.CreateIndex(
                name: "Index_ApiRequestLog_User_Id",
                table: "Basis_ApiRequestLog",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_BillBeDept_BeDept_Id",
                table: "Basis_BillBeDept",
                column: "BeDept_Id");

            migrationBuilder.CreateIndex(
                name: "Index_BillBeDept_Bill_Id",
                table: "Basis_BillBeDept",
                column: "Bill_Id");

            migrationBuilder.CreateIndex(
                name: "Index_BillBeDept_isdeleted",
                table: "Basis_BillBeDept",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Create_User_Id",
                table: "Basis_Dept",
                column: "Create_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_isdeleted",
                table: "Basis_Dept",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Modify_User_Id",
                table: "Basis_Dept",
                column: "Modify_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Super_Id",
                table: "Basis_Dept",
                column: "Super_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Dept_Tenant_Id",
                table: "Basis_Dept",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_Dept_Id",
                table: "Basis_DeptMember",
                column: "Dept_Id");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_IsManager",
                table: "Basis_DeptMember",
                column: "IsManager");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_User_Id",
                table: "Basis_DeptMember",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Create_User_Id",
                table: "Basis_EnumItem",
                column: "Create_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_isdeleted",
                table: "Basis_EnumItem",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_IsSystemEnum",
                table: "Basis_EnumItem",
                column: "IsSystemEnum");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Modify_User_Id",
                table: "Basis_EnumItem",
                column: "Modify_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Super_Id",
                table: "Basis_EnumItem",
                column: "Super_Id");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Tenant_Id",
                table: "Basis_EnumItem",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_IsEnabled",
                table: "Basis_LoginLog",
                column: "IsEnabled");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_IsSuccessful",
                table: "Basis_LoginLog",
                column: "IsSuccessful");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_Tenant_Id",
                table: "Basis_LoginLog",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_User_Id",
                table: "Basis_LoginLog",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Create_User_Id",
                table: "Basis_Meidia",
                column: "Create_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_isdeleted",
                table: "Basis_Meidia",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_IsFolder",
                table: "Basis_Meidia",
                column: "IsFolder");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Modify_User_Id",
                table: "Basis_Meidia",
                column: "Modify_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Resource_Id",
                table: "Basis_Meidia",
                column: "Resource_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Super_Id",
                table: "Basis_Meidia",
                column: "Super_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_Tenant_Id",
                table: "Basis_Meidia",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Create_User_Id",
                table: "Basis_Notify",
                column: "Create_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_isdeleted",
                table: "Basis_Notify",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Modify_User_Id",
                table: "Basis_Notify",
                column: "Modify_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Publush_Id",
                table: "Basis_Notify",
                column: "Publush_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Resource_Id",
                table: "Basis_Notify",
                column: "Resource_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Tenant_Id",
                table: "Basis_Notify",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Notify_Type_Id",
                table: "Basis_Notify",
                column: "Type_Id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_Create_User_Id",
                table: "Basis_NumberOption",
                column: "Create_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_isdeleted",
                table: "Basis_NumberOption",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_Modify_User_Id",
                table: "Basis_NumberOption",
                column: "Modify_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_TaskDate",
                table: "Basis_NumberOption",
                column: "TaskDate");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_Tenant_Id",
                table: "Basis_NumberOption",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberRecord_isdeleted",
                table: "Basis_NumberRecord",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_NumberRecord_Tenant_Id",
                table: "Basis_NumberRecord",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Resource_Uploader_Id",
                table: "Basis_Resource",
                column: "Uploader_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Create_User_Id",
                table: "Basis_Role",
                column: "Create_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_IsAdmin",
                table: "Basis_Role",
                column: "IsAdmin");

            migrationBuilder.CreateIndex(
                name: "Index_Role_IsDefault",
                table: "Basis_Role",
                column: "IsDefault");

            migrationBuilder.CreateIndex(
                name: "Index_Role_isdeleted",
                table: "Basis_Role",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Modify_User_Id",
                table: "Basis_Role",
                column: "Modify_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Super_Id",
                table: "Basis_Role",
                column: "Super_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Role_Tenant_Id",
                table: "Basis_Role",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Role_Id",
                table: "Basis_RolePermission",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Basis_RolePermission_PermissionKey",
                table: "Basis_RolePermission",
                column: "PermissionKey");

            migrationBuilder.CreateIndex(
                name: "Index_TableMap_Discriminator",
                table: "Basis_TableMap",
                column: "Discriminator");

            migrationBuilder.CreateIndex(
                name: "Index_TableMap_FKey_Id",
                table: "Basis_TableMap",
                column: "FKey_Id");

            migrationBuilder.CreateIndex(
                name: "Index_TableMap_Value_Id",
                table: "Basis_TableMap",
                column: "Value_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_HandIcon_Id",
                table: "Basis_Tenant",
                column: "HandIcon_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_isdeleted",
                table: "Basis_Tenant",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_Super_Id",
                table: "Basis_Tenant",
                column: "Super_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_Tenant_Id",
                table: "Basis_Tenant",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_TenantHost_Tenant_Id",
                table: "Basis_TenantHost",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_User_Create_User_Id",
                table: "Basis_User",
                column: "Create_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_User_HandIcon_Id",
                table: "Basis_User",
                column: "HandIcon_Id");

            migrationBuilder.CreateIndex(
                name: "Index_User_IsAdmin",
                table: "Basis_User",
                column: "IsAdmin");

            migrationBuilder.CreateIndex(
                name: "Index_User_isdeleted",
                table: "Basis_User",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_User_Modify_User_Id",
                table: "Basis_User",
                column: "Modify_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_User_Tenant_Id",
                table: "Basis_User",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Email_FromUser_Id",
                table: "Chat_Email",
                column: "FromUser_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Email_Replay_Email_Id",
                table: "Chat_Email",
                column: "Replay_Email_Id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailAnnex_Email_Id",
                table: "Chat_EmailAnnex",
                column: "Email_Id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailAnnex_Resource_Id",
                table: "Chat_EmailAnnex",
                column: "Resource_Id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailContent_Email_Id",
                table: "Chat_EmailContent",
                column: "Email_Id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_Category",
                table: "Chat_EmailTarget",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_Email_Id",
                table: "Chat_EmailTarget",
                column: "Email_Id");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_HaveRead",
                table: "Chat_EmailTarget",
                column: "HaveRead");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_To_Id",
                table: "Chat_EmailTarget",
                column: "To_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_Category",
                table: "Chat_FriendMessage",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_From_Id",
                table: "Chat_FriendMessage",
                column: "From_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_Resource_Id",
                table: "Chat_FriendMessage",
                column: "Resource_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_Tenant_Id",
                table: "Chat_FriendMessage",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_Create_User_Id",
                table: "Chat_Group",
                column: "Create_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_HandIcon_Id",
                table: "Chat_Group",
                column: "HandIcon_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_isdeleted",
                table: "Chat_Group",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_Group_LordUser_Id",
                table: "Chat_Group",
                column: "LordUser_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_Modify_User_Id",
                table: "Chat_Group",
                column: "Modify_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_Group_Tenant_Id",
                table: "Chat_Group",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupManager_Group_Id",
                table: "Chat_GroupManager",
                column: "Group_Id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupManager_User_Id",
                table: "Chat_GroupManager",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupMessage_Category",
                table: "Chat_GroupMessage",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "Index_GroupMessage_From_Id",
                table: "Chat_GroupMessage",
                column: "From_Id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupMessage_Group_Id",
                table: "Chat_GroupMessage",
                column: "Group_Id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupMessage_Resource_Id",
                table: "Chat_GroupMessage",
                column: "Resource_Id");

            migrationBuilder.CreateIndex(
                name: "Index_GroupMessage_Tenant_Id",
                table: "Chat_GroupMessage",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_HaveRead",
                table: "Chat_MessageTarget",
                column: "HaveRead");

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_Message_Id",
                table: "Chat_MessageTarget",
                column: "Message_Id");

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_To_Id",
                table: "Chat_MessageTarget",
                column: "To_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_Bill_Id",
                table: "Flow_FlowInstance",
                column: "Bill_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_CurrNode_Id",
                table: "Flow_FlowInstance",
                column: "CurrNode_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_IsComlete",
                table: "Flow_FlowInstance",
                column: "IsComlete");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_isdeleted",
                table: "Flow_FlowInstance",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_LastChecker_Id",
                table: "Flow_FlowInstance",
                column: "LastChecker_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_Sponsor_Id",
                table: "Flow_FlowInstance",
                column: "Sponsor_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_WorkFlow_Id",
                table: "Flow_FlowInstance",
                column: "WorkFlow_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNextChecker_Checker_Id",
                table: "Flow_FlowNextChecker",
                column: "Checker_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNextChecker_FlowStep_Id",
                table: "Flow_FlowNextChecker",
                column: "FlowStep_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_IsDefault",
                table: "Flow_FlowNode",
                column: "IsDefault");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_isdeleted",
                table: "Flow_FlowNode",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_Super_Id",
                table: "Flow_FlowNode",
                column: "Super_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_WorkFlow_Id",
                table: "Flow_FlowNode",
                column: "WorkFlow_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeChecker_Checker_Id",
                table: "Flow_FlowNodeChecker",
                column: "Checker_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeChecker_FlowNode_Id",
                table: "Flow_FlowNodeChecker",
                column: "FlowNode_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeChecker_isdeleted",
                table: "Flow_FlowNodeChecker",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeChecker_WorkFlow_Id",
                table: "Flow_FlowNodeChecker",
                column: "WorkFlow_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeCond_FlowNode_Id",
                table: "Flow_FlowNodeCond",
                column: "FlowNode_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeCond_isdeleted",
                table: "Flow_FlowNodeCond",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeCond_Value_Id",
                table: "Flow_FlowNodeCond",
                column: "Value_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeCond_WorkFlow_Id",
                table: "Flow_FlowNodeCond",
                column: "WorkFlow_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeEvent_EventNotify",
                table: "Flow_FlowNodeEvent",
                column: "EventNotify");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeEvent_EventTarget",
                table: "Flow_FlowNodeEvent",
                column: "EventTarget");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeEvent_EventTrigger",
                table: "Flow_FlowNodeEvent",
                column: "EventTrigger");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeEvent_FlowNode_Id",
                table: "Flow_FlowNodeEvent",
                column: "FlowNode_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeEvent_WorkFlow_Id",
                table: "Flow_FlowNodeEvent",
                column: "WorkFlow_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_BeForm_Id",
                table: "Flow_FlowStep",
                column: "BeForm_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_FlowInstance_Id",
                table: "Flow_FlowStep",
                column: "FlowInstance_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_FlowNode_Id",
                table: "Flow_FlowStep",
                column: "FlowNode_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_isdeleted",
                table: "Flow_FlowStep",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_IsFinished",
                table: "Flow_FlowStep",
                column: "IsFinished");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_Operater_Id",
                table: "Flow_FlowStep",
                column: "Operater_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepChecker_Bill_Id",
                table: "Flow_FlowStepChecker",
                column: "Bill_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepChecker_FlowInstance_Id",
                table: "Flow_FlowStepChecker",
                column: "FlowInstance_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepChecker_FlowStep_Id",
                table: "Flow_FlowStepChecker",
                column: "FlowStep_Id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepChecker_isdeleted",
                table: "Flow_FlowStepChecker",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepChecker_User_Id",
                table: "Flow_FlowStepChecker",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_Create_User_Id",
                table: "Flow_WorkFlow",
                column: "Create_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_isdeleted",
                table: "Flow_WorkFlow",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_Modify_User_Id",
                table: "Flow_WorkFlow",
                column: "Modify_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_Tenant_Id",
                table: "Flow_WorkFlow",
                column: "Tenant_Id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Agent_Id",
                table: "OA_OaLeave",
                column: "Agent_Id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Create_User_Id",
                table: "OA_OaLeave",
                column: "Create_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Dept_Id",
                table: "OA_OaLeave",
                column: "Dept_Id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_isdeleted",
                table: "OA_OaLeave",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Job_Id",
                table: "OA_OaLeave",
                column: "Job_Id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Modify_User_Id",
                table: "OA_OaLeave",
                column: "Modify_User_Id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Tenant_Id",
                table: "OA_OaLeave",
                column: "Tenant_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basis_ApiRequestLog");

            migrationBuilder.DropTable(
                name: "Basis_BillBeDept");

            migrationBuilder.DropTable(
                name: "Basis_Dept");

            migrationBuilder.DropTable(
                name: "Basis_DeptMember");

            migrationBuilder.DropTable(
                name: "Basis_EnumItem");

            migrationBuilder.DropTable(
                name: "Basis_LoginLog");

            migrationBuilder.DropTable(
                name: "Basis_Meidia");

            migrationBuilder.DropTable(
                name: "Basis_Notify");

            migrationBuilder.DropTable(
                name: "Basis_NumberOption");

            migrationBuilder.DropTable(
                name: "Basis_NumberRecord");

            migrationBuilder.DropTable(
                name: "Basis_Resource");

            migrationBuilder.DropTable(
                name: "Basis_Role");

            migrationBuilder.DropTable(
                name: "Basis_RolePermission");

            migrationBuilder.DropTable(
                name: "Basis_Setting");

            migrationBuilder.DropTable(
                name: "Basis_TableMap");

            migrationBuilder.DropTable(
                name: "Basis_Tenant");

            migrationBuilder.DropTable(
                name: "Basis_TenantHost");

            migrationBuilder.DropTable(
                name: "Basis_User");

            migrationBuilder.DropTable(
                name: "Chat_Email");

            migrationBuilder.DropTable(
                name: "Chat_EmailAnnex");

            migrationBuilder.DropTable(
                name: "Chat_EmailContent");

            migrationBuilder.DropTable(
                name: "Chat_EmailTarget");

            migrationBuilder.DropTable(
                name: "Chat_FriendMessage");

            migrationBuilder.DropTable(
                name: "Chat_Group");

            migrationBuilder.DropTable(
                name: "Chat_GroupManager");

            migrationBuilder.DropTable(
                name: "Chat_GroupMessage");

            migrationBuilder.DropTable(
                name: "Chat_MessageTarget");

            migrationBuilder.DropTable(
                name: "Flow_FlowInstance");

            migrationBuilder.DropTable(
                name: "Flow_FlowNextChecker");

            migrationBuilder.DropTable(
                name: "Flow_FlowNode");

            migrationBuilder.DropTable(
                name: "Flow_FlowNodeChecker");

            migrationBuilder.DropTable(
                name: "Flow_FlowNodeCond");

            migrationBuilder.DropTable(
                name: "Flow_FlowNodeEvent");

            migrationBuilder.DropTable(
                name: "Flow_FlowStep");

            migrationBuilder.DropTable(
                name: "Flow_FlowStepChecker");

            migrationBuilder.DropTable(
                name: "Flow_WorkFlow");

            migrationBuilder.DropTable(
                name: "OA_OaLeave");
        }
    }
}
