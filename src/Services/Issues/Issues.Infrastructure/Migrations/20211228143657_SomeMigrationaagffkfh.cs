using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class SomeMigrationaagffkfh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TypesOfIssues_TypeOfIssueId",
                table: "Issues");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfIssueId",
                table: "Issues",
                type: "nvarchar(63)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TypesOfIssues_TypeOfIssueId",
                table: "Issues",
                column: "TypeOfIssueId",
                principalTable: "TypesOfIssues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TypesOfIssues_TypeOfIssueId",
                table: "Issues");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfIssueId",
                table: "Issues",
                type: "nvarchar(63)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TypesOfIssues_TypeOfIssueId",
                table: "Issues",
                column: "TypeOfIssueId",
                principalTable: "TypesOfIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
