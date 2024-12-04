// See https://aka.ms/new-console-template for more information
using Parking.Models;
using Parking.Services;

Console.WriteLine("Hello, World!");

ParkingRepo repo = new ParkingRepo();

TimeForParkedVehicle timeForParkedVehicle = new TimeForParkedVehicle();
timeForParkedVehicle.TimeStarted = DateTime.Now;

ParkedVehicle vehicle = new ParkedVehicle();
vehicle.LicensePlate = "1234567";
vehicle.Make = "dsalfk";
vehicle.Model = "skdjf";
vehicle.Color = "red";
vehicle.NumberOfWheels = 4;
vehicle.ActiveParked = timeForParkedVehicle;

repo.CreateParking(vehicle);



