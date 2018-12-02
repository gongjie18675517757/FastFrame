using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastFrame.Database.Migrations
{
    public partial class _1812021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Replay_Email_Id = table.Column<string>(nullable: true),
                    FromUser_Id = table.Column<string>(nullable: true),
                    Tenant_Id = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                });
            
            migrationBuilder.CreateTable(
                name: "EmailAnnex",
                columns: table => new
                {
                    Email_Id = table.Column<string>(nullable: true),
                    Resource_Id = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAnnex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailContent",
                columns: table => new
                {
                    Email_Id = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTarget",
                columns: table => new
                {
                    To_Id = table.Column<string>(nullable: true),
                    HaveRead = table.Column<bool>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Email_Id = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTarget", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendMessage",
                columns: table => new
                {
                    Content = table.Column<string>(maxLength: 500, nullable: false),
                    Category = table.Column<int>(nullable: false),
                    Resource_Id = table.Column<string>(nullable: true),
                    From_Id = table.Column<string>(nullable: true),
                    MessageTime = table.Column<DateTime>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Tenant_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Tenant_Id = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    LordUser_Id = table.Column<string>(nullable: true),
                    HandIcon_Id = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupManager",
                columns: table => new
                {
                    Group_Id = table.Column<string>(nullable: true),
                    User_Id = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupManager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessage",
                columns: table => new
                {
                    Content = table.Column<string>(maxLength: 500, nullable: false),
                    Category = table.Column<int>(nullable: false),
                    Resource_Id = table.Column<string>(nullable: true),
                    From_Id = table.Column<string>(nullable: true),
                    MessageTime = table.Column<DateTime>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Tenant_Id = table.Column<string>(nullable: true),
                    Group_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTarget",
                columns: table => new
                {
                    To_Id = table.Column<string>(nullable: true),
                    HaveRead = table.Column<bool>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Message_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTarget", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notify",
                columns: table => new
                {
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Content = table.Column<string>(maxLength: 500, nullable: false),
                    Tenant_Id = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notify", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotifyTarget",
                columns: table => new
                {
                    To_Id = table.Column<string>(nullable: true),
                    HaveRead = table.Column<bool>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Notify_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyTarget", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "f4890afc86cb0cf57721be771e434223", "3d420c79ec0077d03c3f52151b106507" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "EmailAnnex");

            migrationBuilder.DropTable(
                name: "EmailContent");

            migrationBuilder.DropTable(
                name: "EmailTarget");

            migrationBuilder.DropTable(
                name: "FriendMessage");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "GroupManager");

            migrationBuilder.DropTable(
                name: "GroupMessage");

            migrationBuilder.DropTable(
                name: "MessageTarget");

            migrationBuilder.DropTable(
                name: "Notify");

            migrationBuilder.DropTable(
                name: "NotifyTarget");

            migrationBuilder.UpdateData(
                table: "Basis_User",
                keyColumn: "Id",
                keyValue: "00F6P5G2VC2SAP1UJV7HTBYGA",
                columns: new[] { "EncryptionKey", "Password" },
                values: new object[] { "df1aa8afff437e05ba53816a914c12b4", "2a5d53e47b10d00ded06f5889a8af61a" });
        }
    }
}
