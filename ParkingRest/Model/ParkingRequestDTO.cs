namespace ParkingRest.Model
{
    /// <summary>
    /// A requstDTO record that is used to tranfer data between a proxy server and the REST
    /// </summary>
    /// <param name="LicensePlate">Unique ID found through the Raspberry pie</param>
    public record ParkingRequestDTO(string LicensePlate)
    {
    }
}
