


using System.ComponentModel.DataAnnotations;

namespace TallerMecanico.DTOs.Auth;

public class RegisterRequest
{
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string PrimerNombreUsuario { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string PrimerApellidoUsuario { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
}