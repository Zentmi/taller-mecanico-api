
using System.ComponentModel.DataAnnotations;

namespace TallerMecanico.DTOs.Ordenes;

public class CreateOrdenServicioRequest
{
    public int IdVehiculo { get; set; }

    [StringLength(500)]
    public string? ObservacionOrdenServicio { get; set; }
}