


using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.DTOs.Ordenes;
using TallerMecanico.Services;

namespace TallerMecanico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdenServicioController : ControllerBase
{
    private readonly OrdenServicioService _ordenServicioService;

    public OrdenServicioController(OrdenServicioService ordenServicioService)
    {
        _ordenServicioService = ordenServicioService;
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Crear(CreateOrdenServicioRequest request)
    {
        var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(idUsuarioClaim, out var usuarioId))
        {
            return Unauthorized(new
            {
                message = "No se pudo identificar al usuario autenticado."
            });
        }

        var (orden, error) = await _ordenServicioService.CrearAsync(
            request,
            usuarioId);

        if (orden is null)
        {
            return BadRequest(new
            {
                message = error
            });
        }

        return Ok(new OrdenServicioResponse
        {
            PkOrden = orden.PkOrden,
            IdOrdenServicio = orden.IdOrdenServicio,
            FechaOrdenSolicitud = orden.FechaOrdenSolicitud,
            StatusOrden = orden.StatusOrden.ToString(),
            ObservacionOrdenServicio = orden.ObservacionOrdenServicio,
            IdVehiculo = orden.IdVehiculo
        });
    }

    [Authorize(Roles = "Administrador")]
    [HttpPatch("{idOrden}/cancelar")]
    public async Task<IActionResult> Cancelar(int idOrden)
    {
        var (orden, error) = await _ordenServicioService.CancelarAsync(idOrden);

        if (orden is null)
        {
            return BadRequest(new
            {
                message = error
            });
        }

        return Ok(new OrdenServicioResponse
        {
            PkOrden = orden.PkOrden,
            IdOrdenServicio = orden.IdOrdenServicio,
            FechaOrdenSolicitud = orden.FechaOrdenSolicitud,
            StatusOrden = orden.StatusOrden.ToString(),
            ObservacionOrdenServicio = orden.ObservacionOrdenServicio,
            IdVehiculo = orden.IdVehiculo
        });
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var ordenes = await _ordenServicioService.ObtenerTodosAsync();

        var response = ordenes.Select(orden => new OrdenServicioResponse
        {
            PkOrden = orden.PkOrden,
            IdOrdenServicio = orden.IdOrdenServicio,
            FechaOrdenSolicitud = orden.FechaOrdenSolicitud,
            StatusOrden = orden.StatusOrden.ToString(),
            ObservacionOrdenServicio = orden.ObservacionOrdenServicio,
            IdVehiculo = orden.IdVehiculo
        });

        return Ok(response);
    }


    [Authorize]
    [HttpGet("{idOrden}")]
    public async Task<IActionResult> ObtenerPorId(int idOrden)
    {
        var orden = await _ordenServicioService.ObtenerPorIdAsync(idOrden);

        if (orden is null)
        {
            return NotFound(new
            {
                message = "La orden de servicio no existe."
            });
        }

        return Ok(new OrdenServicioResponse
        {
            PkOrden = orden.PkOrden,
            IdOrdenServicio = orden.IdOrdenServicio,
            FechaOrdenSolicitud = orden.FechaOrdenSolicitud,
            StatusOrden = orden.StatusOrden.ToString(),
            ObservacionOrdenServicio = orden.ObservacionOrdenServicio,
            IdVehiculo = orden.IdVehiculo
        });
    }
}