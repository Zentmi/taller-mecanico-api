


using Microsoft.EntityFrameworkCore;
using TallerMecanico.Data;
using TallerMecanico.DTOs.Unidades;
using TallerMecanico.Models;

namespace TallerMecanico.Services;

public class UnidadService
{
    private readonly TallerMecanicoContext _context;

    public UnidadService(TallerMecanicoContext context)
    {
        _context = context;
    }

    public async Task<Unidad> CrearAsync(CreateUnidadRequest request)
    {
        var unidad = new Unidad
        {
            MarcaVehiculo = request.MarcaVehiculo,
            ModeloVehiculo = request.ModeloVehiculo,
            ColorVehiculo = request.ColorVehiculo,
            CombustibleVehiculo = request.CombustibleVehiculo,
            AnioVehiculo = request.AnioVehiculo,
            PlacasVehiculo = request.PlacasVehiculo
        };

        _context.Unidades.Add(unidad);

        await _context.SaveChangesAsync();

        return unidad;
    }

    public async Task<List<Unidad>> ObtenerTodasAsync()
    {
        return await _context.Unidades
            .AsNoTracking()
            .Where(u => u.Activo)
            .ToListAsync();
    }

    public async Task<Unidad?> ObtenerPorIdAsync(int id)
    {
        return await _context.Unidades
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.IdVehiculo == id && u.Activo);
    }

    public async Task<Unidad?> ActualizarAsync(
    int id,
    UpdateUnidadRequest request)
    {
        var unidad = await _context.Unidades
            .FirstOrDefaultAsync(u => u.IdVehiculo == id && u.Activo);

        if (unidad is null)
            return null;

        unidad.MarcaVehiculo = request.MarcaVehiculo;
        unidad.ModeloVehiculo = request.ModeloVehiculo;
        unidad.ColorVehiculo = request.ColorVehiculo;
        unidad.CombustibleVehiculo = request.CombustibleVehiculo;
        unidad.AnioVehiculo = request.AnioVehiculo;
        unidad.PlacasVehiculo = request.PlacasVehiculo;

        await _context.SaveChangesAsync();

        return unidad;
    }

    public async Task<bool> DesactivarAsync(int id)
    {
        var unidad = await _context.Unidades
            .FirstOrDefaultAsync(u => u.IdVehiculo == id && u.Activo);

        if (unidad is null)
            return false;

        unidad.Activo = false;

        await _context.SaveChangesAsync();

        return true;
    }
}