using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreSphere.Migrations
{
    /// <inheritdoc />
    public partial class Notificationsaddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notifications",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notifications",
                table: "Users");
        }
    }
}
