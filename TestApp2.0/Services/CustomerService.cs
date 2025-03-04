﻿using Microsoft.EntityFrameworkCore;
using TestApp2._0.Data;
using TestApp2._0.DTOs;
using TestApp2._0.DTOs.CustomerDTOs;
using TestApp2._0.Models;
using AutoMapper;
using Microsoft.Azure.Cosmos;
using TestApp2._0.Mapping;

namespace TestApp2._0.Services;

public class CustomerService
{
    private readonly ApplicationDbContext _context;

    private readonly CosmosClient _cosmosClient;
    
    
    public CustomerService(ApplicationDbContext context, CosmosClient cosmosClient)
    {
        _context = context;
        _cosmosClient = cosmosClient;
    }


    public async Task<ApiResponse<List<CustomerResponseDTO>>> GetAllCustomersAsync()
    {
        try
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .ToListAsync();

            var customerList = customers.Select(c => c.ToDTO()).ToList();

            return new ApiResponse<List<CustomerResponseDTO>>(200, customerList);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<CustomerResponseDTO>>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }

    public async Task<ApiResponse<CustomerResponseDTO>> AddCustomerAsync(CustomerAddDTO customerDto)
    {
        try
        {
            if (await EmailExistsAsync(customerDto.Email))
            {
                return new ApiResponse<CustomerResponseDTO>(400, "Email already in use");
            }
            

            var customer = customerDto.ToEntity();

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            var customerResponse = customer.ToDTO();

            return new ApiResponse<CustomerResponseDTO>(200, customerResponse);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CustomerResponseDTO>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }


    public async Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIdAsync(string Customerid)
    {
        try
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == Customerid);
            if (customer == null)
            {
                return new ApiResponse<CustomerResponseDTO>(404, "Customer not found");
            }

            var customerResponse = customer.ToDTO();

            return new ApiResponse<CustomerResponseDTO>(200, customerResponse);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CustomerResponseDTO>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }


    
    public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateCustomerAsync(CustomerUpdateDTO customerDto)
    {
        try
        {
            // Retrieve customer using EF Core, which honors the partition key and container settings.
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == customerDto.CustomerId);

            if (customer == null)
            {
                return new ApiResponse<ConfirmationResponseDTO>(404, "Customer not found.");
            }

            // Check if the new email is already used by another customer
            if (customer.Email != customerDto.Email && await EmailExistsAsync(customerDto.Email))
            {
                return new ApiResponse<ConfirmationResponseDTO>(400, "Email is already in use.");
            }

            // Update customer fields
            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.Email = customerDto.Email;
            customer.PhoneNumber = customerDto.PhoneNumber;

            // Save changes using EF Core
            _context.Customers.Update(customer);  // Not strictly necessary since tracking handles it
            await _context.SaveChangesAsync();

            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = $"Customer with Id {customerDto.CustomerId} updated successfully."
            };

            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ConfirmationResponseDTO>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }



    public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteCustomerAsync(string Customerid)
    {
        try
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == Customerid);
            if (customer == null)
            {
                return new ApiResponse<ConfirmationResponseDTO>(404, "Customer not found.");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            var confirmationMessage = new ConfirmationResponseDTO
            {
                Message = $"Customer with Id {Customerid} deleted successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ConfirmationResponseDTO>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }
    
    private async Task<bool> EmailExistsAsync(string email)
    {
        var container = _cosmosClient.GetContainer("TestApp", "Customers");
        var query = new QueryDefinition("SELECT VALUE COUNT(1) FROM c WHERE c.Email = @email")
            .WithParameter("@email", email);

        var iterator = container.GetItemQueryIterator<int>(query);
        var result = await iterator.ReadNextAsync();
        var count = result.FirstOrDefault();

        return count > 0;
    }
    
    
}