using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyMatch.Database.Migrations
{
    /// <inheritdoc />
    public partial class HobbyNoCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventHobby_Hobbies_RelatedHobbiesId",
                table: "EventHobby");

            migrationBuilder.DropForeignKey(
                name: "FK_HobbyUser_Hobbies_HobbiesId",
                table: "HobbyUser");

            migrationBuilder.AddColumn<int>(
                name: "HobbiesId",
                table: "EventHobby",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EventHobby_HobbiesId",
                table: "EventHobby",
                column: "HobbiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventHobby_Events_HobbiesId",
                table: "EventHobby",
                column: "HobbiesId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventHobby_Hobbies_RelatedHobbiesId",
                table: "EventHobby",
                column: "RelatedHobbiesId",
                principalTable: "Hobbies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HobbyUser_AspNetUsers_HobbiesId",
                table: "HobbyUser",
                column: "HobbiesId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HobbyUser_Hobbies_HobbiesId",
                table: "HobbyUser",
                column: "HobbiesId",
                principalTable: "Hobbies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventHobby_Events_HobbiesId",
                table: "EventHobby");

            migrationBuilder.DropForeignKey(
                name: "FK_EventHobby_Hobbies_RelatedHobbiesId",
                table: "EventHobby");

            migrationBuilder.DropForeignKey(
                name: "FK_HobbyUser_AspNetUsers_HobbiesId",
                table: "HobbyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_HobbyUser_Hobbies_HobbiesId",
                table: "HobbyUser");

            migrationBuilder.DropIndex(
                name: "IX_EventHobby_HobbiesId",
                table: "EventHobby");

            migrationBuilder.DropColumn(
                name: "HobbiesId",
                table: "EventHobby");

            migrationBuilder.AddForeignKey(
                name: "FK_EventHobby_Hobbies_RelatedHobbiesId",
                table: "EventHobby",
                column: "RelatedHobbiesId",
                principalTable: "Hobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HobbyUser_Hobbies_HobbiesId",
                table: "HobbyUser",
                column: "HobbiesId",
                principalTable: "Hobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
