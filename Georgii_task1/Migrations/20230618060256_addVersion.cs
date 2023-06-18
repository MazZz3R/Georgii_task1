using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Georgii_task1.Migrations
{
    /// <inheritdoc />
    public partial class addVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppVersions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionString = table.Column<string>(type: "TEXT", nullable: false),
                    AoiUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVersions", x => x.ID);
                });

            migrationBuilder.AddCheckConstraint(
                name: "Age",
                table: "Users",
                sql: "Age > 0 AND Age < 100");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppVersions");

            migrationBuilder.DropCheckConstraint(
                name: "Age",
                table: "Users");
        }
    }
}
