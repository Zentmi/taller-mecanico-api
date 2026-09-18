
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.DTOs.Auth;
using TallerMecanico.Services;


namespace TallerMecanico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistroRequest request)
    {
        var usuario = await _authService.RegisterAsync(request);

        if (usuario is null)
        {
            return Conflict(new
            {
                message = "El nombre de usuario ya está registrado."
            });
        }

        return Ok(new AuthResponse
        {
            IdUsuario = usuario.IdUsuario,
            NombreUsuario = usuario.NombreUsuario,
            PrimerNombreUsuario = usuario.PrimerNombreUsuario,
            PrimerApellidoUsuario = usuario.PrimerApellidoUsuario,
            RolUsuario = usuario.RolUsuario.ToString()
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authService.LoginAsync(request);

        if (token is null)
        {
            return Unauthorized(new
            {
                message = "Usuario o contraseña incorrectos."
            });
        }

        return Ok(new
        {
            token
        });
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("admin")]
    public IActionResult Admin()
    {
        return Ok(new
        {
            mensaje = "Tienes permisos de administrador."
        });
    }
}