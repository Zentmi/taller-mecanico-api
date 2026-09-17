namespace TallerMecanico.DTOs.Ordenes;

public class OrdenServicioResponse
{
    public int PkOrden { get; set; }

    public string IdOrdenServicio { get; set; } = string.Empty;

    public DateTime FechaOrdenSolicitud { get; set; }

    public string StatusOrden { get; set; } = string.Empty;

    public string? ObservacionOrdenServicio { get; set; }

    public int IdVehiculo { get; set; }
}