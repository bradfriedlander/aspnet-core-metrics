using Microsoft.EntityFrameworkCore.Migrations;

namespace demoWebApp.Migrations
{
    public partial class AddRequestMethod : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestMethod",
                table: "Metrics");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestMethod",
                table: "Metrics",
                nullable: true);
        }
    }
}
