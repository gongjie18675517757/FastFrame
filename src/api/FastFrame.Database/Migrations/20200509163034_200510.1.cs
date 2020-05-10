using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _2005101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_reourcemap");

            migrationBuilder.CreateTable(
                name: "basis_resourcemap",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 25, nullable: false),
                    file_id = table.Column<string>(maxLength: 25, nullable: true),
                    fkey_id = table.Column<string>(maxLength: 25, nullable: true),
                    key = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_resourcemap", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "Index_ResourceMap_FKey_Id",
                table: "basis_resourcemap",
                column: "fkey_id");

            migrationBuilder.CreateIndex(
                name: "Index_ResourceMap_File_Id",
                table: "basis_resourcemap",
                column: "file_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basis_resourcemap");

            migrationBuilder.CreateTable(
                name: "basis_reourcemap",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(25) CHARACTER SET utf8mb4", maxLength: 25, nullable: false),
                    fkey_id = table.Column<string>(type: "varchar(25) CHARACTER SET utf8mb4", maxLength: 25, nullable: true),
                    file_id = table.Column<string>(type: "varchar(25) CHARACTER SET utf8mb4", maxLength: 25, nullable: true),
                    key = table.Column<string>(type: "varchar(20) CHARACTER SET utf8mb4", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basis_reourcemap", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "Index_ReourceMap_FKey_Id",
                table: "basis_reourcemap",
                column: "fkey_id");

            migrationBuilder.CreateIndex(
                name: "Index_ReourceMap_File_Id",
                table: "basis_reourcemap",
                column: "file_id");
        }
    }
}
