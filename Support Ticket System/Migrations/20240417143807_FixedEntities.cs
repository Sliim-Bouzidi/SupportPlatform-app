using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    public partial class FixedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_priorities_tickets_TicketID",
                table: "priorities");

            migrationBuilder.DropForeignKey(
                name: "FK_severities_tickets_TicketID",
                table: "severities");

            migrationBuilder.DropForeignKey(
                name: "FK_statuses_tickets_TicketID",
                table: "statuses");

            migrationBuilder.DropIndex(
                name: "IX_statuses_TicketID",
                table: "statuses");

            migrationBuilder.DropIndex(
                name: "IX_severities_TicketID",
                table: "severities");

            migrationBuilder.DropIndex(
                name: "IX_priorities_TicketID",
                table: "priorities");

            migrationBuilder.DropColumn(
                name: "TicketID",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "TicketID",
                table: "priorities");

            migrationBuilder.AddColumn<Guid>(
                name: "PriorityID",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SeverityID",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Reason",
                columns: table => new
                {
                    ReasonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reason", x => x.ReasonID);
                    table.ForeignKey(
                        name: "FK_Reason_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "statushistory",
                columns: table => new
                {
                    StatusHistoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statushistory", x => x.StatusHistoryID);
                    table.ForeignKey(
                        name: "FK_statushistory_statuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_statushistory_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketHistory",
                columns: table => new
                {
                    TicketHistoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    changeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketHistory", x => x.TicketHistoryID);
                    table.ForeignKey(
                        name: "FK_TicketHistory_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketHistory_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tickets_PriorityID",
                table: "tickets",
                column: "PriorityID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_SeverityID",
                table: "tickets",
                column: "SeverityID");

            migrationBuilder.CreateIndex(
                name: "IX_Reason_TicketID",
                table: "Reason",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_statushistory_StatusID",
                table: "statushistory",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_statushistory_TicketID",
                table: "statushistory",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistory_TicketID",
                table: "TicketHistory",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistory_UserID",
                table: "TicketHistory",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_priorities_PriorityID",
                table: "tickets",
                column: "PriorityID",
                principalTable: "priorities",
                principalColumn: "PriorityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_severities_SeverityID",
                table: "tickets",
                column: "SeverityID",
                principalTable: "severities",
                principalColumn: "SeverityID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_priorities_PriorityID",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_severities_SeverityID",
                table: "tickets");

            migrationBuilder.DropTable(
                name: "Reason");

            migrationBuilder.DropTable(
                name: "statushistory");

            migrationBuilder.DropTable(
                name: "TicketHistory");

            migrationBuilder.DropIndex(
                name: "IX_tickets_PriorityID",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_tickets_SeverityID",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "PriorityID",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "SeverityID",
                table: "tickets");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketID",
                table: "statuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TicketID",
                table: "priorities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_statuses_TicketID",
                table: "statuses",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_severities_TicketID",
                table: "severities",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_priorities_TicketID",
                table: "priorities",
                column: "TicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_priorities_tickets_TicketID",
                table: "priorities",
                column: "TicketID",
                principalTable: "tickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_severities_tickets_TicketID",
                table: "severities",
                column: "TicketID",
                principalTable: "tickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_statuses_tickets_TicketID",
                table: "statuses",
                column: "TicketID",
                principalTable: "tickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
