using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2109191 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flow_flowline");

            migrationBuilder.DropTable(
                name: "flow_flowlinecond");

            migrationBuilder.DropTable(
                name: "flow_flownodefield");

            migrationBuilder.DropTable(
                name: "flow_flownoderole");

            migrationBuilder.DropTable(
                name: "flow_flownodeuser");

            migrationBuilder.DropTable(
                name: "flow_flowstep");

            migrationBuilder.DropIndex(
                name: "Index_FlowStepUser_BeDept_Id",
                table: "flow_flowstepuser");

            migrationBuilder.DropIndex(
                name: "Index_FlowStepUser_BeRole_Id",
                table: "flow_flowstepuser");

            migrationBuilder.DropIndex(
                name: "Index_FlowStepUser_FlowStep_Id",
                table: "flow_flowstepuser");

            migrationBuilder.DropIndex(
                name: "Index_FlowProcess_FlowStep_Id",
                table: "flow_flowprocess");

            migrationBuilder.DropColumn(
                name: "name",
                table: "flow_workflow");

            migrationBuilder.DropColumn(
                name: "bedept_id",
                table: "flow_flowstepuser");

            migrationBuilder.DropColumn(
                name: "berole_id",
                table: "flow_flowstepuser");

            migrationBuilder.DropColumn(
                name: "flowstep_id",
                table: "flow_flowstepuser");

            migrationBuilder.DropColumn(
                name: "flowstep_id",
                table: "flow_flowprocess");

            migrationBuilder.DropColumn(
                name: "nodekey",
                table: "flow_flowprocess");

            migrationBuilder.DropColumn(
                name: "operatername",
                table: "flow_flowprocess");

            migrationBuilder.DropColumn(
                name: "deptcheck",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "key",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "name",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "triggerappnotify",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "triggersmsnotify",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "triggerwxnotify",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "version",
                table: "flow_flowinstance");

            migrationBuilder.AddColumn<string>(
                name: "flownode_id",
                table: "flow_flowstepuser",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "关联：FlowNode")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "checkenum",
                table: "flow_flownode",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "审批方式(多人时)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "isdefault",
                table: "flow_flownode",
                type: "tinyint(1)",
                nullable: true,
                comment: "缺省分支(为分支时)");

            migrationBuilder.AddColumn<string>(
                name: "nodeenum",
                table: "flow_flownode",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "节点类型")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "super_id",
                table: "flow_flownode",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "上级")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "flow_flownode",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "标题")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "weight",
                table: "flow_flownode",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                comment: "优先级(为分支时)");

            migrationBuilder.CreateTable(
                name: "flow_flownodechecker",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    workflow_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownode_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    checkerenum = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "审核人类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    checker_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "审核人")
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
                    workflow_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownode_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fieldname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "字段名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    compareenum = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "条件")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    valueenum = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "值类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "值的ID")
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
                    workflow_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownode_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
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
                name: "flow_flowprocessnextchecker",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowprocess_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowProcess")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    checker_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:User")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowprocessnextchecker", x => x.id);
                },
                comment: "指定的下一步审核人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_FlowNode_Id",
                table: "flow_flowstepuser",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_CheckEnum",
                table: "flow_flownode",
                column: "checkenum");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_NodeEnum",
                table: "flow_flownode",
                column: "nodeenum");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_Super_Id",
                table: "flow_flownode",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeChecker_Checker_Id",
                table: "flow_flownodechecker",
                column: "checker_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeChecker_CheckerEnum",
                table: "flow_flownodechecker",
                column: "checkerenum");

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
                name: "Index_FlowNodeCond_CompareEnum",
                table: "flow_flownodecond",
                column: "compareenum");

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
                name: "Index_FlowNodeCond_ValueEnum",
                table: "flow_flownodecond",
                column: "valueenum");

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
                name: "Index_FlowProcessNextChecker_Checker_Id",
                table: "flow_flowprocessnextchecker",
                column: "checker_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcessNextChecker_FlowProcess_Id",
                table: "flow_flowprocessnextchecker",
                column: "flowprocess_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flow_flownodechecker");

            migrationBuilder.DropTable(
                name: "flow_flownodecond");

            migrationBuilder.DropTable(
                name: "flow_flownodeevent");

            migrationBuilder.DropTable(
                name: "flow_flowprocessnextchecker");

            migrationBuilder.DropIndex(
                name: "Index_FlowStepUser_FlowNode_Id",
                table: "flow_flowstepuser");

            migrationBuilder.DropIndex(
                name: "Index_FlowNode_CheckEnum",
                table: "flow_flownode");

            migrationBuilder.DropIndex(
                name: "Index_FlowNode_NodeEnum",
                table: "flow_flownode");

            migrationBuilder.DropIndex(
                name: "Index_FlowNode_Super_Id",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "flownode_id",
                table: "flow_flowstepuser");

            migrationBuilder.DropColumn(
                name: "checkenum",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "isdefault",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "nodeenum",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "super_id",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "title",
                table: "flow_flownode");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "flow_flownode");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "flow_workflow",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "名称")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "bedept_id",
                table: "flow_flowstepuser",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "归属科室")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "berole_id",
                table: "flow_flowstepuser",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "归属角色")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "flowstep_id",
                table: "flow_flowstepuser",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "关联：FlowStep")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "flowstep_id",
                table: "flow_flowprocess",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "关联：FlowStep")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "nodekey",
                table: "flow_flowprocess",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "节点key");

            migrationBuilder.AddColumn<string>(
                name: "operatername",
                table: "flow_flowprocess",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "操作人名称")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "deptcheck",
                table: "flow_flownode",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "主管审核");

            migrationBuilder.AddColumn<int>(
                name: "key",
                table: "flow_flownode",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "节点键");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "flow_flownode",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "名称")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "triggerappnotify",
                table: "flow_flownode",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "触发应用通知");

            migrationBuilder.AddColumn<bool>(
                name: "triggersmsnotify",
                table: "flow_flownode",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "触发短信通知");

            migrationBuilder.AddColumn<bool>(
                name: "triggerwxnotify",
                table: "flow_flownode",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "触发微信通知");

            migrationBuilder.AddColumn<int>(
                name: "version",
                table: "flow_flowinstance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "版本");

            migrationBuilder.CreateTable(
                name: "flow_flowline",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    from = table.Column<int>(type: "int", nullable: false, comment: "从"),
                    text = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    to = table.Column<int>(type: "int", nullable: false, comment: "到"),
                    weights = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false, comment: "权重"),
                    workflow_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:WorkFlow")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowline", x => x.id);
                },
                comment: "流程连接线")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flowlinecond",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    compare = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, comment: "比较方式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fieldname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "字段名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fieldtext = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "字段文本")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fieldvalue = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fieldvaluetext = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "值文本")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowlink_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowLink")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowlinecond", x => x.id);
                },
                comment: "流程线条件")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flownodefield",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fieldname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "字段名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownode_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownodefield", x => x.id);
                },
                comment: "节点动态审核人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flownoderole",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownode_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:Role")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownoderole", x => x.id);
                },
                comment: "节点审核角色")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flownodeuser",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownode_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联:User")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownodeuser", x => x.id);
                },
                comment: "节点审核人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flow_flowstep",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flowinstance_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowInstance")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flownodekey = table.Column<int>(type: "int", nullable: false, comment: "节点键"),
                    flownode_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowNode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nextstepkey = table.Column<int>(type: "int", nullable: false, comment: "下一步节点键"),
                    nextstep_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowStep，下一步")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prevstepkey = table.Column<int>(type: "int", nullable: false, comment: "上一步节点键"),
                    prevstep_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "关联：FlowStep，上一步")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stepname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "步骤名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    steporder = table.Column<int>(type: "int", nullable: false, comment: "步骤序号"),
                    isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowstep", x => x.id);
                },
                comment: "流程步骤")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_BeDept_Id",
                table: "flow_flowstepuser",
                column: "bedept_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_BeRole_Id",
                table: "flow_flowstepuser",
                column: "berole_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_FlowStep_Id",
                table: "flow_flowstepuser",
                column: "flowstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_FlowStep_Id",
                table: "flow_flowprocess",
                column: "flowstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowLine_isdeleted",
                table: "flow_flowline",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowLine_WorkFlow_Id",
                table: "flow_flowline",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowLineCond_FlowLink_Id",
                table: "flow_flowlinecond",
                column: "flowlink_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeField_FlowNode_Id",
                table: "flow_flownodefield",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeField_isdeleted",
                table: "flow_flownodefield",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeRole_FlowNode_Id",
                table: "flow_flownoderole",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeRole_isdeleted",
                table: "flow_flownoderole",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeRole_Role_Id",
                table: "flow_flownoderole",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeUser_FlowNode_Id",
                table: "flow_flownodeuser",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeUser_isdeleted",
                table: "flow_flownodeuser",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeUser_User_Id",
                table: "flow_flownodeuser",
                column: "user_id");

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
                name: "Index_FlowStep_NextStep_Id",
                table: "flow_flowstep",
                column: "nextstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_PrevStep_Id",
                table: "flow_flowstep",
                column: "prevstep_id");
        }
    }
}
