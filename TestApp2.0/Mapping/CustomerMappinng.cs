using TestApp2._0.DTOs.CustomerDTOs;
using TestApp2._0.Models;

namespace TestApp2._0.Mapping;

public static class CustomerMappinng
{
    public static Customer ToEntity(this CustomerAddDTO dto)
    {
        return new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
    }
    
    
    public static CustomerResponseDTO ToDTO(this Customer entity)
    {
        return new CustomerResponseDTO
        {
            CustomerId = entity.CustomerId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.LastName,
            PhoneNumber = entity.PhoneNumber
        };
    }
}