using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class sdsdada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnections_Statuses_ParentStatusId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropIndex(
                name: "IX_StatusInFlowConnections_ParentStatusId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropColumn(
                name: "ParentStatusId",
                table: "StatusInFlowConnections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentStatusId",
                table: "StatusInFlowConnections",
                type: "nvarchar(63)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnections_ParentStatusId",
                table: "StatusInFlowConnections",
                column: "ParentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnections_Statuses_ParentStatusId",
                table: "StatusInFlowConnections",
                column: "ParentStatusId",
                principalTable: "Statuses",
                principalColumn: "Id");
        }
    }
}
