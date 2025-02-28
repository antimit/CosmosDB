using TestApp2._0.DTOs.ProductDTOs;
using TestApp2._0.Models;

namespace TestApp2._0.Mapping;

public static class ProductMapping
{
    public static Product ToEntity(this ProductAddDTO dto)
    {
        return new Product
        {
            Name = dto.Name,
            SalesUnitPrice = dto.SalesUnitPrice,
            Description = dto.Description,
            Weight = dto.Weight,
            Volume = dto.Volume
        };
    }
    
    
    public static ProductResponseDTO ToDTO(this Product entity)
    {
        return new ProductResponseDTO
        {
            ProductId = entity.ProductId,
            Name = entity.Name,
            SalesUnitPrice = entity.SalesUnitPrice,
            Description = entity.Description,
            Weight = entity.Weight,
            Volume = entity.Volume
        };
    }
}