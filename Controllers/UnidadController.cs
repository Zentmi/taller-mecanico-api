

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerMecanico.DTOs.Unidades;
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
        var unidad = await _unidadService.CrearAsync(request);

        return Ok(unidad);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        var unidades = await _unidadService.ObtenerTodasAsync();

        return Ok(unidades);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var unidad = await _unidadService.ObtenerPorIdAsync(id);

        if (unidad is null)
            return NotFound();

        return Ok(unidad);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, UpdateUnidadRequest request)
    {
        var unidad = await _unidadService.ActualizarAsync(id, request);

        if (unidad is null)
            return NotFound();

        return Ok(unidad);
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        var desactivada = await _unidadService.DesactivarAsync(id);

        if (!desactivada)
            return NotFound();

        return NoContent();
    }
}