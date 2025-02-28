using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApp2._0.Models;

namespace TestApp2._0.DTOs.TransportationDTOs;

public class TransportationCreateDTO
{
    public string DriverId { get; set; }

    public string TruckId { get; set; }


    public List<TransportationStopCreateDTO> Stops { get; set; }
    
}