using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFrame.Database.Migrations
{
    /// <inheritdoc />
    public partial class _2212051 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "treecode",
                table: "basis_role",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "树状码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "编码")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "encode",
                table: "basis_role",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "编码")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "treecode",
                table: "basis_enumitem",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "树状码",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "树状码")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql("set sql_safe_updates=off");
            migrationBuilder.Sql("update basis_enumitem set intkey=0 where intkey is null ");

            migrationBuilder.AlterColumn<int>(
                name: "intkey",
                table: "basis_enumitem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "字典键",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "字典键");

            migrationBuilder.AlterColumn<string>(
                name: "treecode",
                table: "basis_dept",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "树状码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "部门代码")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "encode",
                table: "basis_dept",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "部门代码")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00fm5yfgq3q893ylku6uzb57i",
                columns: new[] { "create_user_id", "modify_user_id" },
                values: new object[] { "00fm5yfgq3q893ylku6uzb57i", "00fm5yfgq3q893ylku6uzb57i" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "encode",
                table: "basis_role");

            migrationBuilder.DropColumn(
                name: "encode",
                table: "basis_dept");

            migrationBuilder.AlterColumn<string>(
                name: "treecode",
                table: "basis_role",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "编码",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "树状码")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "treecode",
                table: "basis_enumitem",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "树状码",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "树状码")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "intkey",
                table: "basis_enumitem",
                type: "int",
                nullable: true,
                comment: "字典键",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "字典键");

            migrationBuilder.AlterColumn<string>(
                name: "treecode",
                table: "basis_dept",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "部门代码",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "树状码")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "basis_user",
                keyColumn: "id",
                keyValue: "00fm5yfgq3q893ylku6uzb57i",
                columns: new[] { "create_user_id", "modify_user_id" },
                values: new object[] { null, null });
        }
    }
}
