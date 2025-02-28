using Microsoft.EntityFrameworkCore;
using TestApp2._0.Data;
using TestApp2._0.DTOs;
using TestApp2._0.DTOs.AddressDTOs;
using TestApp2._0.Mapping;
using TestApp2._0.Models;

namespace TestApp2._0.Services;

public class AddressService
{
    private readonly ApplicationDbContext _context;

    public AddressService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<AddressResponseDTO>> AddAddressAsync(AddressAddDTO addressDto)
    {
        try
        {
            var address = addressDto.ToEntity();

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            var addressResponse = address.ToDTO();

            return new ApiResponse<AddressResponseDTO>(200, addressResponse);
        }
        catch (Exception ex)
        {
            return new ApiResponse<AddressResponseDTO>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }

    public async Task<ApiResponse<AddressResponseDTO>> GetAddressByIdAsync(string id)
    {
        try
        {
            var address = await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(add => add.AddressId == id);
            if (address == null)
            {
                return new ApiResponse<AddressResponseDTO>(404, "Address not found.");
            }

            var addressResponse = address.ToDTO();
            return new ApiResponse<AddressResponseDTO>(200, addressResponse);
        }
        catch (Exception ex)
        {
            return new ApiResponse<AddressResponseDTO>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }

    public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteAddressAsync(string addressId)
    {
        try
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(add => add.AddressId == addressId);
            if (address == null)
            {
                return new ApiResponse<ConfirmationResponseDTO>(404, "Address not found.");
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = $"Address with Id {addressId} deleted successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ConfirmationResponseDTO>(500,
                "An unexpected error occurred while processing your request.");
        }
    }

    // Manual mapping from AddressAddDTO to Address entity
    private Address MapToEntity(AddressAddDTO dto)
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

    // Manual mapping from Address entity to AddressResponseDTO
    private AddressResponseDTO MapToDTO(Address entity)
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
