using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "priorities",
                columns: table => new
                {
                    PriorityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriorityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priorities", x => x.PriorityID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "severities",
                columns: table => new
                {
                    SeverityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeverityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_severities", x => x.SeverityID);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    StatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.StatusID);
                });

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
                name: "TicketType",
                columns: table => new
                {
                    TicketTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.TicketTypeID);
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
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Passwordsalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_users_tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "tenants",
                        principalColumn: "TenantID");
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriorityID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SeverityID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TicketTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessFlowId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_tickets_TicketType_TicketTypeID",
                        column: x => x.TicketTypeID,
                        principalTable: "TicketType",
                        principalColumn: "TicketTypeID");
                    table.ForeignKey(
                        name: "FK_tickets_priorities_PriorityID",
                        column: x => x.PriorityID,
                        principalTable: "priorities",
                        principalColumn: "PriorityID");
                    table.ForeignKey(
                        name: "FK_tickets_processFlows_ProcessFlowId",
                        column: x => x.ProcessFlowId,
                        principalTable: "processFlows",
                        principalColumn: "ProcessFlowId");
                    table.ForeignKey(
                        name: "FK_tickets_severities_SeverityID",
                        column: x => x.SeverityID,
                        principalTable: "severities",
                        principalColumn: "SeverityID");
                    table.ForeignKey(
                        name: "FK_tickets_tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "tenants",
                        principalColumn: "TenantID");
                    table.ForeignKey(
                        name: "FK_tickets_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRolesID);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attachments_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    CommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_comments_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "statushistory",
                columns: table => new
                {
                    StatusHistoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "TicketCategory",
                columns: table => new
                {
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCategory", x => new { x.TicketID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_TicketCategory_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketCategory_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticketHistories",
                columns: table => new
                {
                    TicketHistoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    changeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticketHistories", x => x.TicketHistoryID);
                    table.ForeignKey(
                        name: "FK_ticketHistories_tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticketHistories_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_attachments_TicketID",
                table: "attachments",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_comments_TicketID",
                table: "comments",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_comments_UserID",
                table: "comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_processFlows_ParentProcessFlowId",
                table: "processFlows",
                column: "ParentProcessFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_processFlows_TenantID",
                table: "processFlows",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_reasons_TicketID",
                table: "reasons",
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
                name: "IX_taggableitems_TagID",
                table: "taggableitems",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketCategory_CategoryID",
                table: "TicketCategory",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ticketHistories_TicketID",
                table: "ticketHistories",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_ticketHistories_UserID",
                table: "ticketHistories",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_PriorityID",
                table: "tickets",
                column: "PriorityID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_ProcessFlowId",
                table: "tickets",
                column: "ProcessFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_SeverityID",
                table: "tickets",
                column: "SeverityID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_TenantID",
                table: "tickets",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_TicketTypeID",
                table: "tickets",
                column: "TicketTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_UserID",
                table: "tickets",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleID",
                table: "UserRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserID",
                table: "UserRoles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_users_TenantID",
                table: "users",
                column: "TenantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "reasons");

            migrationBuilder.DropTable(
                name: "statushistory");

            migrationBuilder.DropTable(
                name: "taggableitems");

            migrationBuilder.DropTable(
                name: "TicketCategory");

            migrationBuilder.DropTable(
                name: "ticketHistories");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TicketType");

            migrationBuilder.DropTable(
                name: "priorities");

            migrationBuilder.DropTable(
                name: "processFlows");

            migrationBuilder.DropTable(
                name: "severities");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "tenants");
        }
    }
}
