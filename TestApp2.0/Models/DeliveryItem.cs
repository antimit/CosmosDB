using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestApp2._0.Models;

public class DeliveryItem
{
    [Required] 
    public string ProductId { get; set; } 
    
    public Product Product { get; set; }

    [JsonPropertyName("id")]  // Map DeliveryItemId to `id` in CosmosDB

    public string id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }

    public decimal SalesUnitPrice { get; set; }

    public int OrderedCount { get; set; }
    public int? DeliveredCount { get; set; }

    public string? CurrentDeliveryId { get; set; }
    public Delivery? CurrentDelivery { get; set; }

    public decimal TotalCost { get; set; } 
    public int ItemWeight { get; set; }
    public int ItemVolume { get; set; }
}