using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2108211 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_rolemember");

            migrationBuilder.AddColumn<int>(
                name: "childcount",
                table: "basis_tenant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "子节点数量");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "basis_tablemap",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "childcount",
                table: "basis_meidia",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "子节点数量");

            migrationBuilder.AddColumn<int>(
                name: "childcount",
                table: "basis_enumitem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "子节点数量");

            migrationBuilder.AlterColumn<bool>(
                name: "ismanager",
                table: "basis_deptmember",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否管理",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否管理员");

            migrationBuilder.AddColumn<int>(
                name: "childcount",
                table: "basis_dept",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "子节点数量");

            migrationBuilder.CreateIndex(
                name: "Index_TableMap_Discriminator",
                table: "basis_tablemap",
                column: "Discriminator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_TableMap_Discriminator",
                table: "basis_tablemap");

            migrationBuilder.DropColumn(
                name: "childcount",
                table: "basis_tenant");

            migrationBuilder.DropColumn(
                name: "childcount",
                table: "basis_meidia");

            migrationBuilder.DropColumn(
                name: "childcount",
                table: "basis_enumitem");

            migrationBuilder.DropColumn(
                name: "childcount",
                table: "basis_dept");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "basis_tablemap",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "ismanager",
                table: "basis_deptmember",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否管理员",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否管理");

            migrationBuilder.CreateTable(
                name: "basis_rolemember",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "角色")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "用户")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_rolemember", x => x.id);
                },
                comment: "角色成员")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_Role_Id",
                table: "basis_rolemember",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "Index_RoleMember_User_Id",
                table: "basis_rolemember",
                column: "user_id");
        }
    }
}
