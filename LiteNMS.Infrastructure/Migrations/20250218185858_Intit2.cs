using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteNMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Intit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceMetricProvisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddresses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CredentialProfileIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PollTime = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceMetricProvisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceMetricResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPUUsage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemoryUsage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiskUsage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceMetricResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceMetricProvisions");

            migrationBuilder.DropTable(
                name: "DeviceMetricResults");
        }
    }
}
