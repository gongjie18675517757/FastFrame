using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFrame.Database.Migrations
{
    public partial class _2112011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flow_flowinstancedept");

            migrationBuilder.DropTable(
                name: "flow_flowprocess");

            migrationBuilder.DropTable(
                name: "flow_flowprocessnextchecker");

            migrationBuilder.DropTable(
                name: "flow_flowsnapshot");

            migrationBuilder.DropTable(
                name: "flow_flowstepuser");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstance_CurrStep_Id",
                table: "flow_flowinstance");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstance_Enabled",
                table: "flow_flowinstance");

            migrationBuilder.DropColumn(
                name: "currstep_id",
                table: "flow_flowinstance");

            migrationBuilder.DropColumn(
                name: "enabled",
                table: "flow_flowinstance");

            migrationBuilder.AlterColumn<decimal>(
                name: "weight",
                table: "flow_flownode",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                comment: "条件权重(为分支时)",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true,
                oldComment: "优先级(为分支时)");

            migrationBuilder.AddColumn<decimal>(
                name: "votescale",
                table: "flow_flownode",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                comment: "通过比例(多人审批且审批方式为投票时)");

            migrationBuilder.AlterColumn<string>(
                name: "sponsor_id",
                table: "flow_flowinstance",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "流程发起人")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "lastchecker_id",
                table: "flow_flowinstance",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "最后审批人")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "billdes",
                table: "flow_flowinstance",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "单据摘要")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "currnode_id",
                table: "flow_flowinstance",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "currnodename",
                table: "flow_flowinstance",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "当前节点")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "lastcheckername",
                table: "flow_flowinstance",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "最后审批人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "sponsorname",
                table: "flow_flowinstance",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "流程发起人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basis_billbedept",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bill_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：Bill")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bedept_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "归属部门")
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
                name: "flow_flownextchecker",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowstep_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowStep")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    checker_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:User")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownextchecker", x => x.id);
                },
                comment: "审批时指定的下一步审核人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flowstep",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowinstance_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowInstance")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownode_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    beform_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联表单的ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownodename = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "节点名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "动作")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isfinished = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否已办理"),
                    operater_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "审批人")
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
                name: "FlowStepChecker",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(95)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowStep_Id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    User_Id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bill_Id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowInstance_Id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowStepChecker", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_IsDefault",
                table: "flow_flownode",
                column: "isdefault");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_CurrNode_Id",
                table: "flow_flowinstance",
                column: "currnode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_IsComlete",
                table: "flow_flowinstance",
                column: "iscomlete");

            migrationBuilder.CreateIndex(
                name: "Index_MessageTarget_HaveRead",
                table: "chat_messagetarget",
                column: "haveread");

            migrationBuilder.CreateIndex(
                name: "Index_EmailTarget_HaveRead",
                table: "chat_emailtarget",
                column: "haveread");

            migrationBuilder.CreateIndex(
                name: "Index_User_IsAdmin",
                table: "basis_user",
                column: "isadmin");

            migrationBuilder.CreateIndex(
                name: "Index_Role_IsAdmin",
                table: "basis_role",
                column: "isadmin");

            migrationBuilder.CreateIndex(
                name: "Index_Role_IsDefault",
                table: "basis_role",
                column: "isdefault");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_TaskDate",
                table: "basis_numberoption",
                column: "taskdate");

            migrationBuilder.CreateIndex(
                name: "Index_Meidia_IsFolder",
                table: "basis_meidia",
                column: "isfolder");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_IsEnabled",
                table: "basis_loginlog",
                column: "isenabled");

            migrationBuilder.CreateIndex(
                name: "Index_DeptMember_IsManager",
                table: "basis_deptmember",
                column: "ismanager");

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
                name: "Index_FlowNextChecker_Checker_Id",
                table: "flow_flownextchecker",
                column: "checker_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNextChecker_FlowStep_Id",
                table: "flow_flownextchecker",
                column: "flowstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_Action",
                table: "flow_flowstep",
                column: "action");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_billbedept");

            migrationBuilder.DropTable(
                name: "flow_flownextchecker");

            migrationBuilder.DropTable(
                name: "flow_flowstep");

            migrationBuilder.DropTable(
                name: "FlowStepChecker");

            migrationBuilder.DropIndex(
                name: "Index_FlowNode_IsDefault",
                table: "flow_flownode");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstance_CurrNode_Id",
                table: "flow_flowinstance");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstance_IsComlete",
                table: "flow_flowinstance");

            migrationBuilder.DropIndex(
                name: "Index_MessageTarget_HaveRead",
                table: "chat_messagetarget");

            migrationBuilder.DropIndex(
                name: "Index_EmailTarget_HaveRead",
                table: "chat_emailtarget");

            migrationBuilder.DropIndex(
                name: "Index_User_IsAdmin",
                table: "basis_user");

            migrationBuilder.DropIndex(
                name: "Index_Role_IsAdmin",
                table: "basis_role");

            migrationBuilder.DropIndex(
                name: "Index_Role_IsDefault",
                table: "basis_role");

            migrationBuilder.DropIndex(
                name: "Index_NumberOption_TaskDate",
                table: "basis_numberoption");

            migrationBuilder.DropIndex(
                name: "Index_Meidia_IsFolder",
                table: "basis_meidia");

            migrationBuilder.DropIndex(
                name: "Index_LoginLog_IsEnabled",
                table: "basis_loginlog");

            migrationBuilder.DropIndex(
                name: "Index_DeptMember_IsManager",
                table: "basis_deptmember");

            migrationBuilder.DropColumn(
                name: "votescale",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "billdes",
                table: "flow_flowinstance");

            migrationBuilder.DropColumn(
                name: "currnode_id",
                table: "flow_flowinstance");

            migrationBuilder.DropColumn(
                name: "currnodename",
                table: "flow_flowinstance");

            migrationBuilder.DropColumn(
                name: "lastcheckername",
                table: "flow_flowinstance");

            migrationBuilder.DropColumn(
                name: "sponsorname",
                table: "flow_flowinstance");

            migrationBuilder.AlterColumn<decimal>(
                name: "weight",
                table: "flow_flownode",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                comment: "优先级(为分支时)",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true,
                oldComment: "条件权重(为分支时)");

            migrationBuilder.AlterColumn<string>(
                name: "sponsor_id",
                table: "flow_flowinstance",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "流程发起人",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "lastchecker_id",
                table: "flow_flowinstance",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "最后审批人",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "currstep_id",
                table: "flow_flowinstance",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "当前步骤")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "enabled",
                table: "flow_flowinstance",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "状态")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flowinstancedept",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bedept_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "归属部门")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowinstance_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowInstance")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowinstancedept", x => x.id);
                },
                comment: "流程实例归属科室")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flowprocess",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "动作")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    desc = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowinstance_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowInstance")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowstepname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "步骤名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    operatetime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "时间"),
                    operater_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "操作人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowprocess", x => x.id);
                },
                comment: "审批过程")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flowprocessnextchecker",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    checker_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:User")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowprocess_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowProcess")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowprocessnextchecker", x => x.id);
                },
                comment: "指定的下一步审核人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flowsnapshot",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fkey_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "外键：流程ID，或者单据ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowinstance_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowInstance")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    snapshotcontent = table.Column<string>(type: "longtext", nullable: true, comment: "快照内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowsnapshot", x => x.id);
                },
                comment: "流程快照")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flowstepuser",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowinstance_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowInstance")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownode_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：User")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowstepuser", x => x.id);
                },
                comment: "流程步骤审核人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_CurrStep_Id",
                table: "flow_flowinstance",
                column: "currstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_Enabled",
                table: "flow_flowinstance",
                column: "enabled");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstanceDept_BeDept_Id",
                table: "flow_flowinstancedept",
                column: "bedept_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstanceDept_FlowInstance_Id",
                table: "flow_flowinstancedept",
                column: "flowinstance_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstanceDept_isdeleted",
                table: "flow_flowinstancedept",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_Action",
                table: "flow_flowprocess",
                column: "action");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_FlowInstance_Id",
                table: "flow_flowprocess",
                column: "flowinstance_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_isdeleted",
                table: "flow_flowprocess",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_Operater_Id",
                table: "flow_flowprocess",
                column: "operater_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcessNextChecker_Checker_Id",
                table: "flow_flowprocessnextchecker",
                column: "checker_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcessNextChecker_FlowProcess_Id",
                table: "flow_flowprocessnextchecker",
                column: "flowprocess_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowSnapshot_FKey_Id",
                table: "flow_flowsnapshot",
                column: "fkey_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowSnapshot_FlowInstance_Id",
                table: "flow_flowsnapshot",
                column: "flowinstance_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowSnapshot_isdeleted",
                table: "flow_flowsnapshot",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_FlowInstance_Id",
                table: "flow_flowstepuser",
                column: "flowinstance_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_FlowNode_Id",
                table: "flow_flowstepuser",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_isdeleted",
                table: "flow_flowstepuser",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_User_Id",
                table: "flow_flowstepuser",
                column: "user_id");
        }
    }
}
