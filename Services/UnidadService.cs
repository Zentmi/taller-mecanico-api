

using TallerMecanico.DTOs.Common;
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

    public async Task<(Unidad? Unidad, string? Error)> CrearAsync(
        CreateUnidadRequest request)
    {
        var placasExistentes = await _context.Unidades
            .AnyAsync(u => u.PlacasVehiculo == request.PlacasVehiculo);

        if (placasExistentes)
            return (null, "Ya existe una unidad con esas placas.");

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

        return (unidad, null);
    }

    public async Task<PagedResponse<Unidad>> ObtenerTodasAsync(
    int page,
    int pageSize)
    {
        var query = _context.Unidades
            .AsNoTracking()
            .Where(u => u.Activo)
            .OrderBy(u => u.IdVehiculo);

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(
            totalItems / (double)pageSize);

        return new PagedResponse<Unidad>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
    }

    public async Task<Unidad?> ObtenerPorIdAsync(int id)
    {
        return await _context.Unidades
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.IdVehiculo == id && u.Activo);
    }

    public async Task<(Unidad? Unidad, string? Error)> ActualizarAsync(
        int id,
        UpdateUnidadRequest request)
    {
        var unidad = await _context.Unidades
            .FirstOrDefaultAsync(u => u.IdVehiculo == id && u.Activo);

        if (unidad is null)
            return (null, "La unidad no existe.");

        var placasExistentes = await _context.Unidades
            .AnyAsync(u =>
                u.PlacasVehiculo == request.PlacasVehiculo &&
                u.IdVehiculo != id);

        if (placasExistentes)
            return (null, "Ya existe otra unidad con esas placas.");

        unidad.MarcaVehiculo = request.MarcaVehiculo;
        unidad.ModeloVehiculo = request.ModeloVehiculo;
        unidad.ColorVehiculo = request.ColorVehiculo;
        unidad.CombustibleVehiculo = request.CombustibleVehiculo;
        unidad.AnioVehiculo = request.AnioVehiculo;
        unidad.PlacasVehiculo = request.PlacasVehiculo;

        await _context.SaveChangesAsync();

        return (unidad, null);
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