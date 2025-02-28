using Microsoft.AspNetCore.Mvc;
using TestApp2._0.DTOs;
using TestApp2._0.DTOs.AddressDTOs;
using TestApp2._0.Services;

namespace TestApp2._0.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController(AddressService addressService) : ControllerBase
{
    [HttpPost("RegisterAddress")]
    public async Task<ActionResult<ApiResponse<AddressResponseDTO>>> RegisterAddress(
        [FromBody] AddressAddDTO addressDto)
    {
        var response = await addressService.AddAddressAsync(addressDto);
        if (response.StatusCode != 200)
        {
            return StatusCode(response.StatusCode, response);
        }

        return Ok(response);
    }


    [HttpGet("GetAddressById/{id}")]
    public async Task<ActionResult<ApiResponse<AddressResponseDTO>>> GetAddressById(string id)
    {
        var response = await addressService.GetAddressByIdAsync(id);
        if (response.StatusCode != 200)
        {
            return StatusCode(response.StatusCode, response);
        }

        return Ok(response);
    }

    [HttpDelete("DeleteAddress/{id}")]
    public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteCustomer(string id)
    {
        var response = await addressService.DeleteAddressAsync(id);
        if (response.StatusCode != 200)
        {
            return StatusCode(response.StatusCode, response);
        }

        return Ok(response);
    }
}