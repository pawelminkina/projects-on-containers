using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class REEEEEEEEEE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusFlows",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    OrganizationId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfGroupsOfIssues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusesInFlow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    _statusFlowId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusesInFlow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusesInFlow_StatusFlows__statusFlowId",
                        column: x => x._statusFlowId,
                        principalTable: "StatusFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupsOfIssues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _typeOfGroupId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    _connectedStatusFlowId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TimeOfDeleteUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsOfIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupsOfIssues_StatusFlows__connectedStatusFlowId",
                        column: x => x._connectedStatusFlowId,
                        principalTable: "StatusFlows",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupsOfIssues_TypesOfGroupsOfIssues__typeOfGroupId",
                        column: x => x._typeOfGroupId,
                        principalTable: "TypesOfGroupsOfIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusInFlowConnections",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    _connectedStatusInFlowId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    _parentStatusInFlowId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusInFlowConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusInFlowConnections_StatusesInFlow__connectedStatusInFlowId",
                        column: x => x._connectedStatusInFlowId,
                        principalTable: "StatusesInFlow",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StatusInFlowConnections_StatusesInFlow__parentStatusInFlowId",
                        column: x => x._parentStatusInFlowId,
                        principalTable: "StatusesInFlow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    CreatingUserId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Content_TextContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _groupOfIssueId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    TimeOfCreation = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    _statusInFlowId = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_GroupsOfIssues__groupOfIssueId",
                        column: x => x._groupOfIssueId,
                        principalTable: "GroupsOfIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_StatusesInFlow__statusInFlowId",
                        column: x => x._statusInFlowId,
                        principalTable: "StatusesInFlow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupsOfIssues__connectedStatusFlowId",
                table: "GroupsOfIssues",
                column: "_connectedStatusFlowId",
                unique: true,
                filter: "[_connectedStatusFlowId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsOfIssues__typeOfGroupId",
                table: "GroupsOfIssues",
                column: "_typeOfGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues__groupOfIssueId",
                table: "Issues",
                column: "_groupOfIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues__statusInFlowId",
                table: "Issues",
                column: "_statusInFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusesInFlow__statusFlowId",
                table: "StatusesInFlow",
                column: "_statusFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnections__connectedStatusInFlowId",
                table: "StatusInFlowConnections",
                column: "_connectedStatusInFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusInFlowConnections__parentStatusInFlowId",
                table: "StatusInFlowConnections",
                column: "_parentStatusInFlowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "StatusInFlowConnections");

            migrationBuilder.DropTable(
                name: "GroupsOfIssues");

            migrationBuilder.DropTable(
                name: "StatusesInFlow");

            migrationBuilder.DropTable(
                name: "TypesOfGroupsOfIssues");

            migrationBuilder.DropTable(
                name: "StatusFlows");
        }
    }
}
