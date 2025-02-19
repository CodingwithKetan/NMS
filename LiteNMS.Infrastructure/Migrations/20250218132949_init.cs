using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteNMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CredentialProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EncryptedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDiscoveryResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CredentialProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscoveryProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastChecked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDiscoveryResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscoveryProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPRanges = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CredentialProfileIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscoveryProfiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProfiles_ProfileName",
                table: "CredentialProfiles",
                column: "ProfileName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CredentialProfiles");

            migrationBuilder.DropTable(
                name: "DeviceDiscoveryResults");

            migrationBuilder.DropTable(
                name: "DiscoveryProfiles");
        }
    }
}
