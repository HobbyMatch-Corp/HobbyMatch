using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyMatch.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingVenueBusinessClientRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessClientId",
                table: "Venues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Venues_BusinessClientId",
                table: "Venues",
                column: "BusinessClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_AspNetUsers_BusinessClientId",
                table: "Venues",
                column: "BusinessClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_AspNetUsers_BusinessClientId",
                table: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Venues_BusinessClientId",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "BusinessClientId",
                table: "Venues");
        }
    }
}
