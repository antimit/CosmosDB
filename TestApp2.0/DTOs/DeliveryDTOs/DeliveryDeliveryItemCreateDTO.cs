using System.ComponentModel.DataAnnotations;
using TestApp2._0.Models;

namespace TestApp2._0.DTOs.DeliveryDTOs;

public class DeliveryDeliveryItemCreateDTO
{
    [Required(ErrorMessage = "Delivery ID is required.")]
    public string DeliveryItemId { get; set; }
}