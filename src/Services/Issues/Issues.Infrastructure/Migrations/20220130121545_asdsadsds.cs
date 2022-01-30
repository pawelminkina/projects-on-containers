using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issues.Infrastructure.Migrations
{
    public partial class asdsadsds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direction",
                table: "StatusInFlowConnections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Direction",
                table: "StatusInFlowConnections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
