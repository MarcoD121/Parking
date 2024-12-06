using ParkingLib.Models;

namespace ParkingRest.Model
{
    public static class ParkedVehicleConverter
    {
        public static ParkedVehicle Convert(ParkedVehicleDTO dto)
        {
            ParkedVehicle vehicle = new ParkedVehicle();
            TimeForParkedVehicle time = new TimeForParkedVehicle();

            vehicle.LicensePlate = dto.licensePlate;
            vehicle.Make = dto.make;
            vehicle.Model = dto.model;
            vehicle.Color = dto.color;
            vehicle.NumberOfWheels = dto.numberOfWheels;
            vehicle.ActiveParked = new TimeForParkedVehicle
            {
                TimeStarted = dto.dateTime
            };

            return vehicle;
        }

    }
}
