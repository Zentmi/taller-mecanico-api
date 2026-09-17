

namespace TallerMecanico.DTOs.Ordenes;

public class CreateOrdenServicioRequest
{
    public int IdVehiculo { get; set; }

    public string? ObservacionOrdenServicio { get; set; }
}