


namespace TallerMecanico.DTOs.Servicios;

public class ServicioResponse
{
    public int PkServicio { get; set; }

    public string IdServicio { get; set; } = string.Empty;

    public int FkOrden { get; set; }

    public string TipoServicio { get; set; } = string.Empty;

    public string DetalleServicio { get; set; } = string.Empty;

    public decimal CostoServicio { get; set; }

    public DateTime? FechaInicioServicio { get; set; }

    public DateTime? FechaFinServicio { get; set; }

    public DateTime? FechaCancelacionServicio { get; set; }

    public string StatusServicio { get; set; } = string.Empty;
}