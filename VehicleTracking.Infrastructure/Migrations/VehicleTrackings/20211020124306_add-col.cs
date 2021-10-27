using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.Infrastructure.Migrations.VehicleTrackings
{
    public partial class addcol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "VehicleProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DriverInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "VehicleProfile");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DriverInfo");
        }
    }
}
