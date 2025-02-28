using TestApp2._0.Models;

namespace TestApp2._0.DTOs.StopDTOs;

public class StopResponseDTO
{
    public string StopId { get; set; }


    public string StopOrder { get; set; } = Guid.NewGuid().ToString();

    public double DistanceFromPreviousStop { get; set; }

    public string CustomerId { get; set; }


    public string AddressId { get; set; }

    public List<StopDeliveryResponse>? Deliveries { get; set; }

    public string? TransportationId { get; set; }

    public StopStatus Status { get; set; } = StopStatus.Pending;
}