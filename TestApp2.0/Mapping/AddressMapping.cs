using TestApp2._0.DTOs.AddressDTOs;
using TestApp2._0.Models;

namespace TestApp2._0.Mapping;

public static class AddressMapping
{
    public static Address ToEntity(this AddressAddDTO dto)
    {
        return new Address
        {
            Street = dto.Street,
            City = dto.City,
            PostalCode = dto.PostalCode,
            Country = dto.Country,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude
        };
    }
    
    
    public static AddressResponseDTO ToDTO(this Address entity)
    {
        return new AddressResponseDTO
        {
            AddressId = entity.AddressId,
            Street = entity.Street,
            City = entity.City,
            PostalCode = entity.PostalCode,
            Country = entity.Country,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude
        };
    }
}