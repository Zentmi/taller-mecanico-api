


using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

namespace TallerMecanico.Data;

public class TallerMecanicoContext : DbContext
{
    public TallerMecanicoContext(DbContextOptions<TallerMecanicoContext> options)
        : base(options)
    {
    }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Unidad> Unidades { get; set; }

    public DbSet<OrdenServicio> OrdenesServicio { get; set; }

    public DbSet<Servicio> Servicios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.IdUsuario);

        modelBuilder.Entity<Unidad>()
            .HasKey(u => u.IdVehiculo);

        modelBuilder.Entity<OrdenServicio>()
            .HasKey(o => o.PkOrden);

        modelBuilder.Entity<Servicio>()
            .HasKey(s => s.PkServicio);

        modelBuilder.Entity<OrdenServicio>()
        .HasOne<Usuario>()
        .WithMany()
        .HasForeignKey(o => o.UsuarioOrdenServicio);
    }
}

