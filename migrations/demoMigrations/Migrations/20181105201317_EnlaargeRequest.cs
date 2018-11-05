using Microsoft.EntityFrameworkCore.Migrations;

namespace demoWebApp.Migrations
{
    public partial class EnlaargeRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestPath",
                table: "Metrics",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Query",
                table: "Metrics",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestPath",
                table: "Metrics",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Query",
                table: "Metrics",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
