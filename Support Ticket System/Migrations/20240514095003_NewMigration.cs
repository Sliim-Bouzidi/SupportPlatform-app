using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attributs",
                columns: table => new
                {
                    AttributID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributs", x => x.AttributID);
                });

            migrationBuilder.CreateTable(
                name: "ProcessFlowAttrributs",
                columns: table => new
                {
                    ProcessFlowID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PFAttributID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessFlowAttrributs", x => new { x.ProcessFlowID, x.AttributID });
                    table.ForeignKey(
                        name: "FK_ProcessFlowAttrributs_attributs_AttributID",
                        column: x => x.AttributID,
                        principalTable: "attributs",
                        principalColumn: "AttributID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessFlowAttrributs_processFlows_ProcessFlowID",
                        column: x => x.ProcessFlowID,
                        principalTable: "processFlows",
                        principalColumn: "ProcessFlowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessFlowAttrributs_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlowAttrributs_AttributID",
                table: "ProcessFlowAttrributs",
                column: "AttributID");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlowAttrributs_TicketID",
                table: "ProcessFlowAttrributs",
                column: "TicketID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessFlowAttrributs");

            migrationBuilder.DropTable(
                name: "attributs");
        }
    }
}
