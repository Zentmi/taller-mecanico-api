



using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Data;
using TallerMecanico.DTOs.Auth;
using TallerMecanico.Models;
using TallerMecanico.Models.Enums;
using TallerMecanico.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TallerMecanico.Services;

public class AuthService
{
    private readonly TallerMecanicoContext _context;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    private readonly JwtOptions _jwtOptions;

    public AuthService(
        TallerMecanicoContext context,
        IPasswordHasher<Usuario> passwordHasher,
        IOptions<JwtOptions> jwtOptions)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<Usuario?> RegisterAsync(RegisterRequest request)
    {
        var usuarioExiste = await _context.Usuarios
            .AnyAsync(u => u.NombreUsuario == request.NombreUsuario);

        if (usuarioExiste)
        {
            return null;
        }

        var usuario = new Usuario
        {
            NombreUsuario = request.NombreUsuario,
            PrimerNombreUsuario = request.PrimerNombreUsuario,
            PrimerApellidoUsuario = request.PrimerApellidoUsuario,
            RolUsuario = RolUsuario.Comun,
            Activo = true
        };

        usuario.PasswordHash = _passwordHasher.HashPassword(
            usuario,
            request.Password
        );

        _context.Usuarios.Add(usuario);

        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<string?> LoginAsync(LoginRequest request)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NombreUsuario == request.NombreUsuario);

        if (usuario is null)
        {
            return null;
        }

        if (!usuario.Activo)
        {
            return null;
        }

        var resultado = _passwordHasher.VerifyHashedPassword(
            usuario,
            usuario.PasswordHash,
            request.Password
        );

        if (resultado == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return GenerateToken(usuario);
    }

    private string GenerateToken(Usuario usuario)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
        new Claim(ClaimTypes.Name, usuario.NombreUsuario),
        new Claim(ClaimTypes.Role, usuario.RolUsuario.ToString())
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtOptions.Key)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}