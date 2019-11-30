using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1903061 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_notify",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "publush_id",
                table: "chat_notify",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "chat_notify",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "chat_notify",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_notify",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "to_id",
                table: "chat_messagetarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "message_id",
                table: "chat_messagetarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_messagetarget",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "resource_id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "group_id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "from_id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "chat_groupmanager",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "group_id",
                table: "chat_groupmanager",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_groupmanager",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "lorduser_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "handicon_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_group",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_friendmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "resource_id",
                table: "chat_friendmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "from_id",
                table: "chat_friendmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_friendmessage",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "to_id",
                table: "chat_emailtarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "email_id",
                table: "chat_emailtarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_emailtarget",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "email_id",
                table: "chat_emailcontent",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_emailcontent",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "resource_id",
                table: "chat_emailannex",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "email_id",
                table: "chat_emailannex",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_emailannex",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_email",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "replay_email_id",
                table: "chat_email",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "fromuser_id",
                table: "chat_email",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_email",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "handicon_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_user",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_tenanthost",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_tenanthost",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "parent_id",
                table: "basis_tenant",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "handicon_id",
                table: "basis_tenant",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_tenant",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "permission_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_role",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_role",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_role",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_role",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_resource",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_resource",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_permission",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "parent_id",
                table: "basis_permission",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_permission",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "to_id",
                table: "basis_notifytarget",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "notify_id",
                table: "basis_notifytarget",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_notifytarget",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "resource_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "parent_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "basis_employee",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_employee",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_employee",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "dept_id",
                table: "basis_employee",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_employee",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_employee",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "supervisor_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "parent_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_dept",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.CreateTable(
                name: "basis_enumitem",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    enumname = table.Column<string>(maxLength: 150, nullable: true),
                    enumvalue = table.Column<string>(maxLength: 200, nullable: true),
                    parent_id = table.Column<string>(maxLength: 25, nullable: true),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_enumitem", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "encryptionkey", "password" },
                values: new object[] { "26de805da1bc47adea2894e8eec0e027", "03c44f54f87841ec4175d82ce4ccf161" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "basis_enumitem",
                column: "tenant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_enumitem");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_notify",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "publush_id",
                table: "chat_notify",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "chat_notify",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "chat_notify",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_notify",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "to_id",
                table: "chat_messagetarget",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "message_id",
                table: "chat_messagetarget",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_messagetarget",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "resource_id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "group_id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "from_id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "chat_groupmanager",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "group_id",
                table: "chat_groupmanager",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_groupmanager",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "lorduser_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "handicon_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_group",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_friendmessage",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "resource_id",
                table: "chat_friendmessage",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "from_id",
                table: "chat_friendmessage",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_friendmessage",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "to_id",
                table: "chat_emailtarget",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email_id",
                table: "chat_emailtarget",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_emailtarget",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "email_id",
                table: "chat_emailcontent",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_emailcontent",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "resource_id",
                table: "chat_emailannex",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email_id",
                table: "chat_emailannex",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_emailannex",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_email",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "replay_email_id",
                table: "chat_email",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "fromuser_id",
                table: "chat_email",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "chat_email",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "handicon_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_user",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_tenanthost",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_tenanthost",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "parent_id",
                table: "basis_tenant",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "handicon_id",
                table: "basis_tenant",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_tenant",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "permission_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_rolepermission",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_rolemember",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_role",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_role",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_role",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_role",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_resource",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_resource",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_permission",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parent_id",
                table: "basis_permission",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_permission",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "to_id",
                table: "basis_notifytarget",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "notify_id",
                table: "basis_notifytarget",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_notifytarget",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "resource_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parent_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "basis_employee",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_employee",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_employee",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dept_id",
                table: "basis_employee",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_employee",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_employee",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "supervisor_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parent_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "modify_user_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_user_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "basis_dept",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "encryptionkey", "password" },
                values: new object[] { "0796597ef9ab8e60e8de8f747358f38b", "5bd0f30d0fb4d72e595070db5021628f" });
        }
    }
}
