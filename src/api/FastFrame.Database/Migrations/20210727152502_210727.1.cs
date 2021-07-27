using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2107271 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_basis_resourcemap",
                table: "basis_resourcemap");

            migrationBuilder.DropIndex(
                name: "Index_ResourceMap_File_Id",
                table: "basis_resourcemap");

            migrationBuilder.DropColumn(
                name: "file_id",
                table: "basis_resourcemap");

            migrationBuilder.DropColumn(
                name: "key",
                table: "basis_resourcemap");

            migrationBuilder.RenameTable(
                name: "basis_resourcemap",
                newName: "basis_tablemap");

            migrationBuilder.RenameIndex(
                name: "Index_ResourceMap_FKey_Id",
                table: "basis_tablemap",
                newName: "Index_TableMap_FKey_Id");

            migrationBuilder.AlterTable(
                name: "basis_tablemap",
                comment: "表映射",
                oldComment: "资源映射")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "md5",
                table: "basis_resource",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "MD5摘要",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "MD5摘要")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "basis_tablemap",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "keyname",
                table: "basis_tablemap",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "键名")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "value_id",
                table: "basis_tablemap",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "键值")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_basis_tablemap",
                table: "basis_tablemap",
                column: "id");

            migrationBuilder.CreateTable(
                name: "basis_setting",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_setting", x => x.id);
                },
                comment: "系统设置")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "Index_TableMap_Value_Id",
                table: "basis_tablemap",
                column: "value_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_setting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_basis_tablemap",
                table: "basis_tablemap");

            migrationBuilder.DropIndex(
                name: "Index_TableMap_Value_Id",
                table: "basis_tablemap");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "basis_tablemap");

            migrationBuilder.DropColumn(
                name: "keyname",
                table: "basis_tablemap");

            migrationBuilder.DropColumn(
                name: "value_id",
                table: "basis_tablemap");

            migrationBuilder.RenameTable(
                name: "basis_tablemap",
                newName: "basis_resourcemap");

            migrationBuilder.RenameIndex(
                name: "Index_TableMap_FKey_Id",
                table: "basis_resourcemap",
                newName: "Index_ResourceMap_FKey_Id");

            migrationBuilder.AlterTable(
                name: "basis_resourcemap",
                comment: "资源映射",
                oldComment: "表映射")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "md5",
                table: "basis_resource",
                type: "longtext",
                nullable: true,
                comment: "MD5摘要",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "MD5摘要")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "file_id",
                table: "basis_resourcemap",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "关联：Resource")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "key",
                table: "basis_resourcemap",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "标识")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_basis_resourcemap",
                table: "basis_resourcemap",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "Index_ResourceMap_File_Id",
                table: "basis_resourcemap",
                column: "file_id");
        }
    }
}
