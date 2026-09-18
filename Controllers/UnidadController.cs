

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.DTOs.Unidades;
using TallerMecanico.Models;
using TallerMecanico.Services;

namespace TallerMecanico.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UnidadController : ControllerBase
{
    private readonly UnidadService _unidadService;

    public UnidadController(UnidadService unidadService)
    {
        _unidadService = unidadService;
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Crear(CreateUnidadRequest request)
    {
        var (unidad, error) = await _unidadService.CrearAsync(request);

        if (unidad is null)
        {
            return Conflict(new
            {
                message = error
            });
        }

        return Ok(MapearRespuesta(unidad));
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        var unidades = await _unidadService.ObtenerTodasAsync();

        var response = unidades.Select(MapearRespuesta);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var unidad = await _unidadService.ObtenerPorIdAsync(id);

        if (unidad is null)
            return NotFound(new
            {
                message = "La unidad no existe."
            });

        return Ok(MapearRespuesta(unidad));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, UpdateUnidadRequest request)
    {
        var (unidad, error) = await _unidadService.ActualizarAsync(id, request);

        if (unidad is null)
        {
            if (error == "La unidad no existe.")
                return NotFound(new
                {
                    message = error
                });

            return Conflict(new
            {
                message = error
            });
        }

        return Ok(MapearRespuesta(unidad));
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        var desactivada = await _unidadService.DesactivarAsync(id);

        if (!desactivada)
            return NotFound(new
            {
                message = "La unidad no existe."
            });

        return NoContent();
    }

    private static UnidadResponse MapearRespuesta(Unidad unidad)
    {
        return new UnidadResponse
        {
            IdVehiculo = unidad.IdVehiculo,
            MarcaVehiculo = unidad.MarcaVehiculo,
            ModeloVehiculo = unidad.ModeloVehiculo,
            ColorVehiculo = unidad.ColorVehiculo,
            CombustibleVehiculo = unidad.CombustibleVehiculo.ToString(),
            AnioVehiculo = unidad.AnioVehiculo,
            PlacasVehiculo = unidad.PlacasVehiculo,
            FechaRegistroVehiculo = unidad.FechaRegistroVehiculo,
            Activo = unidad.Activo
        };
    }
}