using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace azureCommsHubPlayground.persistance.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "azureBusMessageEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    enqueued = table.Column<DateTime>(type: "datetime2", nullable: true),
                    processed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    processorGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payLoad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_azureBusMessageEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ipAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    countryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    regionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lattitude = table.Column<double>(type: "float", nullable: true),
                    longitude = table.Column<double>(type: "float", nullable: true),
                    timezone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    org = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    autonsys = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ipAddress", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "azureBusMessageEntry");

            migrationBuilder.DropTable(
                name: "ipAddress");
        }
    }
}
