


using System.ComponentModel.DataAnnotations;

namespace TallerMecanico.DTOs.Servicios;

public class CreateServicioRequest
{

    public int PkOrden { get; set; }

    [Required]
    [StringLength(50)]
    public string TipoServicio { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string DetalleServicio { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal CostoServicio { get; set; }
}