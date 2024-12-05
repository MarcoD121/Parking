using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingLib.Models;
using ParkingLib.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLib.Services.Tests
{
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class ParkingRepoTests
    {

        private ParkingContext GetInSqlServerContext()
        {
            var options = new DbContextOptionsBuilder<ParkingContext>().UseSqlServer("Data Source=mssql2.unoeuro.com;Initial Catalog=abdiabbas_dk_db_databasetest;User ID=abdiabbas_dk;Password=F92enzdcEHRkyt5pbaAD;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False").Options;

            return new ParkingContext(options);
        }

        [TestMethod]
        public void CreateParkingTest_Is_Valid()
        {
            var context = GetInSqlServerContext();
            var repo = new ParkingRepo(context);

            var time = new TimeForParkedVehicle(DateTime.Now);
            var vehicle = new ParkedVehicle("1234567", "ford", "e", "red", 4, time);

            repo.CreateParking(vehicle);

            var vehicleInDB = context.ParkedVehicles.FirstOrDefault(x => x.LicensePlate == "1234567");

            Assert.AreEqual(vehicle.Make, vehicleInDB.Make);
        }

        [TestMethod()]
        public void GetAllActiveParkingsTest()
        {
            Assert.Fail();
        }
    }
}