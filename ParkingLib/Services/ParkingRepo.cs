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
    /// <summary>
    /// A class containing CRUD logic for managing parkings 
    /// </summary>
    public class ParkingRepo
    {
        /// <summary>
        /// A list of Parked cars
        /// </summary>
        public List<ParkedVehicle> ActiveParkingList { get; set; }

        /// <summary>
        /// A instance field of the the class containing the connection to the database
        /// </summary>
        private ParkingContext _parkingContext;

        /// <summary>
        /// A default constructor that creates an instance of the class as well as initializing the parking list and db class
        /// </summary>
        public ParkingRepo()
        {
            ActiveParkingList = new List<ParkedVehicle>();
            _parkingContext = new ParkingContext();
        }

        // Bruges til unittesting af DB.
        public ParkingRepo(ParkingContext parkingContext)
        {
            _parkingContext = parkingContext;
        }


        /// <summary>
        /// Gets a list of all the current parked cars from the database using handwritten SQL queries
        /// </summary>
        /// <returns>A list of parked vehicles</returns>
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

        /// <summary>
        /// Gets a list of all the vehicles that is no longer parked from the database
        /// </summary>
        /// <returns>A list of Ended parked vehicles</returns>
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

        /// <summary>
        /// returns a list current parked cars from the database using Entity framework
        /// </summary>
        /// <returns>A list of parked vehicles</returns>
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

        /// <summary>
        /// Finds a current parked car from a given licenseplate and end its parking. The car is moved from the Active parked list to the endedparked list
        /// </summary>
        /// <param name="licenseplate">Unique Id to find a specific car</param>
        /// <returns>The car the whose parking has ended</returns>
        public async Task<EndedParkedVehicle> EndParking(string licenseplate)
        {
            var ParkedObject = await GetParkingById(licenseplate);

            TimeSpan totaltime = DateTime.Now - ParkedObject.ActiveParked.TimeStarted;
            DateTime baseDateTime = new DateTime(2000, 1, 1);
            DateTime totaltimeDateTime = baseDateTime + totaltime;
            EndedParkedVehicle EndedObject = new EndedParkedVehicle
            {
                LicensePlate = ParkedObject.LicensePlate,
                Make = ParkedObject.Make,
                Model = ParkedObject.Model,
                Color = ParkedObject.Color,
                NumberOfWheels = ParkedObject.NumberOfWheels,
                Type = ParkedObject.Type,
                EndedParked = new TimeForEndedVehicle(ParkedObject.ActiveParked.TimeStarted,DateTime.Now, totaltimeDateTime)
            };
            _parkingContext.EndedParkedVehicles.Add(EndedObject);
            _parkingContext.ParkedVehicles.Remove(ParkedObject);
            _parkingContext.SaveChanges();

            return EndedObject;
        }

        /// <summary>
        /// Gets a current parked car from a given licenseplate
        /// </summary>
        /// <param name="LicensePlate">Unique Id to find a specific car</param>
        /// <returns>The found car</returns>
        /// <exception cref="KeyNotFoundException">If no car is found an exception is thrown</exception>
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

        /// <summary>
        /// Deletes the parking of an active parked car
        /// </summary>
        /// <param name="LicensePlate">Unique Id to find a specific car</param>
        /// <returns>The deleted car</returns>
        public async Task<ParkedVehicle> DeleteParking(string LicensePlate)
        {
            ParkedVehicle vehicle = await GetParkingById(LicensePlate);

            _parkingContext.ParkedVehicles.Remove(vehicle);
            _parkingContext.SaveChanges();
            return vehicle;
        }

        /// <summary>
        /// Helping method for reading a parked vehicle from the database
        /// </summary>
        /// <param name="reader">Helper class</param>
        /// <returns>a parked vehicle object with information from the database</returns>
        private ParkedVehicle ReadParkedVehicle(SqlDataReader reader)
        {
            ParkedVehicle parkedVehicle = new ParkedVehicle();
            TimeForParkedVehicle parkedTime = new TimeForParkedVehicle();
            parkedVehicle.ActiveParked = parkedTime;
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

        /// <summary>
        /// Helping method for reading a Endedparked vehicle from the database
        /// </summary>
        /// <param name="reader">Helper class</param>
        /// <returns>an Ended parked vehicle object with information from the database</returns>
        private EndedParkedVehicle ReadEndedParkedVehicle(SqlDataReader reader)
        {
            EndedParkedVehicle endedParkedVehicle = new EndedParkedVehicle();
            TimeForEndedVehicle parkedTime = new TimeForEndedVehicle();
            endedParkedVehicle.EndedParked = parkedTime;
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
