﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLib.Migrations
{
    /// <inheritdoc />
    public partial class Alldone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Registration_Number",
                table: "ParkedVehicles");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "EndedParkedVehicles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "EndedParkedVehicles");

            migrationBuilder.AddColumn<string>(
                name: "Registration_Number",
                table: "ParkedVehicles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
