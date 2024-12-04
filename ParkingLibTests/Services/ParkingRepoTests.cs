using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingLib.Models;
using ParkingLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLib.Services.Tests
{
    [TestClass()]
    public class ParkingRepoTests
    {
        [TestMethod()]
        public void ParkingRepoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllActiveParkingsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateParkingTest()
        {
            TimeForParkedVehicle time = new TimeForParkedVehicle(DateTime.Now);
            ParkedVehicle vehicle = new ParkedVehicle("1234567", "ford", "e", "red", 4, time);

            Assert.Fail(); 
        }

        [TestMethod()]
        public void GetParkingByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteParkingTest()
        {
            Assert.Fail();
        }
    }
}