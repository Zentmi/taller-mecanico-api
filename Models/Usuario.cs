


using TallerMecanico.Models.Enums;

namespace TallerMecanico.Models;

public class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = string.Empty;

    public string PrimerNombreUsuario { get; set; } = string.Empty;

    public string PrimerApellidoUsuario { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public RolUsuario RolUsuario { get; set; }

    public bool Activo { get; set; } = true;

}