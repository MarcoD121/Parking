﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ParkingLib.Models;

/// <summary>
/// Class connected to db table when a car is no longer parked
/// </summary>
public partial class EndedParkedVehicle
{

    /// <summary>
    /// Unique Id
    /// </summary>
    public int VehicleId { get; set; }

    /// <summary>
    /// Unique Id
    /// </summary>
    public int EndedParkedId { get; set; }


    /// <summary>
    /// License plate of the car
    /// </summary>
    public string LicensePlate { get; set; }

    /// <summary>
    /// The name of the car manufacture
    /// </summary>
    public string Make { get; set; }

    /// <summary>
    /// The name of the car model
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Color of the car
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// Number of wheels on the car
    /// </summary>
    public int NumberOfWheels { get; set; }

    /// <summary>
    /// What type is the car etc. passenger car
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// A class containing time for the parked car
    /// </summary>
    public virtual TimeForEndedVehicle EndedParked { get; set; }

    /// <summary>
    /// Creates an instance of the class with null values
    /// </summary>
    public EndedParkedVehicle()
    {
        
    }

    /// <summary>
    /// Creates an instance of car that is no longer parked object
    /// </summary>
    /// <param name="licensePlate">License plate of the car</param>
    /// <param name="make">The name of the car manufacture</param>
    /// <param name="model">The name of the car model</param>
    /// <param name="color">Color of the car</param>
    /// <param name="numberOfWheels">Number of wheels on the car</param>
    /// <param name="type">What type is the car etc. passenger car</param>
    /// <param name="endedParked">A class containing time for the parked car</param>
    public EndedParkedVehicle(string licensePlate, string make, string model, string color, int numberOfWheels, string type, TimeForEndedVehicle endedParked)
    {
        LicensePlate = licensePlate;
        Make = make;
        Model = model;
        Color = color;
        NumberOfWheels = numberOfWheels;
        Type = type;
        EndedParked = endedParked;
    }

}