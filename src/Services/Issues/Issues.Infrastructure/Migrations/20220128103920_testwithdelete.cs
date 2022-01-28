using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class testwithdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusesInFlow_StatusFlows__statusFlowId",
                table: "StatusesInFlow");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusesInFlow_StatusFlows__statusFlowId",
                table: "StatusesInFlow",
                column: "_statusFlowId",
                principalTable: "StatusFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusesInFlow_StatusFlows__statusFlowId",
                table: "StatusesInFlow");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusesInFlow_StatusFlows__statusFlowId",
                table: "StatusesInFlow",
                column: "_statusFlowId",
                principalTable: "StatusFlows",
                principalColumn: "Id");
        }
    }
}
