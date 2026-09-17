

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.DTOs.Servicios;
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

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Crear(CreateServicioRequest request)
    {
        var (servicio, error) = await _servicioService.CrearAsync(request);

        if (servicio is null)
        {
            return BadRequest(new
            {
                message = error
            });
        }

        return Ok(new ServicioResponse
        {
            PkServicio = servicio.PkServicio,
            IdServicio = servicio.IdServicio,
            FkOrden = servicio.FkOrden,
            TipoServicio = servicio.TipoServicio,
            DetalleServicio = servicio.DetalleServicio,
            CostoServicio = servicio.CostoServicio,
            FechaInicioServicio = servicio.FechaInicioServicio,
            FechaFinServicio = servicio.FechaFinServicio,
            FechaCancelacionServicio = servicio.FechaCancelacionServicio,
            StatusServicio = servicio.StatusServicio.ToString()
        });
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
            return BadRequest(new
            {
                message = error
            });
        }

        return Ok(new ServicioResponse
        {
            PkServicio = servicio.PkServicio,
            IdServicio = servicio.IdServicio,
            FkOrden = servicio.FkOrden,
            TipoServicio = servicio.TipoServicio,
            DetalleServicio = servicio.DetalleServicio,
            CostoServicio = servicio.CostoServicio,
            FechaInicioServicio = servicio.FechaInicioServicio,
            FechaFinServicio = servicio.FechaFinServicio,
            FechaCancelacionServicio = servicio.FechaCancelacionServicio,
            StatusServicio = servicio.StatusServicio.ToString()
        });
    }
}