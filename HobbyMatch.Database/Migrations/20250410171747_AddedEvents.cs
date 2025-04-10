using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyMatch.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    MaxUsers = table.Column<int>(type: "int", nullable: false),
                    MinUsers = table.Column<int>(type: "int", nullable: false),
                    Location_Latitude = table.Column<double>(type: "float", nullable: false),
                    Location_Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BusinessClientEvent",
                columns: table => new
                {
                    SponsoredEventsId = table.Column<int>(type: "int", nullable: false),
                    SponsorsPartnersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessClientEvent", x => new { x.SponsoredEventsId, x.SponsorsPartnersId });
                    table.ForeignKey(
                        name: "FK_BusinessClientEvent_AspNetUsers_SponsorsPartnersId",
                        column: x => x.SponsorsPartnersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BusinessClientEvent_Events_SponsoredEventsId",
                        column: x => x.SponsoredEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "EventUser",
                columns: table => new
                {
                    SignUpListId = table.Column<int>(type: "int", nullable: false),
                    SignedUpEventsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => new { x.SignUpListId, x.SignedUpEventsId });
                    table.ForeignKey(
                        name: "FK_EventUser_AspNetUsers_SignUpListId",
                        column: x => x.SignUpListId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EventUser_Events_SignedUpEventsId",
                        column: x => x.SignedUpEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessClientEvent_SponsorsPartnersId",
                table: "BusinessClientEvent",
                column: "SponsorsPartnersId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_SignedUpEventsId",
                table: "EventUser",
                column: "SignedUpEventsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessClientEvent");

            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
