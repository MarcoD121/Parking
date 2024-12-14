using ParkingLib.Models;

namespace ParkingRest.Model
{
    /// <summary>
    /// A ParkedVehicleDTO record that is used to transfer data between the client and the REST  
    /// </summary>
    /// <param name="licensePlate">License plate of the car</param>
    /// <param name="make">The name of the car manufacture</param>
    /// <param name="model">The name of the car model</param>
    /// <param name="color">Color of the car</param>
    /// <param name="numberOfWheels">Number of wheels on the car</param>
    /// <param name="dateTime">The start time for the parked vehicle</param>
    public record ParkedVehicleDTO(string licensePlate, string make, string model, string color, int numberOfWheels, DateTime dateTime);
}
