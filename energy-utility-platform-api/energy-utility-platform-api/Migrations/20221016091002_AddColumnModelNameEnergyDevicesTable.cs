using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace energy_utility_platform_api.Migrations
{
    public partial class AddColumnModelNameEnergyDevicesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModelName",
                table: "EnergyDevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelName",
                table: "EnergyDevices");
        }
    }
}
