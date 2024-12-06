using ParkingLib.Models;

namespace ParkingRest.Model
{
    public static class ParkedVehicleConverter
    {
        public static ParkedVehicle Convert(ParkedVehicleDTO dto)
        {
            ParkedVehicle vehicle = new ParkedVehicle();

            vehicle.LicensePlate = dto.licensePlate;
            vehicle.Make = dto.make;
            vehicle.Model = dto.model;
            vehicle.Color = dto.color;
            vehicle.NumberOfWheels = dto.numberOfWheels;

            return vehicle;
        }

    }
}
