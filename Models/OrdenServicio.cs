


namespace TallerMecanico.Models;

public class OrdenServicio
{
    public int PkOrden { get; set; }

    public string IdOrdenServicio { get; set; } = string.Empty;

    public DateTime FechaOrdenSolicitud { get; set; }

    public string StatusOrdenServicio { get; set; } = string.Empty;

    public int UsuarioOrdenServicio { get; set; }

    public string ObervacionOrdenServicio { get; set; } = string.Empty;

    public int IdVehiculo { get; set; }


}