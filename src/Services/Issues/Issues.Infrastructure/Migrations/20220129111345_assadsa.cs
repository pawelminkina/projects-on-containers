using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class assadsa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusFlows_GroupsOfIssues__connectedStatusFlowId",
                table: "StatusFlows");

            migrationBuilder.DropIndex(
                name: "IX_StatusFlows__connectedStatusFlowId",
                table: "StatusFlows");

            migrationBuilder.DropColumn(
                name: "_connectedStatusFlowId",
                table: "StatusFlows");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsOfIssues__connectedStatusFlowId",
                table: "GroupsOfIssues",
                column: "_connectedStatusFlowId",
                unique: true,
                filter: "[_connectedStatusFlowId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsOfIssues_StatusFlows__connectedStatusFlowId",
                table: "GroupsOfIssues",
                column: "_connectedStatusFlowId",
                principalTable: "StatusFlows",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsOfIssues_StatusFlows__connectedStatusFlowId",
                table: "GroupsOfIssues");

            migrationBuilder.DropIndex(
                name: "IX_GroupsOfIssues__connectedStatusFlowId",
                table: "GroupsOfIssues");

            migrationBuilder.AddColumn<string>(
                name: "_connectedStatusFlowId",
                table: "StatusFlows",
                type: "nvarchar(63)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusFlows__connectedStatusFlowId",
                table: "StatusFlows",
                column: "_connectedStatusFlowId",
                unique: true,
                filter: "[_connectedStatusFlowId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusFlows_GroupsOfIssues__connectedStatusFlowId",
                table: "StatusFlows",
                column: "_connectedStatusFlowId",
                principalTable: "GroupsOfIssues",
                principalColumn: "Id");
        }
    }
}
