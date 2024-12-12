using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingLib.Models;
using ParkingLib.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLib.Services.Tests
{
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class ParkingRepoTests
    {
        private DbContextOptions<ParkingContext> GetInMemoryOptions(string dataBaseName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ParkingContext>();
            optionsBuilder.UseInMemoryDatabase(dataBaseName);

            return optionsBuilder.Options;
        }

        [TestMethod()]
        public async Task ParkingRepoTest_GetAll_Valid()
        {
            var options = GetInMemoryOptions("TestDataBaseGetAll");

            using (var context = new ParkingContext(options))
            {
                TimeForParkedVehicle time = new TimeForParkedVehicle(DateTime.Now);

                context.ParkedVehicles.Add(
                 new ParkedVehicle { LicensePlate = "1234567", Make = "Ford", Model = "Fiesta", Color = "Grey", NumberOfWheels = 4, ActiveParked = time });
                context.ParkedVehicles.Add(
                 new ParkedVehicle { LicensePlate = "7654321", Make = "Toyota", Model = "Astra", Color = "Green", NumberOfWheels = 4, ActiveParked = time });

                context.SaveChanges();
                var repo = new ParkingRepo(context);

                var result = await repo.GetAllActiveParkings();

                Assert.AreEqual(2, result.Count);
            }
        }

        [TestMethod()]
        public void ParkingRepoTest_CreatingParking_Is_Valid()
        {
            var options = GetInMemoryOptions("TestCreatingParking");

            using (var context = new ParkingContext(options))
            {
                TimeForParkedVehicle time = new TimeForParkedVehicle(DateTime.Now);

                ParkedVehicle vehicle = new ParkedVehicle { LicensePlate = "DAT3B", Make = "Skoda", Model = "Octavia", Color = "Brown", NumberOfWheels = 4, ActiveParked = time };

                var repo = new ParkingRepo(context);

                var result = repo.CreateParking(vehicle);

                var vehicleToList = context.ParkedVehicles.ToList();

                Assert.AreEqual(1, vehicleToList.Count);
            }
        }

        [TestMethod()]
        public async Task ParkingRepoTest_DeletingParking_Is_Valid()
        {
            var options = GetInMemoryOptions("TestDeletingParking");

            using (var context = new ParkingContext(options))
            {
                var repo = new ParkingRepo(context);

                TimeForParkedVehicle time = new TimeForParkedVehicle(DateTime.Now);

                ParkedVehicle vehicle = new ParkedVehicle { LicensePlate = "DAT3B", Make = "Skoda", Model = "Octavia", Color = "Brown", NumberOfWheels = 4, ActiveParked = time };
                ParkedVehicle vehicle1 = new ParkedVehicle { LicensePlate = "DAT2B", Make = "Skoda", Model = "Octavia", Color = "Brown", NumberOfWheels = 4, ActiveParked = time };


                var test = repo.CreateParking(vehicle);
                var test1 = repo.CreateParking(vehicle1);


                var result = await repo.DeleteParking(vehicle.LicensePlate);

                var vehicleToList = context.ParkedVehicles.ToList();

                Assert.AreEqual(1, vehicleToList.Count);

            }

        }

        [TestMethod()]
        [DataRow("DAT3B")]
        public async Task ParkingRepoTest_GetParkingById_Is_Valid(string licensePlate)
        {
            var options = GetInMemoryOptions("TestGetParkingById");

            using (var context = new ParkingContext(options))
            {
                var repo = new ParkingRepo(context);

                TimeForParkedVehicle time = new TimeForParkedVehicle(DateTime.Now);

                ParkedVehicle vehicle = new ParkedVehicle
                {
                    LicensePlate = licensePlate,
                    Make = "Toyota",
                    Model = "Corolla",
                    Color = "Blue",
                    NumberOfWheels = 4,
                    ActiveParked = time
                };

                var test = repo.CreateParking(vehicle);

                var result = await repo.GetParkingById(licensePlate);

                Assert.AreEqual(licensePlate, result.LicensePlate);

            }
        }

        [TestMethod()]
        [DataRow("3BDAT")]
        public async Task ParkingRepoTest_GetById_Is_NOT_valid(string licensePlate)
        {
            var options = GetInMemoryOptions("TestGetParkingById_NotValid");

            using (var context = new ParkingContext(options))
            {
                var repo = new ParkingRepo(context);

                _ = Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () =>
                {
                    await repo.GetParkingById(licensePlate);
                });
            }
        }

    }
}