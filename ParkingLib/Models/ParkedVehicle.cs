﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ParkingLib.Models;

public partial class ParkedVehicle
{
    public int VehicleId { get; set; }

    public int ActiveParkedId { get; set; }

    public string LicensePlate { get; set; }

    public string Make { get; set; }

    public string Model { get; set; }

    public string Color { get; set; }

    public int NumberOfWheels { get; set; }

    public virtual TimeForParkedVehicle ActiveParked { get; set; }

    public ParkedVehicle(string licensePlate, string make, string model, string color, int numberOfWheels, TimeForParkedVehicle activeParked)
    {
        LicensePlate = licensePlate;
        Make = make;
        Model = model;
        Color = color;
        NumberOfWheels = numberOfWheels;
        ActiveParked = activeParked;
    }
}