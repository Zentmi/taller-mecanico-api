


using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.DTOs.Common;
using TallerMecanico.DTOs.Ordenes;
using TallerMecanico.Models;
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
            if (error == "La unidad no existe.")
            {
                return NotFound(new
                {
                    message = error
                });
            }

            return Conflict(new
            {
                message = error
            });
        }
        return Ok(MapearRespuesta(orden));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPatch("{idOrden}/cancelar")]
    public async Task<IActionResult> Cancelar(int idOrden)
    {
        var (orden, error) = await _ordenServicioService.CancelarAsync(idOrden);

        if (orden is null)
        {
            if (error == "La orden de servicio no existe.")
            {
                return NotFound(new
                {
                    message = error
                });
            }

            return Conflict(new
            {
                message = error
            });
        }


        return Ok(MapearRespuesta(orden));
    }
    [Authorize]
    [HttpGet]
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos(
    int page = 1,
    int pageSize = 10)
    {
        if (page < 1)
        {
            return BadRequest(new
            {
                message = "El número de página debe ser mayor o igual a 1."
            });
        }

        if (pageSize < 1 || pageSize > 100)
        {
            return BadRequest(new
            {
                message = "El tamaño de página debe estar entre 1 y 100."
            });
        }

        var resultado = await _ordenServicioService.ObtenerTodosAsync(
            page,
            pageSize);

        var response = new PagedResponse<OrdenServicioResponse>
        {
            Items = resultado.Items
                .Select(MapearRespuesta)
                .ToList(),
            Page = resultado.Page,
            PageSize = resultado.PageSize,
            TotalItems = resultado.TotalItems,
            TotalPages = resultado.TotalPages
        };

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

        return Ok(MapearRespuesta(orden));
    }

    private static OrdenServicioResponse MapearRespuesta(OrdenServicio orden)
    {
        return new OrdenServicioResponse
        {
            PkOrden = orden.PkOrden,
            IdOrdenServicio = orden.IdOrdenServicio,
            FechaOrdenSolicitud = orden.FechaOrdenSolicitud,
            StatusOrden = orden.StatusOrden.ToString(),
            ObservacionOrdenServicio = orden.ObservacionOrdenServicio,
            IdVehiculo = orden.IdVehiculo
        };
    }
}