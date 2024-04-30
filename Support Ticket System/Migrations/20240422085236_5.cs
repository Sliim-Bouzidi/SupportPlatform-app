using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewValue",
                table: "statushistory",
                newName: "StatusValue");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tickets");

            migrationBuilder.RenameColumn(
                name: "StatusValue",
                table: "statushistory",
                newName: "NewValue");
        }
    }
}
