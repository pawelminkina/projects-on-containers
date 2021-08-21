using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Issues.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    OrganizationId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusFlows",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    OrganizationId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusFlows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesOfGroupsOfIssues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    OrganizationId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfGroupsOfIssues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesOfIssues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    StatusFlowId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    TypeOfGroupOfIssuesId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Icon = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    OrganizationId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfIssues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupsOfIssues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    OrganizationId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    TypeOfGroupId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    StatusFlowId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsOfIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupsOfIssues_StatusFlows_StatusFlowId",
                        column: x => x.StatusFlowId,
                        principalTable: "StatusFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusesInFlow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    ParentStatusId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    StatusFlowId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    IndexInFlow = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusesInFlow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusesInFlow_Statuses_ParentStatusId",
                        column: x => x.ParentStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatusesInFlow_StatusFlows_StatusFlowId",
                        column: x => x.StatusFlowId,
                        principalTable: "StatusFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    CreatingUserId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    GroupOfIssueId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    TimeOfCreation = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfIssueId = table.Column<string>(type: "nvarchar(63)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_GroupsOfIssues_GroupOfIssueId",
                        column: x => x.GroupOfIssueId,
                        principalTable: "GroupsOfIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_TypesOfIssues_TypeOfIssueId",
                        column: x => x.TypeOfIssueId,
                        principalTable: "TypesOfIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatusInFlowConnection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    ConnectedWithParentId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    ParentStatusId = table.Column<string>(type: "nvarchar(63)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusInFlowConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusInFlowConnection_Statuses_ConnectedWithParentId",
                        column: x => x.ConnectedWithParentId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatusInFlowConnection_StatusesInFlow_ParentStatusId",
                        column: x => x.ParentStatusId,
                        principalTable: "StatusesInFlow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssueContents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    TextContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentIssueId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueContents_Issues_ParentIssueId",
                        column: x => x.ParentIssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupsOfIssues_StatusFlowId",
                table: "GroupsOfIssues",
                column: "StatusFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueContents_ParentIssueId",
                table: "IssueContents",
                column: "ParentIssueId",
                unique: true,
                filter: "[ParentIssueId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_GroupOfIssueId",
                table: "Issues",
                column: "GroupOfIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_TypeOfIssueId",
                table: "Issues",
                column: "TypeOfIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusesInFlow_ParentStatusId",
                table: "StatusesInFlow",
                column: "ParentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusesInFlow_StatusFlowId",
                table: "StatusesInFlow",
                column: "StatusFlowId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueContents");

            migrationBuilder.DropTable(
                name: "StatusInFlowConnection");

            migrationBuilder.DropTable(
                name: "TypesOfGroupsOfIssues");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "StatusesInFlow");

            migrationBuilder.DropTable(
                name: "GroupsOfIssues");

            migrationBuilder.DropTable(
                name: "TypesOfIssues");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "StatusFlows");
        }
    }
}
