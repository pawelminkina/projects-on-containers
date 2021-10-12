using Microsoft.EntityFrameworkCore.Migrations;

namespace Issues.Infrastructure.Migrations
{
    public partial class DefaultChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsOfIssues_StatusFlows_StatusFlowId",
                table: "GroupsOfIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TypesOfIssues_TypeOfIssueId",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "IssueContents");

            migrationBuilder.DropIndex(
                name: "IX_GroupsOfIssues_StatusFlowId",
                table: "GroupsOfIssues");

            migrationBuilder.DropColumn(
                name: "StatusFlowId",
                table: "GroupsOfIssues");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "Statuses",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "TypesOfGroupsOfIssues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "StatusFlows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfIssueId",
                table: "Issues",
                type: "nvarchar(63)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Content_IsArchived",
                table: "Issues",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Content_IsDeleted",
                table: "Issues",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content_TextContent",
                table: "Issues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Issues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "GroupsOfIssues",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TypesOfIssues_TypeOfIssueId",
                table: "Issues",
                column: "TypeOfIssueId",
                principalTable: "TypesOfIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TypesOfIssues_TypeOfIssueId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "TypesOfGroupsOfIssues");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "StatusFlows");

            migrationBuilder.DropColumn(
                name: "Content_IsArchived",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Content_IsDeleted",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Content_TextContent",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "GroupsOfIssues");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Statuses",
                newName: "IsArchived");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfIssueId",
                table: "Issues",
                type: "nvarchar(63)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)");

            migrationBuilder.AddColumn<string>(
                name: "StatusFlowId",
                table: "GroupsOfIssues",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "IssueContents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    ParentIssueId = table.Column<string>(type: "nvarchar(63)", nullable: true),
                    TextContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsOfIssues_StatusFlows_StatusFlowId",
                table: "GroupsOfIssues",
                column: "StatusFlowId",
                principalTable: "StatusFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TypesOfIssues_TypeOfIssueId",
                table: "Issues",
                column: "TypeOfIssueId",
                principalTable: "TypesOfIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
