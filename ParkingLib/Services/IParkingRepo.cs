using ParkingLib.Models;

namespace ParkingLib.Services
{
    public interface IParkingRepo
    {
        public List<ParkedVehicle> ActiveParkingList { get; set; }

        public ParkedVehicle CreateParking(ParkedVehicle ParkedObject);
        public Task<ParkedVehicle> DeleteParking(string LicensePlate);
        public Task<EndedParkedVehicle> EndParking(string licenseplate);
        public Task<List<ParkedVehicle>> GetActiveParkings();
        public Task<List<ParkedVehicle>> GetAllActiveParkings();
        public Task<List<EndedParkedVehicle>> GetEndedParkings();
        public Task<ParkedVehicle> GetParkingById(string LicensePlate);
    }
}