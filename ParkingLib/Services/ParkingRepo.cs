using Microsoft.EntityFrameworkCore;
using ParkingLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLib.Services
{
    public class ParkingRepo
    {
        public List<ParkedVehicle> ActiveParkingList { get; set; }
        private ParkingContext _parkingContext;

        public ParkingRepo()
        {
            ActiveParkingList = new List<ParkedVehicle>();
            _parkingContext = new ParkingContext();
        }

        public async Task<List<ParkedVehicle>> GetAllActiveParkings()
        {
            return await _parkingContext.ParkedVehicles.ToListAsync();
        }
        public ParkedVehicle CreateParking(ParkedVehicle ParkedObject)
        {
            _parkingContext.ParkedVehicles.Add(ParkedObject);
            _parkingContext.SaveChanges();
            return ParkedObject;
        }

        public async Task<ParkedVehicle> GetParkingById(string LicensePlate)
        {

            ParkedVehicle? vehicle = await _parkingContext.ParkedVehicles.FirstOrDefaultAsync(v => v.LicensePlate == LicensePlate);

            if (vehicle == null)
            {
                throw new KeyNotFoundException("License plate was not found");
            }

            return vehicle;
        }
        public async Task<ParkedVehicle> DeleteParking(string LicensePlate)
        {
            ParkedVehicle vehicle = await GetParkingById(LicensePlate);
            _parkingContext.ParkedVehicles.Remove(vehicle);
            _parkingContext.SaveChanges();
            return vehicle;
        }
    }
}
