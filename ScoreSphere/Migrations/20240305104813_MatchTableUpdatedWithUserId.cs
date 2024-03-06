using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreSphere.Migrations
{
    /// <inheritdoc />
    public partial class MatchTableUpdatedWithUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Matches",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Matches");
        }
    }
}
