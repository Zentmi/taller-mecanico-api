


using Microsoft.EntityFrameworkCore;
using TallerMecanico.Data;
using TallerMecanico.DTOs.Servicios;
using TallerMecanico.Models;
using TallerMecanico.Models.Enums;

namespace TallerMecanico.Services;

public class ServicioService
{
    private readonly TallerMecanicoContext _context;

    public ServicioService(TallerMecanicoContext context)
    {
        _context = context;
    }

    public async Task<(Servicio? Servicio, string? Error)> CrearAsync(
        CreateServicioRequest request)
    {
        var orden = await _context.OrdenesServicio
            .FirstOrDefaultAsync(o => o.PkOrden == request.PkOrden);

        if (orden is null)
            return (null, "La orden de servicio no existe.");

        if (orden.StatusOrden != StatusOrden.Pendiente)
            return (null, "La orden de servicio ya no está pendiente y no puede recibir nuevos servicios.");

        var servicio = new Servicio
        {
            PkOrden = request.PkOrden,
            TipoServicio = request.TipoServicio,
            DetalleServicio = request.DetalleServicio,
            CostoServicio = request.CostoServicio,
            StatusServicio = StatusServicio.Pendiente
        };

        _context.Servicios.Add(servicio);

        await _context.SaveChangesAsync();

        servicio.IdServicio = $"Serv-{servicio.PkServicio:D3}";

        await _context.SaveChangesAsync();

        return (servicio, null);
    }

    public async Task<(Servicio? Servicio, string? Error)> CambiarEstadoAsync(
        int idServicio,
        UpdateServicioStatusRequest request)
    {
        var servicio = await _context.Servicios
            .FirstOrDefaultAsync(s => s.PkServicio == idServicio);

        if (servicio is null)
            return (null, "El servicio no existe.");

        var orden = await _context.OrdenesServicio
            .FirstOrDefaultAsync(o => o.PkOrden == servicio.PkOrden);

        if (orden is null)
            return (null, "La orden de servicio no existe.");

        if (orden.StatusOrden == StatusOrden.Cancelada)
            return (null,
                "La orden de servicio está cancelada y sus servicios no pueden modificarse.");

        var estadoActual = servicio.StatusServicio;
        var nuevoEstado = request.StatusServicio;

        if (estadoActual == nuevoEstado)
            return (null, "El servicio ya tiene ese estado.");

        var transicionValida = estadoActual switch
        {
            StatusServicio.Pendiente =>
                nuevoEstado == StatusServicio.EnProceso ||
                nuevoEstado == StatusServicio.Cancelado,

            StatusServicio.EnProceso =>
                nuevoEstado == StatusServicio.Completado ||
                nuevoEstado == StatusServicio.Cancelado,

            StatusServicio.Completado => false,

            StatusServicio.Cancelado => false,

            _ => false
        };

        if (!transicionValida)
            return (null,
                $"No se permite cambiar el servicio de {estadoActual} a {nuevoEstado}.");

        servicio.StatusServicio = nuevoEstado;

        if (nuevoEstado == StatusServicio.EnProceso)
        {
            servicio.FechaInicioServicio = DateTime.UtcNow;
        }
        else if (nuevoEstado == StatusServicio.Completado)
        {
            servicio.FechaFinServicio = DateTime.UtcNow;
        }
        else if (nuevoEstado == StatusServicio.Cancelado)
        {
            servicio.FechaCancelacionServicio = DateTime.UtcNow;
        }

        await ActualizarEstadoOrdenAsync(servicio.PkOrden);

        await _context.SaveChangesAsync();

        return (servicio, null);
    }

    private async Task ActualizarEstadoOrdenAsync(int pkOrden)
    {
        var orden = await _context.OrdenesServicio
            .FirstOrDefaultAsync(o => o.PkOrden == pkOrden);

        if (orden is null)
            return;

        if (orden.StatusOrden == StatusOrden.Cancelada)
            return;

        var servicios = await _context.Servicios
            .Where(s => s.PkOrden == pkOrden)
            .ToListAsync();

        if (servicios.Count == 0)
        {
            orden.StatusOrden = StatusOrden.Pendiente;
            return;
        }

        var serviciosActivos = servicios
            .Where(s => s.StatusServicio != StatusServicio.Cancelado)
            .ToList();

        if (serviciosActivos.Count == 0)
        {
            orden.StatusOrden = StatusOrden.Pendiente;
            return;
        }

        if (serviciosActivos.All(s =>
            s.StatusServicio == StatusServicio.Completado))
        {
            orden.StatusOrden = StatusOrden.Completada;
            return;
        }

        if (serviciosActivos.Any(s =>
            s.StatusServicio == StatusServicio.Completado))
        {
            orden.StatusOrden = StatusOrden.ParcialmenteCompletada;
            return;
        }

        if (serviciosActivos.Any(s =>
            s.StatusServicio == StatusServicio.EnProceso))
        {
            orden.StatusOrden = StatusOrden.EnProceso;
            return;
        }

        orden.StatusOrden = StatusOrden.Pendiente;
    }
}