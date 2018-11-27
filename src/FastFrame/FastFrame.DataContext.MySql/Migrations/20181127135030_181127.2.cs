using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1811272 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "CMS_Article");

            migrationBuilder.AddColumn<string>(
                name: "ArticleContent_Id",
                table: "CMS_Article",
                maxLength: 25,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CMS_ArticleContent",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS_ArticleContent", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "b93aa30467287a9d67917b4fc26c7558", "b2675064e48182c349d4fc88f54fb609" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "CMS_ArticleContent",
                column: "OrganizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CMS_ArticleContent");

            migrationBuilder.DropColumn(
                name: "ArticleContent_Id",
                table: "CMS_Article");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "CMS_Article",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "352a093e83c82849c534e4dfa7af6611", "c306f2936fd13b1f1af8d67a450a9807" });
        }
    }
}
