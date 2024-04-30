using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    /// <inheritdoc />
    public partial class _17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reasons",
                columns: table => new
                {
                    ReasonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reasons", x => x.ReasonID);
                    table.ForeignKey(
                        name: "FK_reasons_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reasons_TicketID",
                table: "reasons",
                column: "TicketID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reasons");
        }
    }
}
