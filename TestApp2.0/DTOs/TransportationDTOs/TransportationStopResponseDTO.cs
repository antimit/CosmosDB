using TestApp2._0.Models;

namespace TestApp2._0.DTOs.TransportationDTOs;

public class TransportationStopResponseDTO
{

    public string StopOrder { get; set; }

    public double DistanceFromPreviousStop { get; set; }

    public string CustomerId { get; set; }

   

    public string AddressId { get; set; }
    
    public StopStatus Status { get; set; } 
    
}