using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyMatch.Database.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenEndsAtUtc",
                table: "AspNetUsers",
                newName: "RefreshTokenExpiresAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiresAt",
                table: "AspNetUsers",
                newName: "RefreshTokenEndsAtUtc");
        }
    }
}
