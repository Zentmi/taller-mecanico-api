


namespace TallerMecanico.Models;

public class Unidad
{

    public int IdVehiculo { get; set; }

    public string MarcaVehiculo { get; set; } = string.Empty;

    public string ModeloVehiculo { get; set; } = string.Empty;

    public string ColorVehiculo { get; set; } = string.Empty;

    public string CombustibleVehiculo { get; set; } = string.Empty;
    public int AniooVehiculo { get; set; }

    public string PlacasVehiculo { get; set; } = string.Empty;

    public DateTime FechaRegistroVehiculo { get; set; }

}