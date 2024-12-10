using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLib.Migrations
{
    /// <inheritdoc />
    public partial class ItWorksNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "ParkedVehicles",
                newName: "Registration_Number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Registration_Number",
                table: "ParkedVehicles",
                newName: "RegistrationNumber");
        }
    }
}
