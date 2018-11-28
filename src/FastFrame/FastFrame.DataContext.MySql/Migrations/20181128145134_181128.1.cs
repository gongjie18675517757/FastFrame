using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1811281 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basis_Organize");

            migrationBuilder.DropTable(
                name: "Basis_OrganizeHost");

            migrationBuilder.DropIndex(
                name: "Index_OrganizeId",
                table: "CMS_ArticleContent");

            migrationBuilder.DropIndex(
                name: "Index_OrganizeId",
                table: "Basis_QueryProgramDetail");

            migrationBuilder.DropIndex(
                name: "Index_OrganizeId",
                table: "Basis_QueryProgram");

            migrationBuilder.DropIndex(
                name: "Index_OrganizeId",
                table: "Basis_Foreign");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CMS_ArticleContent");

            migrationBuilder.DropColumn(
                name: "OrganizeId",
                table: "CMS_ArticleContent");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Basis_QueryProgramDetail");

            migrationBuilder.DropColumn(
                name: "OrganizeId",
                table: "Basis_QueryProgramDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Basis_QueryProgram");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Basis_Permission");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Basis_Foreign");

            migrationBuilder.DropColumn(
                name: "OrganizeId",
                table: "Basis_Foreign");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "CMS_Meidia",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "CMS_ArticleCategory",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "CMS_Article",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "Basis_User",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "Basis_RolePermission",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "Basis_RoleMember",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "Basis_Role",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "Basis_Resource",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "SearchProgram_Id",
                table: "Basis_QueryProgramDetail",
                newName: "QueryProgram_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "Basis_QueryProgram",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "Basis_Permission",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "Basis_Menu",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "Basis_Employee",
                newName: "Tenant_Id");

            migrationBuilder.RenameColumn(
                name: "OrganizeId",
                table: "Basis_Dept",
                newName: "Tenant_Id");

            migrationBuilder.CreateTable(
                name: "Basis_Tenant",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    EnCode = table.Column<string>(maxLength: 50, nullable: false),
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_TenantHost",
                columns: table => new
                {
                    Host = table.Column<string>(maxLength: 200, nullable: false),
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    Tenant_Id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_TenantHost", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Basis_Tenant",
                columns: new[] { "Id", "EnCode", "IsDeleted", "Name" },
                values: new object[] { "00F6P5G2VC2SAP1UJV7HTBYGU", "default", false, "默认组织" });

            migrationBuilder.InsertData(
                table: "Basis_TenantHost",
                columns: new[] { "Id", "Host", "Tenant_Id" },
                values: new object[,]
                {
                    { "00F6P5G2VC2SAP1UJV7HTBYGB", "192.168.1.100:8081", "00F6P5G2VC2SAP1UJV7HTBYGU" },
                    { "00F6P5G2VC2SAP1UJV7HTBYGc", "192.168.1.100:82", "00F6P5G2VC2SAP1UJV7HTBYGU" }
                });

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "350c4281aeee46b7981d6a0d2bc9a7c1", "159e48092577ed41d493467026a63ed9" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_TenantHost",
                column: "Tenant_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basis_Tenant");

            migrationBuilder.DropTable(
                name: "Basis_TenantHost");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "CMS_Meidia",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "CMS_ArticleCategory",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "CMS_Article",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "Basis_User",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "Basis_RolePermission",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "Basis_RoleMember",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "Basis_Role",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "Basis_Resource",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "QueryProgram_Id",
                table: "Basis_QueryProgramDetail",
                newName: "SearchProgram_Id");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Basis_QueryProgram",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "Basis_Permission",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "Basis_Menu",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "Basis_Employee",
                newName: "OrganizeId");

            migrationBuilder.RenameColumn(
                name: "Tenant_Id",
                table: "Basis_Dept",
                newName: "OrganizeId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CMS_ArticleContent",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OrganizeId",
                table: "CMS_ArticleContent",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Basis_QueryProgramDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OrganizeId",
                table: "Basis_QueryProgramDetail",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Basis_QueryProgram",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Basis_Permission",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Basis_Foreign",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OrganizeId",
                table: "Basis_Foreign",
                maxLength: 25,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Basis_Organize",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    EnCode = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Organize", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_OrganizeHost",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    Host = table.Column<string>(maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_OrganizeHost", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Basis_Organize",
                columns: new[] { "Id", "EnCode", "IsDeleted", "Name", "OrganizeId" },
                values: new object[] { "00F6P5G2VC2SAP1UJV7HTBYGU", "default", false, "默认组织", "00F6P5G2VC2SAP1UJV7HTBYGU" });

            migrationBuilder.InsertData(
                table: "Basis_OrganizeHost",
                columns: new[] { "Id", "Host", "IsDeleted", "OrganizeId" },
                values: new object[,]
                {
                    { "00F6P5G2VC2SAP1UJV7HTBYGB", "192.168.1.100:8081", false, "00F6P5G2VC2SAP1UJV7HTBYGU" },
                    { "00F6P5G2VC2SAP1UJV7HTBYGc", "192.168.1.100:82", false, "00F6P5G2VC2SAP1UJV7HTBYGU" }
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

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_QueryProgramDetail",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_QueryProgram",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_Foreign",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_Organize",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_OrganizeHost",
                column: "OrganizeId");
        }
    }
}
