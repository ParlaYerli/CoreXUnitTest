using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    lastname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "lastname", "name" },
                values: new object[] { 1, "Yerli", "Azra" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "lastname", "name" },
                values: new object[] { 2, "Yerli", "Hazal" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "lastname", "name" },
                values: new object[] { 3, "Yerli", "Parla" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
