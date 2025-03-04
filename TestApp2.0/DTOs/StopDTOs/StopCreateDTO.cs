﻿using System.ComponentModel.DataAnnotations;
using TestApp2._0.Models;

namespace TestApp2._0.DTOs.StopDTOs;

public class StopCreateDTO
{
    [Required(ErrorMessage = "StopOrder is Required")]
    public string StopOrder { get; set; } = Guid.NewGuid().ToString();

    public double DistanceFromPreviousStop { get; set; }


    [Required] public string CustomerId { get; set; }


    [Required] public string AddressId { get; set; }

    public List<StopDeliveryCreate>? Deliveries { get; set; }

    

    
}