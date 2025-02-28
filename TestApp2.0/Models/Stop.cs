using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp2._0.Models;

public class Stop
{
    public string StopId { get; set; } = Guid.NewGuid().ToString();


    public string StopOrder { get; set; }

    public double DistanceFromPreviousStop { get; set; }

    [Required] public string CustomerId { get; set; }

    [ForeignKey("CustomerId")] public Customer Customer { get; set; }

    [Required] public string AddressId { get; set; }
    [ForeignKey("AddressId")] public Address Address { get; set; }

    public ICollection<Delivery>? Deliveries { get; set; } = new List<Delivery>();

    [Required] public string? TransportationId { get; set; }
    [ForeignKey("TransportationId")] public Transportation? CurrentTransportation { get; set; }

    [Required] public StopStatus Status { get; set; } = StopStatus.Pending;
}