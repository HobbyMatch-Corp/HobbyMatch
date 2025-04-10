using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyMatch.Database.Migrations
{
    /// <inheritdoc />
    public partial class NoCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessClientEvent_AspNetUsers_SponsorsPartnersId",
                table: "BusinessClientEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessClientEvent_Events_SponsoredEventsId",
                table: "BusinessClientEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_AspNetUsers_SignUpListId",
                table: "EventUser");

            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_Events_SignedUpEventsId",
                table: "EventUser");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessClientEvent_AspNetUsers_SponsorsPartnersId",
                table: "BusinessClientEvent",
                column: "SponsorsPartnersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessClientEvent_Events_SponsoredEventsId",
                table: "BusinessClientEvent",
                column: "SponsoredEventsId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_AspNetUsers_SignUpListId",
                table: "EventUser",
                column: "SignUpListId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_Events_SignedUpEventsId",
                table: "EventUser",
                column: "SignedUpEventsId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessClientEvent_AspNetUsers_SponsorsPartnersId",
                table: "BusinessClientEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessClientEvent_Events_SponsoredEventsId",
                table: "BusinessClientEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_AspNetUsers_SignUpListId",
                table: "EventUser");

            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_Events_SignedUpEventsId",
                table: "EventUser");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessClientEvent_AspNetUsers_SponsorsPartnersId",
                table: "BusinessClientEvent",
                column: "SponsorsPartnersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessClientEvent_Events_SponsoredEventsId",
                table: "BusinessClientEvent",
                column: "SponsoredEventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_AspNetUsers_SignUpListId",
                table: "EventUser",
                column: "SignUpListId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_Events_SignedUpEventsId",
                table: "EventUser",
                column: "SignedUpEventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
