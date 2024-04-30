using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    public partial class FixedEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_severities_SeverityID",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_tickets_SeverityID",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "SeverityID",
                table: "tickets");

            migrationBuilder.CreateIndex(
                name: "IX_severities_TicketID",
                table: "severities",
                column: "TicketID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_severities_tickets_TicketID",
                table: "severities",
                column: "TicketID",
                principalTable: "tickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_severities_tickets_TicketID",
                table: "severities");

            migrationBuilder.DropIndex(
                name: "IX_severities_TicketID",
                table: "severities");

            migrationBuilder.AddColumn<Guid>(
                name: "SeverityID",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tickets_SeverityID",
                table: "tickets",
                column: "SeverityID");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_severities_SeverityID",
                table: "tickets",
                column: "SeverityID",
                principalTable: "severities",
                principalColumn: "SeverityID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
