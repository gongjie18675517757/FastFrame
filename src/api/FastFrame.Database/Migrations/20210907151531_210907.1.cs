using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2109071 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_FlowStepUser_tenant_id",
                table: "flow_flowstepuser");

            migrationBuilder.DropIndex(
                name: "Index_FlowStep_tenant_id",
                table: "flow_flowstep");

            migrationBuilder.DropIndex(
                name: "Index_FlowSnapshot_tenant_id",
                table: "flow_flowsnapshot");

            migrationBuilder.DropIndex(
                name: "Index_FlowProcess_tenant_id",
                table: "flow_flowprocess");

            migrationBuilder.DropIndex(
                name: "Index_FlowNodeUser_tenant_id",
                table: "flow_flownodeuser");

            migrationBuilder.DropIndex(
                name: "Index_FlowNodeRole_tenant_id",
                table: "flow_flownoderole");

            migrationBuilder.DropIndex(
                name: "Index_FlowNodeField_tenant_id",
                table: "flow_flownodefield");

            migrationBuilder.DropIndex(
                name: "Index_FlowNode_tenant_id",
                table: "flow_flownode");

            migrationBuilder.DropIndex(
                name: "Index_FlowLine_tenant_id",
                table: "flow_flowline");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstanceDept_tenant_id",
                table: "flow_flowinstancedept");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstance_tenant_id",
                table: "flow_flowinstance");

            migrationBuilder.DropIndex(
                name: "Index_Email_tenant_id",
                table: "chat_email");

            migrationBuilder.DropIndex(
                name: "Index_Resource_tenant_id",
                table: "basis_resource");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flowstepuser");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flowstep");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flowsnapshot");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flowprocess");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flownodeuser");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flownoderole");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flownodefield");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flowline");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flowinstancedept");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flowinstance");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "chat_email");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "basis_resource");

            migrationBuilder.RenameIndex(
                name: "Index_OaLeave_tenant_id",
                table: "oa_oaleave",
                newName: "Index_OaLeave_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_WorkFlow_tenant_id",
                table: "flow_workflow",
                newName: "Index_WorkFlow_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_GroupMessage_tenant_id",
                table: "chat_groupmessage",
                newName: "Index_GroupMessage_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_Group_tenant_id",
                table: "chat_group",
                newName: "Index_Group_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_FriendMessage_tenant_id",
                table: "chat_friendmessage",
                newName: "Index_FriendMessage_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_User_tenant_id",
                table: "basis_user",
                newName: "Index_User_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_Role_tenant_id",
                table: "basis_role",
                newName: "Index_Role_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_NumberRecord_tenant_id",
                table: "basis_numberrecord",
                newName: "Index_NumberRecord_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_NumberOption_tenant_id",
                table: "basis_numberoption",
                newName: "Index_NumberOption_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_Notify_tenant_id",
                table: "basis_notify",
                newName: "Index_Notify_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_Meidia_tenant_id",
                table: "basis_meidia",
                newName: "Index_Meidia_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_LoginLog_tenant_id",
                table: "basis_loginlog",
                newName: "Index_LoginLog_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_EnumItem_tenant_id",
                table: "basis_enumitem",
                newName: "Index_EnumItem_Tenant_Id");

            migrationBuilder.RenameIndex(
                name: "Index_Dept_tenant_id",
                table: "basis_dept",
                newName: "Index_Dept_Tenant_Id");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "oa_oaleave",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "租户ID",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "flow_workflow",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "租户ID",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_groupmessage",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_group",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "租户ID",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_friendmessage",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_user",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "租户ID",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_role",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "租户ID",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_numberrecord",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_numberoption",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "租户ID",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_notify",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "租户ID",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_meidia",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "租户ID",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_loginlog",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ipaddress",
                table: "basis_loginlog",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "IP",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true,
                oldComment: "IP")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_enumitem",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "租户ID",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_dept",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "租户ID",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_treechild",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    treename = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "树名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    super_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "上级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    child_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "下级")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_treechild", x => x.id);
                },
                comment: "树的递归下级")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_FlowStatus",
                table: "oa_oaleave",
                column: "flowstatus");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_LeaveCategory",
                table: "oa_oaleave",
                column: "leavecategory");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_Enabled",
                table: "flow_workflow",
                column: "enabled");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_Action",
                table: "flow_flowprocess",
                column: "action");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_Enabled",
                table: "flow_flowinstance",
                column: "enabled");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_Status",
                table: "flow_flowinstance",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "Index_GroupMessage_Category",
                table: "chat_groupmessage",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "Index_FriendMessage_Category",
                table: "chat_friendmessage",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_Category",
                table: "chat_emailtarget",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "Index_User_Enable",
                table: "basis_user",
                column: "enable");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_FmtDate",
                table: "basis_numberoption",
                column: "fmtdate");

            migrationBuilder.CreateIndex(
                name: "Index_EnumItem_Key",
                table: "basis_enumitem",
                column: "key");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_treechild");

            migrationBuilder.DropIndex(
                name: "Index_OaLeave_FlowStatus",
                table: "oa_oaleave");

            migrationBuilder.DropIndex(
                name: "Index_OaLeave_LeaveCategory",
                table: "oa_oaleave");

            migrationBuilder.DropIndex(
                name: "Index_WorkFlow_Enabled",
                table: "flow_workflow");

            migrationBuilder.DropIndex(
                name: "Index_FlowProcess_Action",
                table: "flow_flowprocess");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstance_Enabled",
                table: "flow_flowinstance");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstance_Status",
                table: "flow_flowinstance");

            migrationBuilder.DropIndex(
                name: "Index_GroupMessage_Category",
                table: "chat_groupmessage");

            migrationBuilder.DropIndex(
                name: "Index_FriendMessage_Category",
                table: "chat_friendmessage");

            migrationBuilder.DropIndex(
                name: "Index_EmailTarget_Category",
                table: "chat_emailtarget");

            migrationBuilder.DropIndex(
                name: "Index_User_Enable",
                table: "basis_user");

            migrationBuilder.DropIndex(
                name: "Index_NumberOption_FmtDate",
                table: "basis_numberoption");

            migrationBuilder.DropIndex(
                name: "Index_EnumItem_Key",
                table: "basis_enumitem");

            migrationBuilder.RenameIndex(
                name: "Index_OaLeave_Tenant_Id",
                table: "oa_oaleave",
                newName: "Index_OaLeave_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_WorkFlow_Tenant_Id",
                table: "flow_workflow",
                newName: "Index_WorkFlow_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_GroupMessage_Tenant_Id",
                table: "chat_groupmessage",
                newName: "Index_GroupMessage_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_Group_Tenant_Id",
                table: "chat_group",
                newName: "Index_Group_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_FriendMessage_Tenant_Id",
                table: "chat_friendmessage",
                newName: "Index_FriendMessage_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_User_Tenant_Id",
                table: "basis_user",
                newName: "Index_User_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_Role_Tenant_Id",
                table: "basis_role",
                newName: "Index_Role_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_NumberRecord_Tenant_Id",
                table: "basis_numberrecord",
                newName: "Index_NumberRecord_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_NumberOption_Tenant_Id",
                table: "basis_numberoption",
                newName: "Index_NumberOption_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_Notify_Tenant_Id",
                table: "basis_notify",
                newName: "Index_Notify_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_Meidia_Tenant_Id",
                table: "basis_meidia",
                newName: "Index_Meidia_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_LoginLog_Tenant_Id",
                table: "basis_loginlog",
                newName: "Index_LoginLog_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_EnumItem_Tenant_Id",
                table: "basis_enumitem",
                newName: "Index_EnumItem_tenant_id");

            migrationBuilder.RenameIndex(
                name: "Index_Dept_Tenant_Id",
                table: "basis_dept",
                newName: "Index_Dept_tenant_id");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "oa_oaleave",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "租户ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "flow_workflow",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "租户ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flowstepuser",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flowstep",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flowsnapshot",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flowprocess",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flownodeuser",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flownoderole",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flownodefield",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flownode",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flowline",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flowinstancedept",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flowinstance",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_groupmessage",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_group",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "租户ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_friendmessage",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "chat_email",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_user",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "租户ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_role",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "租户ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "basis_resource",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_numberrecord",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_numberoption",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "租户ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_notify",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "租户ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_meidia",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "租户ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_loginlog",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ipaddress",
                table: "basis_loginlog",
                type: "varchar(45)",
                nullable: true,
                comment: "IP",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "IP")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_enumitem",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "租户ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_dept",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "租户ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_tenant_id",
                table: "flow_flowstepuser",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_tenant_id",
                table: "flow_flowstep",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowSnapshot_tenant_id",
                table: "flow_flowsnapshot",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_tenant_id",
                table: "flow_flowprocess",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeUser_tenant_id",
                table: "flow_flownodeuser",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeRole_tenant_id",
                table: "flow_flownoderole",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeField_tenant_id",
                table: "flow_flownodefield",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_tenant_id",
                table: "flow_flownode",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowLine_tenant_id",
                table: "flow_flowline",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstanceDept_tenant_id",
                table: "flow_flowinstancedept",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_tenant_id",
                table: "flow_flowinstance",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Email_tenant_id",
                table: "chat_email",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_Resource_tenant_id",
                table: "basis_resource",
                column: "tenant_id");
        }
    }
}
