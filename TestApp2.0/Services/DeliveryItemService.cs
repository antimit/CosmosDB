using AutoMapper;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using TestApp2._0.Data;
using TestApp2._0.DTOs;
using TestApp2._0.DTOs.DeliveryItemsDTOs;
using TestApp2._0.Mapping;
using TestApp2._0.Models;

namespace TestApp2._0.Services;

public class DeliveryItemService
{
    private readonly ApplicationDbContext _context;
    private readonly CosmosClient _cosmosClient;

   

    public DeliveryItemService(ApplicationDbContext context, CosmosClient client)
    {
        _context = context;
        _cosmosClient = client;

    }

    // public async Task<ApiResponse<DeliveryItemResponseDTO>> CreateDeliveryItemAsync(
    //     DeliveryItemCreateDTO deliveryItemDto)
    // {
    //     try
    //     {
    //         var product = await _context.Products
    //             .AsNoTracking()
    //             .FirstOrDefaultAsync(p => p.ProductId == deliveryItemDto.productId);
    //
    //         if (product == null)
    //         {
    //             return new ApiResponse<DeliveryItemResponseDTO>(400, "Invalid Product ID.");
    //         }
    //
    //         decimal totalCost = deliveryItemDto.OrderedCount * product.SalesUnitPrice;
    //
    //
    //         var deliveryItem = deliveryItemDto.ToEntity();
    //         // deliveryItem.Product = product;
    //         // deliveryItem.TotalCost = totalCost;
    //
    //         _context.DeliveryItems.Add(deliveryItem);
    //         await _context.SaveChangesAsync();
    //
    //         var response = deliveryItem.ToDTO();
    //
    //         return new ApiResponse<DeliveryItemResponseDTO>(200, response);
    //     }
    //     catch (Exception ex)
    //     {
    //         return new ApiResponse<DeliveryItemResponseDTO>(500, $"Error: {ex.Message}");
    //     }
    // }
    public async Task<ApiResponse<DeliveryItemResponseDTO>> CreateDeliveryItemAsync(DeliveryItemCreateDTO dto)
    {
        try
        {
            var container = _cosmosClient.GetContainer("TestApp", "DeliveryItems");

            // Fetch Product (this assumes Products are stored separately — you can skip this if you embed it yourself)
            var productContainer = _cosmosClient.GetContainer("TestApp", "Products");
            var productResponse = await productContainer.ReadItemAsync<Product>(
                dto.productId, new PartitionKey(dto.productId)
            );
            var product = productResponse.Resource;

            var deliveryItem = new DeliveryItem
            {
                id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                OrderedCount = dto.OrderedCount,
                Product = product, // Embed product directly
                ProductId = product.ProductId, // Still store ProductId if you want easy querying
                TotalCost = dto.OrderedCount * product.SalesUnitPrice,
                ItemVolume = 0,
                ItemWeight = 0
            };

            // Use Upsert (insert or update if it already exists)
            await container.CreateItemAsync(deliveryItem, new PartitionKey(deliveryItem.id));

            var responseDto = deliveryItem.ToDTO(); // You can map it to DTO if needed
            return new ApiResponse<DeliveryItemResponseDTO>(200, responseDto);
        }
        catch (CosmosException cosmosEx)
        {
            return new ApiResponse<DeliveryItemResponseDTO>(cosmosEx.StatusCode == System.Net.HttpStatusCode.Conflict ? 409 : 500,
                $"Cosmos error: {cosmosEx.Message}");
        }
        catch (Exception ex)
        {
            return new ApiResponse<DeliveryItemResponseDTO>(500, $"Unexpected error: {ex.Message}");
        }
    }


    public async Task<ApiResponse<List<DeliveryItemResponseDTO>>> GetAllDeliveryItemsAsync()
    {
        try
        {
            var container = _cosmosClient.GetContainer("TestApp", "DeliveryItems");

            var query = new QueryDefinition("SELECT * FROM c");  // Simple query to get all items
            var iterator = container.GetItemQueryIterator<DeliveryItem>(query);

            var deliveryItems = new List<DeliveryItem>();

            while (iterator.HasMoreResults)
            {
                var resultSet = await iterator.ReadNextAsync();
                deliveryItems.AddRange(resultSet);
            }

            var responseList = deliveryItems
                .Select(d => d.ToDTO())
                .ToList();

            return new ApiResponse<List<DeliveryItemResponseDTO>>(200, responseList);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<DeliveryItemResponseDTO>>(500, $"Error: {ex.Message}");
        }
    }


    public async Task<ApiResponse<List<DeliveryItemResponseDTO>>> GetFilteredDeliveryItemsAsync(
        decimal? maxsalesUnitPrice, int? minItemWeight)
    {
        try
        {
            var query = _context.DeliveryItems.AsQueryable();

            if (maxsalesUnitPrice.HasValue)
            {
                query = query.Where(di => di.SalesUnitPrice <= maxsalesUnitPrice.Value);
            }

            if (minItemWeight.HasValue)
            {
                query = query.Where(di => di.ItemWeight > minItemWeight.Value);
            }

            var deliveryItems = await query
                .AsNoTracking()
                .ToListAsync();

            var responseList = deliveryItems.Select(c =>c.ToDTO()).ToList();

            return new ApiResponse<List<DeliveryItemResponseDTO>>(200, responseList);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<DeliveryItemResponseDTO>>(500, $"Error: {ex.Message}");
        }
    }


    public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteDeliveryItemAsync(string deliveryItemId)
    {
        try
        {
            
            var deliveryItem = await _context.DeliveryItems.FirstOrDefaultAsync(c => c.id ==deliveryItemId);
            if (deliveryItem == null)
            {
                return new ApiResponse<ConfirmationResponseDTO>(404, "DeliveryItem not found.");
            }

            _context.DeliveryItems.Remove(deliveryItem);
            await _context.SaveChangesAsync();
            var confirmation = new ConfirmationResponseDTO
            {
                Message = $"DeliveryItem with Id {deliveryItem.id} deleted successfully."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmation);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ConfirmationResponseDTO>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }
}