using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2005021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "basis_tenanthost",
                keyColumn: "id",
                keyValue: "00fm5yfh942593ylkueadrwah");

            migrationBuilder.DeleteData(
                table: "basis_tenanthost",
                keyColumn: "id",
                keyValue: "00fm5yfhhpy393ylku9dk1u5b");

            migrationBuilder.DeleteData(
                table: "basis_tenanthost",
                keyColumn: "id",
                keyValue: "00fm5yfhqq1h93ylkumx3gm53");

            migrationBuilder.DropColumn(
                name: "isdisabled",
                table: "basis_user");

            migrationBuilder.DropColumn(
                name: "isroot",
                table: "basis_user");

            migrationBuilder.DropColumn(
                name: "canhavechildren",
                table: "basis_tenant");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_groupmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_group",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_friendmessage",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_email",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_user",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "enable",
                table: "basis_user",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "basis_tenant",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_role",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_resource",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_permission",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_notify",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_meidia",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_enumitem",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_dept",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "basis_loginlog",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    user_id = table.Column<string>(maxLength: 25, nullable: true),
                    logintime = table.Column<DateTime>(nullable: false),
                    lasttime = table.Column<DateTime>(nullable: false),
                    expiredtime = table.Column<DateTime>(nullable: false),
                    enable = table.Column<string>(maxLength: 50, nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_loginlog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_numberoption",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    bemodule = table.Column<string>(maxLength: 100, nullable: false),
                    prefix = table.Column<string>(maxLength: 10, nullable: true),
                    taskdate = table.Column<bool>(nullable: false),
                    seriallength = table.Column<int>(nullable: false),
                    suffix = table.Column<string>(maxLength: 10, nullable: true),
                    datefield = table.Column<string>(maxLength: 50, nullable: true),
                    datefieldtext = table.Column<string>(maxLength: 50, nullable: true),
                    fmtdate = table.Column<string>(maxLength: 50, nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_numberoption", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basis_numberrecord",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    bemodule = table.Column<string>(maxLength: 50, nullable: true),
                    year = table.Column<int>(nullable: false),
                    month = table.Column<int>(nullable: false),
                    day = table.Column<int>(nullable: false),
                    serial = table.Column<int>(nullable: false),
                    prevserial = table.Column<int>(nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_numberrecord", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flowinstance",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    version = table.Column<int>(nullable: false),
                    enabled = table.Column<string>(maxLength: 50, nullable: false),
                    bemodulename = table.Column<string>(maxLength: 50, nullable: false),
                    bemoduletext = table.Column<string>(maxLength: 50, nullable: true),
                    bill_id = table.Column<string>(maxLength: 25, nullable: false),
                    billnumber = table.Column<string>(maxLength: 50, nullable: true),
                    status = table.Column<string>(maxLength: 50, nullable: false),
                    workflow_id = table.Column<string>(maxLength: 25, nullable: false),
                    currstep_id = table.Column<string>(maxLength: 25, nullable: true),
                    sponsor_id = table.Column<string>(maxLength: 25, nullable: true),
                    starttime = table.Column<DateTime>(nullable: true),
                    iscomlete = table.Column<bool>(nullable: false),
                    completetime = table.Column<DateTime>(nullable: true),
                    lastchecker_id = table.Column<string>(maxLength: 25, nullable: true),
                    lastchecktime = table.Column<DateTime>(nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowinstance", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flowline",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    workflow_id = table.Column<string>(maxLength: 25, nullable: true),
                    text = table.Column<string>(maxLength: 100, nullable: true),
                    weights = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    from = table.Column<int>(nullable: false),
                    to = table.Column<int>(nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowline", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flowlinecond",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    flowlink_id = table.Column<string>(maxLength: 25, nullable: true),
                    fieldname = table.Column<string>(maxLength: 100, nullable: false),
                    fieldtext = table.Column<string>(maxLength: 100, nullable: false),
                    compare = table.Column<string>(maxLength: 10, nullable: false),
                    fieldvalue = table.Column<string>(maxLength: 100, nullable: false),
                    fieldvaluetext = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowlinecond", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flownode",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    workflow_id = table.Column<string>(maxLength: 25, nullable: true),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    key = table.Column<int>(nullable: false),
                    deptcheck = table.Column<bool>(nullable: false),
                    triggerappnotify = table.Column<bool>(nullable: false),
                    triggerwxnotify = table.Column<bool>(nullable: false),
                    triggersmsnotify = table.Column<bool>(nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownode", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flownodefield",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    flownode_id = table.Column<string>(maxLength: 25, nullable: true),
                    fieldname = table.Column<string>(maxLength: 100, nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownodefield", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flownoderole",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    flownode_id = table.Column<string>(maxLength: 25, nullable: true),
                    role_id = table.Column<string>(maxLength: 25, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownoderole", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flownodeuser",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    flownode_id = table.Column<string>(maxLength: 25, nullable: true),
                    user_id = table.Column<string>(maxLength: 25, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flownodeuser", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flowprocess",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    flowinstance_id = table.Column<string>(maxLength: 25, nullable: true),
                    flowstep_id = table.Column<string>(maxLength: 25, nullable: true),
                    nodekey = table.Column<int>(nullable: false),
                    flowstepname = table.Column<string>(maxLength: 50, nullable: true),
                    action = table.Column<string>(maxLength: 50, nullable: true),
                    operater_id = table.Column<string>(maxLength: 25, nullable: true),
                    operatername = table.Column<string>(maxLength: 50, nullable: true),
                    operatetime = table.Column<DateTime>(nullable: false),
                    desc = table.Column<string>(maxLength: 500, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowprocess", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flowsnapshot",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    flowinstance_id = table.Column<string>(maxLength: 25, nullable: true),
                    fkey_id = table.Column<string>(maxLength: 25, nullable: true),
                    snapshotcontent = table.Column<string>(nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowsnapshot", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flowstep",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    flowinstance_id = table.Column<string>(maxLength: 25, nullable: true),
                    flownode_id = table.Column<string>(maxLength: 25, nullable: true),
                    steporder = table.Column<int>(nullable: false),
                    stepname = table.Column<string>(maxLength: 50, nullable: true),
                    prevstep_id = table.Column<string>(maxLength: 25, nullable: true),
                    prevstepkey = table.Column<int>(nullable: false),
                    nextstep_id = table.Column<string>(maxLength: 25, nullable: true),
                    nextstepkey = table.Column<int>(nullable: false),
                    flownodekey = table.Column<int>(nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowstep", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_flowstepuser",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    flowinstance_id = table.Column<string>(maxLength: 25, nullable: true),
                    flowstep_id = table.Column<string>(maxLength: 25, nullable: true),
                    user_id = table.Column<string>(maxLength: 25, nullable: true),
                    bedept_id = table.Column<string>(maxLength: 25, nullable: true),
                    berole_id = table.Column<string>(maxLength: 25, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_flowstepuser", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flow_workflow",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    bemodule = table.Column<string>(maxLength: 100, nullable: true),
                    bemodulename = table.Column<string>(maxLength: 150, nullable: true),
                    version = table.Column<int>(nullable: false),
                    enabled = table.Column<string>(maxLength: 50, nullable: false),
                    remarks = table.Column<string>(maxLength: 500, nullable: true),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flow_workflow", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FlowInstanceDept",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FlowInstance_Id = table.Column<string>(nullable: true),
                    BeDept_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowInstanceDept", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "basis_tenant",
                keyColumn: "id",
                keyValue: "00fm5yfgzpgp93ylkuxshsc73",
                column: "urlmark",
                value: "*");

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00fm5yfgq3q893ylku6uzb57i",
                column: "tenant_id",
                value: null);

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_Tenant_Id",
                table: "basis_tenant",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_User_Id",
                table: "basis_loginlog",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_LoginLog_tenant_id",
                table: "basis_loginlog",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_Create_User_Id",
                table: "basis_numberoption",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_Modify_User_Id",
                table: "basis_numberoption",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_isdeleted",
                table: "basis_numberoption",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_NumberOption_tenant_id",
                table: "basis_numberoption",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_NumberRecord_isdeleted",
                table: "basis_numberrecord",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_NumberRecord_tenant_id",
                table: "basis_numberrecord",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_Bill_Id",
                table: "flow_flowinstance",
                column: "bill_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_CurrStep_Id",
                table: "flow_flowinstance",
                column: "currstep_id");

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
                name: "Index_FlowInstance_isdeleted",
                table: "flow_flowinstance",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowInstance_tenant_id",
                table: "flow_flowinstance",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowLine_WorkFlow_Id",
                table: "flow_flowline",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowLine_isdeleted",
                table: "flow_flowline",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowLine_tenant_id",
                table: "flow_flowline",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowLineCond_FlowLink_Id",
                table: "flow_flowlinecond",
                column: "flowlink_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_WorkFlow_Id",
                table: "flow_flownode",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_isdeleted",
                table: "flow_flownode",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNode_tenant_id",
                table: "flow_flownode",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeField_FlowNode_Id",
                table: "flow_flownodefield",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeField_isdeleted",
                table: "flow_flownodefield",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeField_tenant_id",
                table: "flow_flownodefield",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeRole_FlowNode_Id",
                table: "flow_flownoderole",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeRole_Role_Id",
                table: "flow_flownoderole",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeRole_isdeleted",
                table: "flow_flownoderole",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeRole_tenant_id",
                table: "flow_flownoderole",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeUser_FlowNode_Id",
                table: "flow_flownodeuser",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeUser_User_Id",
                table: "flow_flownodeuser",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeUser_isdeleted",
                table: "flow_flownodeuser",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowNodeUser_tenant_id",
                table: "flow_flownodeuser",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_FlowInstance_Id",
                table: "flow_flowprocess",
                column: "flowinstance_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_FlowStep_Id",
                table: "flow_flowprocess",
                column: "flowstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_Operater_Id",
                table: "flow_flowprocess",
                column: "operater_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_isdeleted",
                table: "flow_flowprocess",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowProcess_tenant_id",
                table: "flow_flowprocess",
                column: "tenant_id");

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
                name: "Index_FlowSnapshot_tenant_id",
                table: "flow_flowsnapshot",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_FlowInstance_Id",
                table: "flow_flowstep",
                column: "flowinstance_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_FlowNode_Id",
                table: "flow_flowstep",
                column: "flownode_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_NextStep_Id",
                table: "flow_flowstep",
                column: "nextstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_PrevStep_Id",
                table: "flow_flowstep",
                column: "prevstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_isdeleted",
                table: "flow_flowstep",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStep_tenant_id",
                table: "flow_flowstep",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_BeDept_Id",
                table: "flow_flowstepuser",
                column: "bedept_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_BeRole_Id",
                table: "flow_flowstepuser",
                column: "berole_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_FlowInstance_Id",
                table: "flow_flowstepuser",
                column: "flowinstance_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_FlowStep_Id",
                table: "flow_flowstepuser",
                column: "flowstep_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_User_Id",
                table: "flow_flowstepuser",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_isdeleted",
                table: "flow_flowstepuser",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_FlowStepUser_tenant_id",
                table: "flow_flowstepuser",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_Create_User_Id",
                table: "flow_workflow",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_Modify_User_Id",
                table: "flow_workflow",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_isdeleted",
                table: "flow_workflow",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_WorkFlow_tenant_id",
                table: "flow_workflow",
                column: "tenant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_loginlog");

            migrationBuilder.DropTable(
                name: "basis_numberoption");

            migrationBuilder.DropTable(
                name: "basis_numberrecord");

            migrationBuilder.DropTable(
                name: "flow_flowinstance");

            migrationBuilder.DropTable(
                name: "flow_flowline");

            migrationBuilder.DropTable(
                name: "flow_flowlinecond");

            migrationBuilder.DropTable(
                name: "flow_flownode");

            migrationBuilder.DropTable(
                name: "flow_flownodefield");

            migrationBuilder.DropTable(
                name: "flow_flownoderole");

            migrationBuilder.DropTable(
                name: "flow_flownodeuser");

            migrationBuilder.DropTable(
                name: "flow_flowprocess");

            migrationBuilder.DropTable(
                name: "flow_flowsnapshot");

            migrationBuilder.DropTable(
                name: "flow_flowstep");

            migrationBuilder.DropTable(
                name: "flow_flowstepuser");

            migrationBuilder.DropTable(
                name: "flow_workflow");

            migrationBuilder.DropTable(
                name: "FlowInstanceDept");

            migrationBuilder.DropIndex(
                name: "Index_Tenant_Tenant_Id",
                table: "basis_tenant");

            migrationBuilder.DropColumn(
                name: "enable",
                table: "basis_user");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "basis_tenant");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_groupmessage",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_group",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_friendmessage",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "chat_email",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_user",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdisabled",
                table: "basis_user",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isroot",
                table: "basis_user",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "canhavechildren",
                table: "basis_tenant",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_role",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_resource",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_permission",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_notify",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_meidia",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_enumitem",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "basis_dept",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "basis_tenant",
                keyColumn: "id",
                keyValue: "00fm5yfgzpgp93ylkuxshsc73",
                columns: new[] { "canhavechildren", "urlmark" },
                values: new object[] { true, null });

            migrationBuilder.InsertData(
                table: "basis_tenanthost",
                columns: new[] { "id", "host", "tenant_id" },
                values: new object[,]
                {
                    { "00fm5yfh942593ylkueadrwah", "*", "00fm5yfgzpgp93ylkuxshsc73" },
                    { "00fm5yfhhpy393ylku9dk1u5b", "192.168.1.100:8081", "00fm5yfgzpgp93ylkuxshsc73" },
                    { "00fm5yfhqq1h93ylkumx3gm53", "192.168.1.100:82", "00fm5yfgzpgp93ylkuxshsc73" }
                });

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00fm5yfgq3q893ylku6uzb57i",
                columns: new[] { "isroot", "tenant_id" },
                values: new object[] { true, "00fm5yfgzpgp93ylkuxshsc73" });
        }
    }
}
