using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class NewColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsOfIssues_StatusFlows__connectedStatusFlowId",
                table: "GroupsOfIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupsOfIssues_TypesOfGroupsOfIssues__typeOfGroupId",
                table: "GroupsOfIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_GroupsOfIssues__groupOfIssueId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_StatusesInFlow__statusInFlowId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusesInFlow_StatusFlows__statusFlowId",
                table: "StatusesInFlow");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnections_StatusesInFlow__connectedStatusInFlowId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnections_StatusesInFlow__parentStatusInFlowId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropIndex(
                name: "IX_GroupsOfIssues__connectedStatusFlowId",
                table: "GroupsOfIssues");

            migrationBuilder.RenameColumn(
                name: "_parentStatusInFlowId",
                table: "StatusInFlowConnections",
                newName: "ParentStatusInFlowId");

            migrationBuilder.RenameColumn(
                name: "_connectedStatusInFlowId",
                table: "StatusInFlowConnections",
                newName: "ConnectedStatusInFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusInFlowConnections__parentStatusInFlowId",
                table: "StatusInFlowConnections",
                newName: "IX_StatusInFlowConnections_ParentStatusInFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusInFlowConnections__connectedStatusInFlowId",
                table: "StatusInFlowConnections",
                newName: "IX_StatusInFlowConnections_ConnectedStatusInFlowId");

            migrationBuilder.RenameColumn(
                name: "_statusFlowId",
                table: "StatusesInFlow",
                newName: "StatusFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusesInFlow__statusFlowId",
                table: "StatusesInFlow",
                newName: "IX_StatusesInFlow_StatusFlowId");

            migrationBuilder.RenameColumn(
                name: "_statusInFlowId",
                table: "Issues",
                newName: "StatusInFlowId");

            migrationBuilder.RenameColumn(
                name: "_groupOfIssueId",
                table: "Issues",
                newName: "GroupOfIssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues__statusInFlowId",
                table: "Issues",
                newName: "IX_Issues_StatusInFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues__groupOfIssueId",
                table: "Issues",
                newName: "IX_Issues_GroupOfIssueId");

            migrationBuilder.RenameColumn(
                name: "_typeOfGroupId",
                table: "GroupsOfIssues",
                newName: "TypeOfGroupId");

            migrationBuilder.RenameColumn(
                name: "_connectedStatusFlowId",
                table: "GroupsOfIssues",
                newName: "ConnectedStatusFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupsOfIssues__typeOfGroupId",
                table: "GroupsOfIssues",
                newName: "IX_GroupsOfIssues_TypeOfGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsOfIssues_ConnectedStatusFlowId",
                table: "GroupsOfIssues",
                column: "ConnectedStatusFlowId",
                unique: true,
                filter: "[ConnectedStatusFlowId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsOfIssues_StatusFlows_ConnectedStatusFlowId",
                table: "GroupsOfIssues",
                column: "ConnectedStatusFlowId",
                principalTable: "StatusFlows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsOfIssues_TypesOfGroupsOfIssues_TypeOfGroupId",
                table: "GroupsOfIssues",
                column: "TypeOfGroupId",
                principalTable: "TypesOfGroupsOfIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_GroupsOfIssues_GroupOfIssueId",
                table: "Issues",
                column: "GroupOfIssueId",
                principalTable: "GroupsOfIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_StatusesInFlow_StatusInFlowId",
                table: "Issues",
                column: "StatusInFlowId",
                principalTable: "StatusesInFlow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusesInFlow_StatusFlows_StatusFlowId",
                table: "StatusesInFlow",
                column: "StatusFlowId",
                principalTable: "StatusFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnections_StatusesInFlow_ConnectedStatusInFlowId",
                table: "StatusInFlowConnections",
                column: "ConnectedStatusInFlowId",
                principalTable: "StatusesInFlow",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnections_StatusesInFlow_ParentStatusInFlowId",
                table: "StatusInFlowConnections",
                column: "ParentStatusInFlowId",
                principalTable: "StatusesInFlow",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsOfIssues_StatusFlows_ConnectedStatusFlowId",
                table: "GroupsOfIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupsOfIssues_TypesOfGroupsOfIssues_TypeOfGroupId",
                table: "GroupsOfIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_GroupsOfIssues_GroupOfIssueId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_StatusesInFlow_StatusInFlowId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusesInFlow_StatusFlows_StatusFlowId",
                table: "StatusesInFlow");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnections_StatusesInFlow_ConnectedStatusInFlowId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnections_StatusesInFlow_ParentStatusInFlowId",
                table: "StatusInFlowConnections");

            migrationBuilder.DropIndex(
                name: "IX_GroupsOfIssues_ConnectedStatusFlowId",
                table: "GroupsOfIssues");

            migrationBuilder.RenameColumn(
                name: "ParentStatusInFlowId",
                table: "StatusInFlowConnections",
                newName: "_parentStatusInFlowId");

            migrationBuilder.RenameColumn(
                name: "ConnectedStatusInFlowId",
                table: "StatusInFlowConnections",
                newName: "_connectedStatusInFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusInFlowConnections_ParentStatusInFlowId",
                table: "StatusInFlowConnections",
                newName: "IX_StatusInFlowConnections__parentStatusInFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusInFlowConnections_ConnectedStatusInFlowId",
                table: "StatusInFlowConnections",
                newName: "IX_StatusInFlowConnections__connectedStatusInFlowId");

            migrationBuilder.RenameColumn(
                name: "StatusFlowId",
                table: "StatusesInFlow",
                newName: "_statusFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusesInFlow_StatusFlowId",
                table: "StatusesInFlow",
                newName: "IX_StatusesInFlow__statusFlowId");

            migrationBuilder.RenameColumn(
                name: "StatusInFlowId",
                table: "Issues",
                newName: "_statusInFlowId");

            migrationBuilder.RenameColumn(
                name: "GroupOfIssueId",
                table: "Issues",
                newName: "_groupOfIssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_StatusInFlowId",
                table: "Issues",
                newName: "IX_Issues__statusInFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_GroupOfIssueId",
                table: "Issues",
                newName: "IX_Issues__groupOfIssueId");

            migrationBuilder.RenameColumn(
                name: "TypeOfGroupId",
                table: "GroupsOfIssues",
                newName: "_typeOfGroupId");

            migrationBuilder.RenameColumn(
                name: "ConnectedStatusFlowId",
                table: "GroupsOfIssues",
                newName: "_connectedStatusFlowId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupsOfIssues_TypeOfGroupId",
                table: "GroupsOfIssues",
                newName: "IX_GroupsOfIssues__typeOfGroupId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsOfIssues_TypesOfGroupsOfIssues__typeOfGroupId",
                table: "GroupsOfIssues",
                column: "_typeOfGroupId",
                principalTable: "TypesOfGroupsOfIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_GroupsOfIssues__groupOfIssueId",
                table: "Issues",
                column: "_groupOfIssueId",
                principalTable: "GroupsOfIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_StatusesInFlow__statusInFlowId",
                table: "Issues",
                column: "_statusInFlowId",
                principalTable: "StatusesInFlow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusesInFlow_StatusFlows__statusFlowId",
                table: "StatusesInFlow",
                column: "_statusFlowId",
                principalTable: "StatusFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnections_StatusesInFlow__connectedStatusInFlowId",
                table: "StatusInFlowConnections",
                column: "_connectedStatusInFlowId",
                principalTable: "StatusesInFlow",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnections_StatusesInFlow__parentStatusInFlowId",
                table: "StatusInFlowConnections",
                column: "_parentStatusInFlowId",
                principalTable: "StatusesInFlow",
                principalColumn: "Id");
        }
    }
}
