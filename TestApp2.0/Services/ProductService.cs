using AutoMapper;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using TestApp2._0.Data;
using TestApp2._0.DTOs;
using TestApp2._0.DTOs.ProductDTOs;
using TestApp2._0.Mapping;
using TestApp2._0.Models;

namespace TestApp2._0.Services;

public class ProductService
{
    private readonly ApplicationDbContext _context;
    private readonly CosmosClient _cosmosClient;

    public ProductService(ApplicationDbContext context, CosmosClient cosmosClient)
    {
        _context = context;
        _cosmosClient = cosmosClient;
    }


    public async Task<ApiResponse<ProductResponseDTO>> CreateProductAsync(ProductAddDTO productDto)
    {
        try
        {
            if (await NameExistsAsync(productDto.Name))
            {
                return new ApiResponse<ProductResponseDTO>(400, "Product name already in use");
            }

            var product = productDto.ToEntity();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var productResponse = product.ToDTO();
            return new ApiResponse<ProductResponseDTO>(200, productResponse);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ProductResponseDTO>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }

    public async Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsAsync()
    {
        try
        {
            var products = await _context.Products
                .AsNoTracking()
                .ToListAsync();
            var productList = products.Select(c => c.ToDTO()).ToList();
            return new ApiResponse<List<ProductResponseDTO>>(200, productList);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProductResponseDTO>>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }
    
    
    
    public async Task<ApiResponse<ProductResponseDTO>> UpdateProductAsync(ProductUpdateDTO productDto)
    {
        try
        {
          
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productDto.ProductId);
        
            if (existingProduct == null)
            {
                return new ApiResponse<ProductResponseDTO>(404, "Product not found.");
            }

           
            existingProduct.SalesUnitPrice = productDto.SalesUnitPrice;

           
            await _context.SaveChangesAsync();

            var updatedProductResponse = existingProduct.ToDTO();

            return new ApiResponse<ProductResponseDTO>(200, updatedProductResponse);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ProductResponseDTO>(500,
                $"An unexpected error occurred while processing your request, Error: {ex.Message}");
        }
    }
    
    
    private async Task<bool> NameExistsAsync(string name)
    {
        var container = _cosmosClient.GetContainer("TestApp", "Products");
        var query = new QueryDefinition("SELECT VALUE COUNT(1) FROM c WHERE c.Name = @name")
            .WithParameter("@name", name);

        var iterator = container.GetItemQueryIterator<int>(query);
        var result = await iterator.ReadNextAsync();
        var count = result.FirstOrDefault();

        return count > 0;
    }

}