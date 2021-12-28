using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class SomeMigrationaagff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_GroupsOfIssues_GroupOfIssueId",
                table: "Issues");

            migrationBuilder.AlterColumn<string>(
                name: "GroupOfIssueId",
                table: "Issues",
                type: "nvarchar(63)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_GroupsOfIssues_GroupOfIssueId",
                table: "Issues",
                column: "GroupOfIssueId",
                principalTable: "GroupsOfIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_GroupsOfIssues_GroupOfIssueId",
                table: "Issues");

            migrationBuilder.AlterColumn<string>(
                name: "GroupOfIssueId",
                table: "Issues",
                type: "nvarchar(63)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_GroupsOfIssues_GroupOfIssueId",
                table: "Issues",
                column: "GroupOfIssueId",
                principalTable: "GroupsOfIssues",
                principalColumn: "Id");
        }
    }
}
