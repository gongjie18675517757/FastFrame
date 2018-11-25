using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1811251 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CMS_Article",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    ArticleCategory_Id = table.Column<string>(maxLength: 25, nullable: true),
                    Url = table.Column<string>(maxLength: 50, nullable: false),
                    Summarize = table.Column<string>(maxLength: 50, nullable: true),
                    Thumbnail_Id = table.Column<string>(maxLength: 25, nullable: true),
                    Content = table.Column<string>(maxLength: 200, nullable: false),
                    IsRelease = table.Column<bool>(nullable: false),
                    LookCount = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CMS_ArticleCategory",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Parent_Id = table.Column<string>(maxLength: 25, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Url = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS_ArticleCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CMS_Meidia",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Href = table.Column<string>(maxLength: 200, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMS_Meidia", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "6274d890dd46fdcb5ae31bc85e2f5221", "20bda9db5c100d25e228ad732c8443b4" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "CMS_Article",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "CMS_ArticleCategory",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "CMS_Meidia",
                column: "OrganizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CMS_Article");

            migrationBuilder.DropTable(
                name: "CMS_ArticleCategory");

            migrationBuilder.DropTable(
                name: "CMS_Meidia");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "198d53127062d93f425c3358bf674dc3", "fbe30e729fd8204dda84508da6ddfebf" });
        }
    }
}
