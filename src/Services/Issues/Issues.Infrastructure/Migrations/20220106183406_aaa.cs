using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class aaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnection_Statuses_ConnectedWithParentId",
                table: "StatusInFlowConnection");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusInFlowConnection_StatusesInFlow_ParentStatusId",
                table: "StatusInFlowConnection");

            migrationBuilder.DropIndex(
                name: "IX_StatusInFlowConnection_ConnectedWithParentId",
                table: "StatusInFlowConnection");

            migrationBuilder.DropIndex(
                name: "IX_StatusInFlowConnection_ParentStatusId",
                table: "StatusInFlowConnection");

            migrationBuilder.RenameColumn(
                name: "ConnectedWithParentId",
                table: "StatusInFlowConnection",
                newName: "ConnectedStatusId");

            migrationBuilder.AddColumn<int>(
                name: "Direction",
                table: "StatusInFlowConnection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParentStatusInFlowId",
                table: "StatusInFlowConnection",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnection_ConnectedStatusId",
                table: "StatusInFlowConnection",
                column: "ConnectedStatusId",
                unique: true,
                filter: "[ConnectedStatusId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnection_ParentStatusId",
                table: "StatusInFlowConnection",
                column: "ParentStatusId",
                unique: true,
                filter: "[ParentStatusId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnection_ParentStatusInFlowId",
                table: "StatusInFlowConnection",
                column: "ParentStatusInFlowId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_StatusInFlowConnection_ConnectedStatusId",
                table: "StatusInFlowConnection");

            migrationBuilder.DropIndex(
                name: "IX_StatusInFlowConnection_ParentStatusId",
                table: "StatusInFlowConnection");

            migrationBuilder.DropIndex(
                name: "IX_StatusInFlowConnection_ParentStatusInFlowId",
                table: "StatusInFlowConnection");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "StatusInFlowConnection");

            migrationBuilder.DropColumn(
                name: "ParentStatusInFlowId",
                table: "StatusInFlowConnection");

            migrationBuilder.RenameColumn(
                name: "ConnectedStatusId",
                table: "StatusInFlowConnection",
                newName: "ConnectedWithParentId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnection_ConnectedWithParentId",
                table: "StatusInFlowConnection",
                column: "ConnectedWithParentId",
                unique: true,
                filter: "[ConnectedWithParentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnection_ParentStatusId",
                table: "StatusInFlowConnection",
                column: "ParentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnection_Statuses_ConnectedWithParentId",
                table: "StatusInFlowConnection",
                column: "ConnectedWithParentId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusInFlowConnection_StatusesInFlow_ParentStatusId",
                table: "StatusInFlowConnection",
                column: "ParentStatusId",
                principalTable: "StatusesInFlow",
                principalColumn: "Id");
        }
    }
}
