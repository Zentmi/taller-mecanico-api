


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
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TallerMecanicoContext).Assembly);
    }


}

