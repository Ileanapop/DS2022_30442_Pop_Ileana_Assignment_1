using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace energy_utility_platform_api.Migrations
{
    public partial class ModifyPKUserDeviceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergyConsumptions_UserDevices_UserDeviceUserId_UserDeviceEnergyDeviceId",
                table: "EnergyConsumptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDevices",
                table: "UserDevices");

            migrationBuilder.DropIndex(
                name: "IX_EnergyConsumptions_UserDeviceUserId_UserDeviceEnergyDeviceId",
                table: "EnergyConsumptions");

            migrationBuilder.DropColumn(
                name: "UserDeviceEnergyDeviceId",
                table: "EnergyConsumptions");

            migrationBuilder.RenameColumn(
                name: "UserDeviceUserId",
                table: "EnergyConsumptions",
                newName: "UserDeviceId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserDevices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDevices",
                table: "UserDevices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_UserId",
                table: "UserDevices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyConsumptions_UserDeviceId",
                table: "EnergyConsumptions",
                column: "UserDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergyConsumptions_UserDevices_UserDeviceId",
                table: "EnergyConsumptions",
                column: "UserDeviceId",
                principalTable: "UserDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergyConsumptions_UserDevices_UserDeviceId",
                table: "EnergyConsumptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDevices",
                table: "UserDevices");

            migrationBuilder.DropIndex(
                name: "IX_UserDevices_UserId",
                table: "UserDevices");

            migrationBuilder.DropIndex(
                name: "IX_EnergyConsumptions_UserDeviceId",
                table: "EnergyConsumptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserDevices");

            migrationBuilder.RenameColumn(
                name: "UserDeviceId",
                table: "EnergyConsumptions",
                newName: "UserDeviceUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserDeviceEnergyDeviceId",
                table: "EnergyConsumptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDevices",
                table: "UserDevices",
                columns: new[] { "UserId", "EnergyDeviceId" });

            migrationBuilder.CreateIndex(
                name: "IX_EnergyConsumptions_UserDeviceUserId_UserDeviceEnergyDeviceId",
                table: "EnergyConsumptions",
                columns: new[] { "UserDeviceUserId", "UserDeviceEnergyDeviceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EnergyConsumptions_UserDevices_UserDeviceUserId_UserDeviceEnergyDeviceId",
                table: "EnergyConsumptions",
                columns: new[] { "UserDeviceUserId", "UserDeviceEnergyDeviceId" },
                principalTable: "UserDevices",
                principalColumns: new[] { "UserId", "EnergyDeviceId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
