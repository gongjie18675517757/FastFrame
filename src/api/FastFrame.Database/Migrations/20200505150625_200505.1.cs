using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2005051 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_permission");

            migrationBuilder.CreateTable(
                name: "basis_reourcemap",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    file_id = table.Column<string>(maxLength: 25, nullable: true),
                    fkey_id = table.Column<string>(maxLength: 25, nullable: true),
                    key = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_reourcemap", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "oa_oaleave",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    modify_user_id = table.Column<string>(maxLength: 25, nullable: true),
                    modifytime = table.Column<DateTime>(nullable: false),
                    number = table.Column<string>(maxLength: 20, nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 25, nullable: false),
                    job_id = table.Column<string>(maxLength: 25, nullable: false),
                    dept_id = table.Column<string>(maxLength: 25, nullable: false),
                    leavecategory = table.Column<string>(maxLength: 50, nullable: false),
                    agent_id = table.Column<string>(maxLength: 25, nullable: false),
                    starttime = table.Column<DateTime>(nullable: false),
                    endtime = table.Column<DateTime>(nullable: false),
                    days = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    reasons = table.Column<string>(maxLength: 500, nullable: true),
                    flowstatus = table.Column<string>(maxLength: 50, nullable: false),
                    isdeleted = table.Column<bool>(nullable: false),
                    tenant_id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oa_oaleave", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "Index_ReourceMap_FKey_Id",
                table: "basis_reourcemap",
                column: "fkey_id");

            migrationBuilder.CreateIndex(
                name: "Index_ReourceMap_File_Id",
                table: "basis_reourcemap",
                column: "file_id");

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
                name: "Index_OaLeave_Job_Id",
                table: "oa_oaleave",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_Modify_User_Id",
                table: "oa_oaleave",
                column: "modify_user_id");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_isdeleted",
                table: "oa_oaleave",
                column: "isdeleted");

            migrationBuilder.CreateIndex(
                name: "Index_OaLeave_tenant_id",
                table: "oa_oaleave",
                column: "tenant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_reourcemap");

            migrationBuilder.DropTable(
                name: "oa_oaleave");

            migrationBuilder.CreateTable(
                name: "basis_permission",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25) CHARACTER SET utf8mb4", maxLength: 25, nullable: false),
                    areaname = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: false),
                    encode = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: false),
                    super_id = table.Column<string>(type: "varchar(25) CHARACTER SET utf8mb4", maxLength: 25, nullable: true),
                    tenant_id = table.Column<string>(type: "varchar(25) CHARACTER SET utf8mb4", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_permission", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "Index_Permission_Super_Id",
                table: "basis_permission",
                column: "super_id");

            migrationBuilder.CreateIndex(
                name: "Index_Permission_tenant_id",
                table: "basis_permission",
                column: "tenant_id");
        }
    }
}
