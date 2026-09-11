


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerMecanico.Models;

namespace TallerMecanico.Data.Configurations;


public class OrdenServicioConfiguration : IEntityTypeConfiguration<OrdenServicio>
{
    public void Configure(EntityTypeBuilder<OrdenServicio> builder)
    {
        builder
            .HasKey(o => o.PkOrden);

        builder
            .HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(o => o.UsuarioOrdenServicio);

        builder
           .HasOne<Unidad>()
           .WithMany()
           .HasForeignKey(o => o.IdVehiculo);
    }
}