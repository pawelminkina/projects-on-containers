using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class sssa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnection_Statuses_ConnectedStatusId",
                table: "StatusInFlowConnection");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnection_Statuses_ParentStatusId",
                table: "StatusInFlowConnection");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnection_StatusesInFlow_ParentStatusInFlowId",
                table: "StatusInFlowConnection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusInFlowConnection",
                table: "StatusInFlowConnection");

            migrationBuilder.RenameTable(
                name: "StatusInFlowConnection",
                newName: "StatusInFlowConnections");

            migrationBuilder.RenameIndex(
                name: "IX_StatusInFlowConnection_ParentStatusInFlowId",
                table: "StatusInFlowConnections",
                newName: "IX_StatusInFlowConnections_ParentStatusInFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusInFlowConnection_ParentStatusId",
                table: "StatusInFlowConnections",
                newName: "IX_StatusInFlowConnections_ParentStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusInFlowConnection_ConnectedStatusId",
                table: "StatusInFlowConnections",
                newName: "IX_StatusInFlowConnections_ConnectedStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusInFlowConnections",
                table: "StatusInFlowConnections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnections_Statuses_ConnectedStatusId",
                table: "StatusInFlowConnections",
                column: "ConnectedStatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnections_Statuses_ParentStatusId",
                table: "StatusInFlowConnections",
                column: "ParentStatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnections_StatusesInFlow_ParentStatusInFlowId",
                table: "StatusInFlowConnections",
                column: "ParentStatusInFlowId",
                principalTable: "StatusesInFlow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnections_Statuses_ConnectedStatusId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnections_Statuses_ParentStatusId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnections_StatusesInFlow_ParentStatusInFlowId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusInFlowConnections",
                table: "StatusInFlowConnections");

            migrationBuilder.RenameTable(
                name: "StatusInFlowConnections",
                newName: "StatusInFlowConnection");

            migrationBuilder.RenameIndex(
                name: "IX_StatusInFlowConnections_ParentStatusInFlowId",
                table: "StatusInFlowConnection",
                newName: "IX_StatusInFlowConnection_ParentStatusInFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusInFlowConnections_ParentStatusId",
                table: "StatusInFlowConnection",
                newName: "IX_StatusInFlowConnection_ParentStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusInFlowConnections_ConnectedStatusId",
                table: "StatusInFlowConnection",
                newName: "IX_StatusInFlowConnection_ConnectedStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusInFlowConnection",
                table: "StatusInFlowConnection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnection_Statuses_ConnectedStatusId",
                table: "StatusInFlowConnection",
                column: "ConnectedStatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnection_Statuses_ParentStatusId",
                table: "StatusInFlowConnection",
                column: "ParentStatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnection_StatusesInFlow_ParentStatusInFlowId",
                table: "StatusInFlowConnection",
                column: "ParentStatusInFlowId",
                principalTable: "StatusesInFlow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
