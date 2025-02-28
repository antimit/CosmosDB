using TestApp2._0.DTOs.DeliveryItemsDTOs;
using TestApp2._0.Models;

namespace TestApp2._0.Mapping;

public static class DeliveryItemMapping
{
    
    

    public static DeliveryItem ToEntity(this DeliveryItemCreateDTO dto)
    {
        return new DeliveryItem
        {
            ProductId = dto.productId,
            Name = dto.Name,
            OrderedCount = dto.OrderedCount,
            TotalCost = 0,
            ItemWeight = 0,
            ItemVolume = 0
        };
    }
    
    
    public static DeliveryItemResponseDTO ToDTO(this DeliveryItem entity)
    {
        return new DeliveryItemResponseDTO
        {
            DeliveryItemId = entity.id,
            productId = entity.ProductId,
            Name = entity.Name,
            SalesUnitPrice = entity.SalesUnitPrice,
            OrderedCount = entity.OrderedCount,
            DeliveredCount = entity.DeliveredCount,
            CurrentDeliveryId = entity.id,
            TotalCost = entity.TotalCost,
            ItemWeight = entity.ItemWeight,
            ItemVolume = entity.ItemVolume
        };
    }
}