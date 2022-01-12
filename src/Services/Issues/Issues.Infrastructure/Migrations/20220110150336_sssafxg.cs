using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class sssafxg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatusInFlowConnections_ConnectedStatusId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropIndex(
                name: "IX_StatusInFlowConnections_ParentStatusId",
                table: "StatusInFlowConnections");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnections_ConnectedStatusId",
                table: "StatusInFlowConnections",
                column: "ConnectedStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnections_ParentStatusId",
                table: "StatusInFlowConnections",
                column: "ParentStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatusInFlowConnections_ConnectedStatusId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropIndex(
                name: "IX_StatusInFlowConnections_ParentStatusId",
                table: "StatusInFlowConnections");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnections_ConnectedStatusId",
                table: "StatusInFlowConnections",
                column: "ConnectedStatusId",
                unique: true,
                filter: "[ConnectedStatusId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnections_ParentStatusId",
                table: "StatusInFlowConnections",
                column: "ParentStatusId",
                unique: true,
                filter: "[ParentStatusId] IS NOT NULL");
        }
    }
}
