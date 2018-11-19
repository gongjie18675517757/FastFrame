using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1811191 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Basis_Permission",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Parent_Id = table.Column<string>(maxLength: 25, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AreaName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_Role",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EnCode = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_RoleMember",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Role_Id = table.Column<string>(maxLength: 25, nullable: true),
                    User_Id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_RoleMember", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basis_RolePermission",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    OrganizeId = table.Column<string>(maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Role_Id = table.Column<string>(maxLength: 25, nullable: true),
                    Permission_Id = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basis_RolePermission", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "cdc80492ca4482af58eecd71ab1cd15a", "009425a43e464906ade6c1505a5e6446" });

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_Permission",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_Role",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_RoleMember",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "Index_OrganizeId",
                table: "Basis_RolePermission",
                column: "OrganizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basis_Permission");

            migrationBuilder.DropTable(
                name: "Basis_Role");

            migrationBuilder.DropTable(
                name: "Basis_RoleMember");

            migrationBuilder.DropTable(
                name: "Basis_RolePermission");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "f2ddd9711d0c6c633acc2dbcc6f5aef3", "05ab47835362dc6509373865e21f54e8" });
        }
    }
}
