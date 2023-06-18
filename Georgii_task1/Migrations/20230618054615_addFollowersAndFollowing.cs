using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Georgii_task1.Migrations
{
    /// <inheritdoc />
    public partial class addFollowersAndFollowing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    followersId = table.Column<int>(type: "INTEGER", nullable: false),
                    followingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.followersId, x.followingId });
                    table.ForeignKey(
                        name: "FK_UserUser_Users_followersId",
                        column: x => x.followersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_Users_followingId",
                        column: x => x.followingId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_followingId",
                table: "UserUser",
                column: "followingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUser");
        }
    }
}
