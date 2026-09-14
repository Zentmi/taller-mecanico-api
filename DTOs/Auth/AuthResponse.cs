

namespace TallerMecanico.DTOs.Auth;

public class AuthResponse
{
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string PrimerNombreUsuario { get; set; } = string.Empty;
    public string PrimerApellidoUsuario { get; set; } = string.Empty;
    public string RolUsuario { get; set; } = string.Empty;
}