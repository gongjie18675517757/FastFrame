using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFrame.Database.Migrations
{
    public partial class _2112161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FlowStepChecker",
                table: "FlowStepChecker");

            migrationBuilder.RenameTable(
                name: "FlowStepChecker",
                newName: "flow_flowstepchecker");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "flow_flowstepchecker",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "FlowStep_Id",
                table: "flow_flowstepchecker",
                newName: "flowstep_id");

            migrationBuilder.RenameColumn(
                name: "FlowInstance_Id",
                table: "flow_flowstepchecker",
                newName: "flowinstance_id");

            migrationBuilder.RenameColumn(
                name: "Bill_Id",
                table: "flow_flowstepchecker",
                newName: "bill_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "flow_flowstepchecker",
                newName: "id");

            migrationBuilder.AlterTable(
                name: "flow_flowstepchecker",
                comment: "流程步骤审核人")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "flow_flowstepchecker",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "关联：User",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "flowstep_id",
                table: "flow_flowstepchecker",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "关联：FlowStep",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "flowinstance_id",
                table: "flow_flowstepchecker",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "关联：FlowInstance",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "bill_id",
                table: "flow_flowstepchecker",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "单据ID",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "flow_flowstepchecker",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                comment: "",
                oldClrType: typeof(string),
                oldType: "varchar(95)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "flow_flowstepchecker",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_flow_flowstepchecker",
                table: "flow_flowstepchecker",
                column: "id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_flow_flowstepchecker",
                table: "flow_flowstepchecker");

            migrationBuilder.DropIndex(
                name: "Index_FlowStepChecker_Bill_Id",
                table: "flow_flowstepchecker");

            migrationBuilder.DropIndex(
                name: "Index_FlowStepChecker_FlowInstance_Id",
                table: "flow_flowstepchecker");

            migrationBuilder.DropIndex(
                name: "Index_FlowStepChecker_FlowStep_Id",
                table: "flow_flowstepchecker");

            migrationBuilder.DropIndex(
                name: "Index_FlowStepChecker_isdeleted",
                table: "flow_flowstepchecker");

            migrationBuilder.DropIndex(
                name: "Index_FlowStepChecker_User_Id",
                table: "flow_flowstepchecker");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "flow_flowstepchecker");

            migrationBuilder.RenameTable(
                name: "flow_flowstepchecker",
                newName: "FlowStepChecker");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "FlowStepChecker",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "flowstep_id",
                table: "FlowStepChecker",
                newName: "FlowStep_Id");

            migrationBuilder.RenameColumn(
                name: "flowinstance_id",
                table: "FlowStepChecker",
                newName: "FlowInstance_Id");

            migrationBuilder.RenameColumn(
                name: "bill_id",
                table: "FlowStepChecker",
                newName: "Bill_Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "FlowStepChecker",
                newName: "Id");

            migrationBuilder.AlterTable(
                name: "FlowStepChecker",
                oldComment: "流程步骤审核人")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "User_Id",
                table: "FlowStepChecker",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "关联：User")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FlowStep_Id",
                table: "FlowStepChecker",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "关联：FlowStep")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FlowInstance_Id",
                table: "FlowStepChecker",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "关联：FlowInstance")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Bill_Id",
                table: "FlowStepChecker",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "单据ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "FlowStepChecker",
                type: "varchar(95)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldComment: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlowStepChecker",
                table: "FlowStepChecker",
                column: "Id");
        }
    }
}
