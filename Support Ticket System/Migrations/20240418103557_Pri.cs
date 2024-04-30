using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    public partial class Pri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_priorities_PriorityID",
                table: "tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "PriorityID",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_priorities_PriorityID",
                table: "tickets",
                column: "PriorityID",
                principalTable: "priorities",
                principalColumn: "PriorityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_priorities_PriorityID",
                table: "tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "PriorityID",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_priorities_PriorityID",
                table: "tickets",
                column: "PriorityID",
                principalTable: "priorities",
                principalColumn: "PriorityID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
