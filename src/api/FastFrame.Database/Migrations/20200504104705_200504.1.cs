using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2005041 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_RolePermission_Permission_Id",
                table: "basis_rolepermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlowInstanceDept",
                table: "FlowInstanceDept");

            migrationBuilder.DropColumn(
                name: "permission_id",
                table: "basis_rolepermission");

            migrationBuilder.RenameTable(
                name: "FlowInstanceDept",
                newName: "flow_flowinstancedept");

            migrationBuilder.RenameColumn(
                name: "FlowInstance_Id",
                table: "flow_flowinstancedept",
                newName: "flowinstance_id");

            migrationBuilder.RenameColumn(
                name: "BeDept_Id",
                table: "flow_flowinstancedept",
                newName: "bedept_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "flow_flowinstancedept",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "permissionkey",
                table: "basis_rolepermission",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "superpermissionkey",
                table: "basis_rolepermission",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "flowinstance_id",
                table: "flow_flowinstancedept",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "bedept_id",
                table: "flow_flowinstancedept",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "flow_flowinstancedept",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(95) CHARACTER SET utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "flow_flowinstancedept",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "flow_flowinstancedept",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_flow_flowinstancedept",
                table: "flow_flowinstancedept",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_basis_rolepermission_permissionkey",
                table: "basis_rolepermission",
                column: "permissionkey");

            migrationBuilder.CreateIndex(
                name: "IX_basis_rolepermission_superpermissionkey",
                table: "basis_rolepermission",
                column: "superpermissionkey");

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
                name: "Index_FlowInstanceDept_tenant_id",
                table: "flow_flowinstancedept",
                column: "tenant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_basis_rolepermission_permissionkey",
                table: "basis_rolepermission");

            migrationBuilder.DropIndex(
                name: "IX_basis_rolepermission_superpermissionkey",
                table: "basis_rolepermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_flow_flowinstancedept",
                table: "flow_flowinstancedept");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstanceDept_BeDept_Id",
                table: "flow_flowinstancedept");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstanceDept_FlowInstance_Id",
                table: "flow_flowinstancedept");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstanceDept_isdeleted",
                table: "flow_flowinstancedept");

            migrationBuilder.DropIndex(
                name: "Index_FlowInstanceDept_tenant_id",
                table: "flow_flowinstancedept");

            migrationBuilder.DropColumn(
                name: "permissionkey",
                table: "basis_rolepermission");

            migrationBuilder.DropColumn(
                name: "superpermissionkey",
                table: "basis_rolepermission");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "flow_flowinstancedept");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "flow_flowinstancedept");

            migrationBuilder.RenameTable(
                name: "flow_flowinstancedept",
                newName: "FlowInstanceDept");

            migrationBuilder.RenameColumn(
                name: "flowinstance_id",
                table: "FlowInstanceDept",
                newName: "FlowInstance_Id");

            migrationBuilder.RenameColumn(
                name: "bedept_id",
                table: "FlowInstanceDept",
                newName: "BeDept_Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "FlowInstanceDept",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "permission_id",
                table: "basis_rolepermission",
                type: "varchar(25) CHARACTER SET utf8mb4",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FlowInstance_Id",
                table: "FlowInstanceDept",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BeDept_Id",
                table: "FlowInstanceDept",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "FlowInstanceDept",
                type: "varchar(95) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlowInstanceDept",
                table: "FlowInstanceDept",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "Index_RolePermission_Permission_Id",
                table: "basis_rolepermission",
                column: "permission_id");
        }
    }
}
