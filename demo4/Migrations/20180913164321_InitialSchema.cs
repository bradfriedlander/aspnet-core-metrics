using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace demo4.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    Application = table.Column<string>(maxLength: 64, nullable: false),
                    Details = table.Column<string>(nullable: true),
                    ElpasedTime = table.Column<int>(nullable: false),
                    ExceptionMessage = table.Column<string>(nullable: true),
                    MetricId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Query = table.Column<string>(maxLength: 128, nullable: false),
                    RequestPath = table.Column<string>(maxLength: 128, nullable: false),
                    ResultCode = table.Column<int>(nullable: false),
                    ResultCount = table.Column<int>(nullable: false),
                    ServerName = table.Column<string>(maxLength: 64, nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    TraceId = table.Column<string>(maxLength: 64, nullable: false),
                    UserName = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.MetricId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metrics");
        }
    }
}
