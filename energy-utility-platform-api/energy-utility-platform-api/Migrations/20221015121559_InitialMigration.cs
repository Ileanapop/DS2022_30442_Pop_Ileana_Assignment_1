using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace energy_utility_platform_api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnergyDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValue: "Device description"),
                    MaxHourlyEnergy = table.Column<float>(type: "real", nullable: false, defaultValue: 0f)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "client"),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDevices",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnergyDeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevices", x => new { x.UserId, x.EnergyDeviceId });
                    table.ForeignKey(
                        name: "FK_UserDevices_EnergyDevices_EnergyDeviceId",
                        column: x => x.EnergyDeviceId,
                        principalTable: "EnergyDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDevices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyConsumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Consumption = table.Column<float>(type: "real", nullable: false),
                    UserDeviceUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDeviceEnergyDeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyConsumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergyConsumptions_UserDevices_UserDeviceUserId_UserDeviceEnergyDeviceId",
                        columns: x => new { x.UserDeviceUserId, x.UserDeviceEnergyDeviceId },
                        principalTable: "UserDevices",
                        principalColumns: new[] { "UserId", "EnergyDeviceId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnergyConsumptions_UserDeviceUserId_UserDeviceEnergyDeviceId",
                table: "EnergyConsumptions",
                columns: new[] { "UserDeviceUserId", "UserDeviceEnergyDeviceId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_EnergyDeviceId",
                table: "UserDevices",
                column: "EnergyDeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnergyConsumptions");

            migrationBuilder.DropTable(
                name: "UserDevices");

            migrationBuilder.DropTable(
                name: "EnergyDevices");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
