


using System.ComponentModel.DataAnnotations;
using TallerMecanico.Models.Enums;

namespace TallerMecanico.DTOs.Unidades;

public class UpdateUnidadRequest
{
    [Required]
    [StringLength(15)]
    public string MarcaVehiculo { get; set; } = string.Empty;

    [Required]
    [StringLength(15)]
    public string ModeloVehiculo { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string ColorVehiculo { get; set; } = string.Empty;

    [Required]
    public TipoCombustible CombustibleVehiculo { get; set; }

    [Range(1900, 2100)]
    public int AnioVehiculo { get; set; }

    [Required]
    [StringLength(10)]
    public string PlacasVehiculo { get; set; } = string.Empty;
}