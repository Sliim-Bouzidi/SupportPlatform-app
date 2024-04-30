using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    /// <inheritdoc />
    public partial class _14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_tickets_TicketID",
                table: "TicketHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistory_users_UserID",
                table: "TicketHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketHistory",
                table: "TicketHistory");

            migrationBuilder.RenameTable(
                name: "TicketHistory",
                newName: "ticketHistories");

            migrationBuilder.RenameIndex(
                name: "IX_TicketHistory_UserID",
                table: "ticketHistories",
                newName: "IX_ticketHistories_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_TicketHistory_TicketID",
                table: "ticketHistories",
                newName: "IX_ticketHistories_TicketID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "ticketHistories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ticketHistories",
                table: "ticketHistories",
                column: "TicketHistoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_ticketHistories_tickets_TicketID",
                table: "ticketHistories",
                column: "TicketID",
                principalTable: "tickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticketHistories_users_UserID",
                table: "ticketHistories",
                column: "UserID",
                principalTable: "users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticketHistories_tickets_TicketID",
                table: "ticketHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketHistories_users_UserID",
                table: "ticketHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ticketHistories",
                table: "ticketHistories");

            migrationBuilder.RenameTable(
                name: "ticketHistories",
                newName: "TicketHistory");

            migrationBuilder.RenameIndex(
                name: "IX_ticketHistories_UserID",
                table: "TicketHistory",
                newName: "IX_TicketHistory_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ticketHistories_TicketID",
                table: "TicketHistory",
                newName: "IX_TicketHistory_TicketID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "TicketHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketHistory",
                table: "TicketHistory",
                column: "TicketHistoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistory_tickets_TicketID",
                table: "TicketHistory",
                column: "TicketID",
                principalTable: "tickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistory_users_UserID",
                table: "TicketHistory",
                column: "UserID",
                principalTable: "users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
