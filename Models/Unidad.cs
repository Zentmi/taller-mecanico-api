


using TallerMecanico.Models.Enums;

namespace TallerMecanico.Models;

public class Unidad
{

    public int IdVehiculo { get; set; }

    public string MarcaVehiculo { get; set; } = string.Empty;

    public string ModeloVehiculo { get; set; } = string.Empty;

    public string ColorVehiculo { get; set; } = string.Empty;

    public TipoCombustible CombustibleVehiculo { get; set; }
    public int AnioVehiculo { get; set; }

    public string PlacasVehiculo { get; set; } = string.Empty;

    public DateTime FechaRegistroVehiculo { get; set; }

    public bool Activo { get; set; } = true;

}