using System.ComponentModel.DataAnnotations;
using TestApp2._0.Models;

namespace TestApp2._0.DTOs.TransportationDTOs;

public class TransportationResponseDTO
{
    public string TransportationId { get; set; }

    public string DriverId { get; set; }


    public string TruckId { get; set; }
    public ICollection<TransportationStopResponseDTO> Stops { get; set; }
    public TransportationStatus Status { get; set; }
    
    public DateTime CreatedAt { get; set; } 

    public DateTime? UpdatedAt { get; set; }
    
    
    public string? LastStopId { get; set; }
    
    public int CompletedStopsCount { get; set; }

    public bool IsFinalized { get; set; }
}