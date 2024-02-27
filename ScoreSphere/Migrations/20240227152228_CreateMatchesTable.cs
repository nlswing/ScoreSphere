using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ScoreSphere.Migrations
{
    /// <inheritdoc />
    public partial class CreateMatchesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Team1ID = table.Column<int>(type: "integer", nullable: true),
                    Team2ID = table.Column<int>(type: "integer", nullable: true),
                    Team1Goals = table.Column<int>(type: "integer", nullable: true),
                    Team2Goals = table.Column<int>(type: "integer", nullable: true),
                    Team1Logo = table.Column<string>(type: "text", nullable: true),
                    Team2Logo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
