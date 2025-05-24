using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyMatch.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventHobby_Hobbies_RelatedHobbiesId",
                table: "EventHobby");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventHobby",
                table: "EventHobby");

            migrationBuilder.DropIndex(
                name: "IX_EventHobby_RelatedHobbiesId",
                table: "EventHobby");

            migrationBuilder.DropColumn(
                name: "RelatedHobbiesId",
                table: "EventHobby");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventHobby",
                table: "EventHobby",
                columns: new[] { "EventsId", "HobbiesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventHobby_Hobbies_HobbiesId",
                table: "EventHobby",
                column: "HobbiesId",
                principalTable: "Hobbies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventHobby_Hobbies_HobbiesId",
                table: "EventHobby");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventHobby",
                table: "EventHobby");

            migrationBuilder.AddColumn<int>(
                name: "RelatedHobbiesId",
                table: "EventHobby",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventHobby",
                table: "EventHobby",
                columns: new[] { "EventsId", "RelatedHobbiesId" });

            migrationBuilder.CreateIndex(
                name: "IX_EventHobby_RelatedHobbiesId",
                table: "EventHobby",
                column: "RelatedHobbiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventHobby_Hobbies_RelatedHobbiesId",
                table: "EventHobby",
                column: "RelatedHobbiesId",
                principalTable: "Hobbies",
                principalColumn: "Id");
        }
    }
}
