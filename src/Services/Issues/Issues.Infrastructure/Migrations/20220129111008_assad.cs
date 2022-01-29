using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class assad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_connected StatusFlowId",
                table: "GroupsOfIssues",
                newName: "_connectedStatusFlowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_connectedStatusFlowId",
                table: "GroupsOfIssues",
                newName: "_connected StatusFlowId");
        }
    }
}
