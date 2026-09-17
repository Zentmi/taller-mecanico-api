


using TallerMecanico.Models.Enums;

namespace TallerMecanico.Models;

public class Servicio
{
    public int PkServicio { get; set; }

    public string IdServicio { get; set; } = string.Empty;

    public int PkOrden { get; set; }

    public string TipoServicio { get; set; } = string.Empty;

    public string DetalleServicio { get; set; } = string.Empty;

    public decimal CostoServicio { get; set; }

    public DateTime? FechaInicioServicio { get; set; }

    public DateTime? FechaFinServicio { get; set; }

    public DateTime? FechaCancelacionServicio { get; set; }

    public StatusServicio StatusServicio { get; set; }

    public OrdenServicio OrdenServicio { get; set; } = null!;
}