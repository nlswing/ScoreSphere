using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreSphere.Migrations
{
    /// <inheritdoc />
    public partial class isLiveAddedtoMatchTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLive",
                table: "Matches",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLive",
                table: "Matches");
        }
    }
}
