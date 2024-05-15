using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    /// <inheritdoc />
    public partial class verynewnewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessFlowAttrributs_processFlows_ProcessFlowID",
                table: "ProcessFlowAttrributs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessFlowAttrributs",
                table: "ProcessFlowAttrributs");

            migrationBuilder.RenameColumn(
                name: "ProcessFlowID",
                table: "ProcessFlowAttrributs",
                newName: "ProcessFlowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessFlowAttrributs",
                table: "ProcessFlowAttrributs",
                column: "PFAttributID");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlowAttrributs_ProcessFlowId",
                table: "ProcessFlowAttrributs",
                column: "ProcessFlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessFlowAttrributs_processFlows_ProcessFlowId",
                table: "ProcessFlowAttrributs",
                column: "ProcessFlowId",
                principalTable: "processFlows",
                principalColumn: "ProcessFlowId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessFlowAttrributs_processFlows_ProcessFlowId",
                table: "ProcessFlowAttrributs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessFlowAttrributs",
                table: "ProcessFlowAttrributs");

            migrationBuilder.DropIndex(
                name: "IX_ProcessFlowAttrributs_ProcessFlowId",
                table: "ProcessFlowAttrributs");

            migrationBuilder.RenameColumn(
                name: "ProcessFlowId",
                table: "ProcessFlowAttrributs",
                newName: "ProcessFlowID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessFlowAttrributs",
                table: "ProcessFlowAttrributs",
                columns: new[] { "ProcessFlowID", "AttributID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessFlowAttrributs_processFlows_ProcessFlowID",
                table: "ProcessFlowAttrributs",
                column: "ProcessFlowID",
                principalTable: "processFlows",
                principalColumn: "ProcessFlowId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
