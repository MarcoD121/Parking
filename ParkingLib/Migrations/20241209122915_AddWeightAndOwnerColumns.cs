using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLib.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightAndOwnerColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeForEndedVehicles",
                columns: table => new
                {
                    EndedParked_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStarted = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeEnded = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TimeForE__D2B9BC5ED11C9883", x => x.EndedParked_Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeForParkedVehicles",
                columns: table => new
                {
                    ActiveParked_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStarted = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TimeForP__9774E10531C9BE82", x => x.ActiveParked_Id);
                });

            migrationBuilder.CreateTable(
                name: "EndedParkedVehicles",
                columns: table => new
                {
                    Vehicle_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndedParked_Id = table.Column<int>(type: "int", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Make = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberOfWheels = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EndedPar__CE6D7C95C597C5C4", x => x.Vehicle_Id);
                    table.ForeignKey(
                        name: "FK__EndedPark__Ended__2FCF1A8A",
                        column: x => x.EndedParked_Id,
                        principalTable: "TimeForEndedVehicles",
                        principalColumn: "EndedParked_Id");
                });

            migrationBuilder.CreateTable(
                name: "ParkedVehicles",
                columns: table => new
                {
                    Vehicle_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveParked_Id = table.Column<int>(type: "int", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Make = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberOfWheels = table.Column<int>(type: "int", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ParkedVe__CE6D7C9556A6FD58", x => x.Vehicle_Id);
                    table.ForeignKey(
                        name: "FK__ParkedVeh__Activ__2CF2ADDF",
                        column: x => x.ActiveParked_Id,
                        principalTable: "TimeForParkedVehicles",
                        principalColumn: "ActiveParked_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndedParkedVehicles_EndedParked_Id",
                table: "EndedParkedVehicles",
                column: "EndedParked_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicles_ActiveParked_Id",
                table: "ParkedVehicles",
                column: "ActiveParked_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndedParkedVehicles");

            migrationBuilder.DropTable(
                name: "ParkedVehicles");

            migrationBuilder.DropTable(
                name: "TimeForEndedVehicles");

            migrationBuilder.DropTable(
                name: "TimeForParkedVehicles");
        }
    }
}
