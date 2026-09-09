


namespace TallerMecanico.Models;

public class Usuario
{
    public int IdUsuario { get; set; }

    public string PrimerNombreUsuario { get; set; } = string.Empty;

    public string PrimerApellidoUsuario { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string RolUsuario { get; set; } = string.Empty;

}