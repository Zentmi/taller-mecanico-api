


using TallerMecanico.Models.Enums;

namespace TallerMecanico.Models;

public class OrdenServicio
{
    public int PkOrden { get; set; }

    public string IdOrdenServicio { get; set; } = string.Empty;

    public DateTime FechaOrdenSolicitud { get; set; }

    public StatusOrden StatusOrden { get; set; }

    public int UsuarioIdOrdenServicio { get; set; }

    public string? ObservacionOrdenServicio { get; set; }

    public int IdVehiculo { get; set; }

    public Unidad Unidad { get; set; } = null!;

    public Usuario Usuario { get; set; } = null!;
}