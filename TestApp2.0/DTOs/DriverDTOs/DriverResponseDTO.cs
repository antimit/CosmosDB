﻿using TestApp2._0.Models;

namespace TestApp2._0.DTOs.DriverDTOs;

public class DriverResponseDTO
{
    public string DriverId { get; set; }


    public string FirstName { get; set; }


    public string LastName { get; set; }

    public string Email { get; set; }


    public string PhoneNumber { get; set; }

    public DriverStatus Status { get; set; }
    public int? TransportationId { get; set; }
}