using ParkingLib.Models;

namespace ParkingRest.Model
{
    /// <summary>
    /// A helper class containing logic for converting af DTO to a ParkedVehicle object
    /// </summary>
    public static class ParkedVehicleConverter
    {
        /// <summary>
        /// A static method that converts a DTO object to a parkedvehicle object
        /// </summary>
        /// <param name="dto">ParkedVehicle DTO class</param>
        /// <returns>Parked vehicle object</returns>
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
