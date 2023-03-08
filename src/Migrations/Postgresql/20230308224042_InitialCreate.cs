using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace uServiceDemo.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Temperature = table.Column<int>(type: "INTEGER", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "UTC_TIMESTAMP()"),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "UTC_TIMESTAMP()"),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Version = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecast");
        }
    }
}
