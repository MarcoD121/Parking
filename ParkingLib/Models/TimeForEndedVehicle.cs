﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ParkingLib.Models;

public partial class TimeForEndedVehicle
{
    public int EndedParkedId { get; set; }

    public DateTime TimeStarted { get; set; }

    public DateTime TimeEnded { get; set; }

    public DateTime TotalTime { get; set; }

    public virtual ICollection<EndedParkedVehicle> EndedParkedVehicles { get; set; } = new List<EndedParkedVehicle>();
    public TimeForEndedVehicle(DateTime timeStarted, DateTime timeEnded, DateTime totalTime)
    {
        TimeStarted = timeStarted;
        TimeEnded = timeEnded;
        TotalTime = totalTime;
    }
}