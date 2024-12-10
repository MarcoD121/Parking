using Microsoft.Data.SqlClient;
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

        // Bruges til testing af DB.
        public ParkingRepo(ParkingContext parkingContext)
        {
            _parkingContext = parkingContext;
        }


        public async Task<List<ParkedVehicle>> GetActiveParkings()
        {
            List<ParkedVehicle> list = new List<ParkedVehicle>();
            string query = "SELECT p.Vehicle_Id, p.ActiveParked_Id, p.LicensePlate, p.Make, p.Model, p.Color, p.NumberOfWheels, p.Type, TFPV.TimeStarted FROM ParkedVehicles as p INNER JOIN TimeForParkedVehicles as TFPV ON p.ActiveParked_Id = TFPV.ActiveParked_Id";

            using (SqlConnection connection = new SqlConnection(Secret.ConnectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) 
                    {
                        while (await reader.ReadAsync()) 
                        {
                            ParkedVehicle vehicle = ReadParkedVehicle(reader);
                            list.Add(vehicle);
                        }
                    }
                }
            }

            return list;
        }

        public async Task<List<EndedParkedVehicle>> GetEndedParkings()
        {
            List<EndedParkedVehicle> list = new List<EndedParkedVehicle>();
            string query = "SELECT p.Vehicle_Id, p.EndedParked_Id, p.LicensePlate, p.Make, p.Model, p.Color, p.NumberOfWheels, p.Type, TFEV.TimeStarted, TFEV.TimeEnded, TFEV.TotalTime FROM EndedParkedVehicles as p INNER JOIN TimeForEndedVehicles as TFEV ON p.EndedParked_Id = TFEV.EndedParked_Id";

            using (SqlConnection connection = new SqlConnection(Secret.ConnectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            EndedParkedVehicle vehicle = ReadEndedParkedVehicle(reader);
                            list.Add(vehicle);
                        }
                    }
                }
            }

            return list;
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

            ParkedVehicle? vehicle = await _parkingContext.ParkedVehicles.
                Include(v => v.ActiveParked).
                FirstOrDefaultAsync(v => v.LicensePlate == LicensePlate);

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

        private ParkedVehicle ReadParkedVehicle(SqlDataReader reader)
        {
            ParkedVehicle parkedVehicle = new ParkedVehicle();
            TimeForParkedVehicle parkedTime = new TimeForParkedVehicle();
            parkedVehicle.VehicleId = reader.GetInt32(0);
            parkedVehicle.ActiveParkedId = reader.GetInt32(1);
            parkedVehicle.LicensePlate = reader.GetString(2);
            parkedVehicle.Make = reader.GetString(3);
            parkedVehicle.Model = reader.GetString(4);
            parkedVehicle.Color = reader.GetString(5);
            parkedVehicle.NumberOfWheels = reader.GetInt32(6);
            parkedVehicle.Type = reader.GetString(7);
            parkedTime.TimeStarted = reader.GetDateTime(8);

            return parkedVehicle;
        }

        private EndedParkedVehicle ReadEndedParkedVehicle(SqlDataReader reader)
        {
            EndedParkedVehicle endedParkedVehicle = new EndedParkedVehicle();
            TimeForEndedVehicle parkedTime = new TimeForEndedVehicle();
            endedParkedVehicle.VehicleId = reader.GetInt32(0);
            endedParkedVehicle.EndedParkedId = reader.GetInt32(1);
            endedParkedVehicle.LicensePlate = reader.GetString(2);
            endedParkedVehicle.Make = reader.GetString(3);
            endedParkedVehicle.Model = reader.GetString(4);
            endedParkedVehicle.Color = reader.GetString(5);
            endedParkedVehicle.NumberOfWheels = reader.GetInt32(6);
            endedParkedVehicle.Type = reader.GetString(7);
            parkedTime.TimeStarted = reader.GetDateTime(8);
            parkedTime.TimeEnded = reader.GetDateTime(9);
            parkedTime.TotalTime = reader.GetDateTime(10);

            return endedParkedVehicle;
        }
    }
}
