


using Microsoft.EntityFrameworkCore;
using TallerMecanico.Data;
using TallerMecanico.DTOs.Ordenes;
using TallerMecanico.Models;
using TallerMecanico.Models.Enums;

namespace TallerMecanico.Services;

public class OrdenServicioService
{
    private readonly TallerMecanicoContext _context;

    public OrdenServicioService(TallerMecanicoContext context)
    {
        _context = context;
    }

    public async Task<(OrdenServicio? Orden, string? Error)> CrearAsync(
        CreateOrdenServicioRequest request,
        int usuarioId)
    {
        var unidad = await _context.Unidades
        .FirstOrDefaultAsync(u => u.IdVehiculo == request.IdVehiculo && u.Activo);

        if (unidad is null)
            return (null, "La unidad no existe o está inactiva.");

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.IdUsuario == usuarioId);

        if (usuario is null)
            return (null, "El usuario no existe.");

        var orden = new OrdenServicio
        {
            IdOrdenServicio = string.Empty,
            StatusOrden = StatusOrden.Pendiente,
            UsuarioIdOrdenServicio = usuarioId,
            ObservacionOrdenServicio = request.ObservacionOrdenServicio,
            IdVehiculo = request.IdVehiculo
        };

        _context.OrdenesServicio.Add(orden);

        await _context.SaveChangesAsync();

        orden.IdOrdenServicio = $"Ord-Serv-{orden.PkOrden:D3}";

        await _context.SaveChangesAsync();

        return (orden, null);
    }

    public async Task<OrdenServicio?> ObtenerPorIdAsync(int idOrden)
    {
        return await _context.OrdenesServicio
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.PkOrden == idOrden);
    }

    public async Task<(OrdenServicio? Orden, string? Error)> CancelarAsync(
    int idOrden)
    {
        var orden = await _context.OrdenesServicio
            .FirstOrDefaultAsync(o => o.PkOrden == idOrden);

        if (orden is null)
            return (null, "La orden de servicio no existe.");

        if (orden.StatusOrden == StatusOrden.Completada)
            return (null, "La orden de servicio ya está completada y no puede cancelarse.");

        if (orden.StatusOrden == StatusOrden.Cancelada)
            return (null, "La orden de servicio ya está cancelada.");

        orden.StatusOrden = StatusOrden.Cancelada;

        await _context.SaveChangesAsync();

        return (orden, null);
    }
}