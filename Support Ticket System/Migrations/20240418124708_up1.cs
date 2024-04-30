using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    public partial class up1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldValue",
                table: "statushistory");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "statushistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "statushistory");

            migrationBuilder.AddColumn<string>(
                name: "OldValue",
                table: "statushistory",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
