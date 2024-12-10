﻿using Microsoft.Data.SqlClient;
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


        public async Task<List<ParkedVehicle>> GetSqlList()
        {
            List<ParkedVehicle> list = new List<ParkedVehicle>();
            string query = "SELECT p.Vehicle_Id, p.ActiveParked_Id, p.LicensePlate, p.Make, p.Model, p.Color, p.NumberOfWheels, TFPV.TimeStarted FROM ParkedVehicles as p INNER JOIN TimeForParkedVehicles as TFPV ON p.ActiveParked_Id = TFPV.ActiveParked_Id";

            using (SqlConnection connection = new SqlConnection(Secret.ConnectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) 
                    {
                        while (await reader.ReadAsync()) 
                        {
                            ParkedVehicle parkedVehicle = ReadObject(reader);
                            list.Add(parkedVehicle);
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

        private ParkedVehicle ReadObject(SqlDataReader reader)
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
            parkedTime.TimeStarted = reader.GetDateTime(7);

            return parkedVehicle;

        }
    }
}
