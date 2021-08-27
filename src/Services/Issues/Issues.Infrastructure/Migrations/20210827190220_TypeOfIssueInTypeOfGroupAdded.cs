using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Issues.Infrastructure.Migrations
{
    public partial class TypeOfIssueInTypeOfGroupAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "TypesOfIssues");

            migrationBuilder.DropColumn(
                name: "StatusFlowId",
                table: "TypesOfIssues");

            migrationBuilder.DropColumn(
                name: "TypeOfGroupOfIssuesId",
                table: "TypesOfIssues");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "GroupsOfIssues");

            migrationBuilder.CreateTable(
                name: "TypesOfIssueInTypeOfGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    StatusFlowId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    TypeOfGroupOfIssuesId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfIssueInTypeOfGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypesOfIssueInTypeOfGroups_StatusFlows_StatusFlowId",
                        column: x => x.StatusFlowId,
                        principalTable: "StatusFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypesOfIssueInTypeOfGroups_TypesOfGroupsOfIssues_TypeOfGroupOfIssuesId",
                        column: x => x.TypeOfGroupOfIssuesId,
                        principalTable: "TypesOfGroupsOfIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypesOfIssueInTypeOfGroups_TypesOfIssues_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TypesOfIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupsOfIssues_TypeOfGroupId",
                table: "GroupsOfIssues",
                column: "TypeOfGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesOfIssueInTypeOfGroups_ParentId",
                table: "TypesOfIssueInTypeOfGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesOfIssueInTypeOfGroups_StatusFlowId",
                table: "TypesOfIssueInTypeOfGroups",
                column: "StatusFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesOfIssueInTypeOfGroups_TypeOfGroupOfIssuesId",
                table: "TypesOfIssueInTypeOfGroups",
                column: "TypeOfGroupOfIssuesId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsOfIssues_TypesOfGroupsOfIssues_TypeOfGroupId",
                table: "GroupsOfIssues",
                column: "TypeOfGroupId",
                principalTable: "TypesOfGroupsOfIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsOfIssues_TypesOfGroupsOfIssues_TypeOfGroupId",
                table: "GroupsOfIssues");

            migrationBuilder.DropTable(
                name: "TypesOfIssueInTypeOfGroups");

            migrationBuilder.DropIndex(
                name: "IX_GroupsOfIssues_TypeOfGroupId",
                table: "GroupsOfIssues");

            migrationBuilder.AddColumn<byte[]>(
                name: "Icon",
                table: "TypesOfIssues",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "StatusFlowId",
                table: "TypesOfIssues",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfGroupOfIssuesId",
                table: "TypesOfIssues",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrganizationId",
                table: "GroupsOfIssues",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                defaultValue: "");
        }
    }
}
