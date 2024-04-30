using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    public partial class TicketAdj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_processFlows_ProcessFlowId",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_tenants_TenantID",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_users_UserID",
                table: "tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantID",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProcessFlowId",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_processFlows_ProcessFlowId",
                table: "tickets",
                column: "ProcessFlowId",
                principalTable: "processFlows",
                principalColumn: "ProcessFlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_tenants_TenantID",
                table: "tickets",
                column: "TenantID",
                principalTable: "tenants",
                principalColumn: "TenantID");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_users_UserID",
                table: "tickets",
                column: "UserID",
                principalTable: "users",
                principalColumn: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_processFlows_ProcessFlowId",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_tenants_TenantID",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_users_UserID",
                table: "tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantID",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProcessFlowId",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_processFlows_ProcessFlowId",
                table: "tickets",
                column: "ProcessFlowId",
                principalTable: "processFlows",
                principalColumn: "ProcessFlowId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_tenants_TenantID",
                table: "tickets",
                column: "TenantID",
                principalTable: "tenants",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_users_UserID",
                table: "tickets",
                column: "UserID",
                principalTable: "users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
