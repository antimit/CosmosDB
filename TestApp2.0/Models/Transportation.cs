﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp2._0.Models;

public class Transportation
{
    public string TransportationId { get; set; }  = Guid.NewGuid().ToString();

    public string DriverId { get; set; }
    [ForeignKey("DriverId")] 
    public Driver Driver { get; set; }


    public string TruckId { get; set; }

    [ForeignKey("TruckId")] public Vehicle Truck { get; set; }


    public ICollection<Stop>? Stops { get; set; }
    public TransportationStatus TransportationStatus { get; set; }


    [Required(ErrorMessage = "Departure Time is required")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    [Required] public DateTime? DepartureTime { get; set; }
    


    public string? LastStopId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Completed stops cannot be negative")]
    public int CompletedStopsCount { get; set; }
}