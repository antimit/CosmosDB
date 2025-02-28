using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TestApp2._0.Models;



public class Customer
{
    [JsonPropertyName("id")] // This makes CustomerId the Cosmos 'id' field.
    public string  CustomerId { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters.")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "PhoneNumber is required.")]
    public string PhoneNumber { get; set; }
    
    public Stop? Stop { get; set; }
    
}