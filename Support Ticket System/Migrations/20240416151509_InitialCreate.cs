using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    TagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "tenants",
                columns: table => new
                {
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenants", x => x.TenantID);
                });

            migrationBuilder.CreateTable(
                name: "processFlows",
                columns: table => new
                {
                    ProcessFlowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessFlowName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentProcessFlowId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_processFlows", x => x.ProcessFlowId);
                    table.ForeignKey(
                        name: "FK_processFlows_processFlows_ParentProcessFlowId",
                        column: x => x.ParentProcessFlowId,
                        principalTable: "processFlows",
                        principalColumn: "ProcessFlowId");
                    table.ForeignKey(
                        name: "FK_processFlows_tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_users_tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessFlowId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_tickets_processFlows_ProcessFlowId",
                        column: x => x.ProcessFlowId,
                        principalTable: "processFlows",
                        principalColumn: "ProcessFlowId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tickets_tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tickets_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "priorities",
                columns: table => new
                {
                    PriorityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriorityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priorities", x => x.PriorityID);
                   
                });

            migrationBuilder.CreateTable(
                name: "severities",
                columns: table => new
                {
                    SeverityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeverityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_severities", x => x.SeverityID);
                    table.ForeignKey(
                        name: "FK_severities_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    StatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.StatusID);
                    table.ForeignKey(
                        name: "FK_statuses_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "taggableitems",
                columns: table => new
                {
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taggableitems", x => new { x.TicketID, x.TagID });
                    table.ForeignKey(
                        name: "FK_taggableitems_tags_TagID",
                        column: x => x.TagID,
                        principalTable: "tags",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_taggableitems_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_priorities_TicketID",
                table: "priorities",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_processFlows_ParentProcessFlowId",
                table: "processFlows",
                column: "ParentProcessFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_processFlows_TenantID",
                table: "processFlows",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_severities_TicketID",
                table: "severities",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_statuses_TicketID",
                table: "statuses",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_taggableitems_TagID",
                table: "taggableitems",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_ProcessFlowId",
                table: "tickets",
                column: "ProcessFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_TenantID",
                table: "tickets",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_UserID",
                table: "tickets",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_users_TenantID",
                table: "users",
                column: "TenantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "priorities");

            migrationBuilder.DropTable(
                name: "severities");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "taggableitems");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "processFlows");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "tenants");
        }
    }
}
