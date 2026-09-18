

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.DTOs.Servicios;
using TallerMecanico.Models;
using TallerMecanico.Services;

namespace TallerMecanico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicioController : ControllerBase
{
    private readonly ServicioService _servicioService;

    public ServicioController(ServicioService servicioService)
    {
        _servicioService = servicioService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var servicios = await _servicioService.ObtenerTodosAsync();

        var response = servicios.Select(MapearRespuesta);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("{idServicio}")]
    public async Task<IActionResult> ObtenerPorId(int idServicio)
    {
        var servicio = await _servicioService.ObtenerPorIdAsync(idServicio);

        if (servicio is null)
        {
            return NotFound(new
            {
                message = "El servicio no existe."
            });
        }

        return Ok(MapearRespuesta(servicio));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Crear(CreateServicioRequest request)
    {
        var (servicio, error) = await _servicioService.CrearAsync(request);

        if (servicio is null)
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

        return Ok(MapearRespuesta(servicio));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPatch("{idServicio}/estado")]
    public async Task<IActionResult> CambiarEstado(
        int idServicio,
        UpdateServicioStatusRequest request)
    {
        var (servicio, error) = await _servicioService.CambiarEstadoAsync(
            idServicio,
            request);

        if (servicio is null)
        {
            if (error == "El servicio no existe." ||
                error == "La orden de servicio no existe.")
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

        return Ok(MapearRespuesta(servicio));
    }
    private static ServicioResponse MapearRespuesta(Servicio servicio)
    {
        return new ServicioResponse
        {
            PkServicio = servicio.PkServicio,
            IdServicio = servicio.IdServicio,
            PkOrden = servicio.PkOrden,
            TipoServicio = servicio.TipoServicio,
            DetalleServicio = servicio.DetalleServicio,
            CostoServicio = servicio.CostoServicio,
            FechaInicioServicio = servicio.FechaInicioServicio,
            FechaFinServicio = servicio.FechaFinServicio,
            FechaCancelacionServicio = servicio.FechaCancelacionServicio,
            StatusServicio = servicio.StatusServicio.ToString()
        };
    }
}