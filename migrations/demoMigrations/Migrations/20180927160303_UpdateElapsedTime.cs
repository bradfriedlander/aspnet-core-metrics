using Microsoft.EntityFrameworkCore.Migrations;

namespace demoWebApp.Migrations
{
    public partial class UpdateElapsedTime : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ElapsedTime",
                table: "Metrics",
                newName: "ElpasedTime");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ElpasedTime",
                table: "Metrics",
                newName: "ElapsedTime");
        }
    }
}
