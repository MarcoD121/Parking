using ParkingLib.Models;

namespace ParkingRest.Model
{
    public record ParkedVehicleDTO(string licensePlate, string make, string model, string color, int numberOfWheels, TimeForParkedVehicle activeParked);
}
