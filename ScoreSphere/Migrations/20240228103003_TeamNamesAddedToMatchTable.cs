using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreSphere.Migrations
{
    /// <inheritdoc />
    public partial class TeamNamesAddedToMatchTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Team2ID",
                table: "Matches",
                newName: "Team2Id");

            migrationBuilder.RenameColumn(
                name: "Team1ID",
                table: "Matches",
                newName: "Team1Id");

            migrationBuilder.AddColumn<string>(
                name: "Team1Name",
                table: "Matches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Team2Name",
                table: "Matches",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team1Name",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Team2Name",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Team2Id",
                table: "Matches",
                newName: "Team2ID");

            migrationBuilder.RenameColumn(
                name: "Team1Id",
                table: "Matches",
                newName: "Team1ID");
        }
    }
}
