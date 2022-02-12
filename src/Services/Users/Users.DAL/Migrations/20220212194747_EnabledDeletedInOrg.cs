using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.DAL.Migrations
{
    public partial class EnabledDeletedInOrg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "dbo",
                table: "Organizations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "dbo",
                table: "Organizations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
