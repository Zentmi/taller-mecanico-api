


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
            .HasForeignKey(o => o.UsuarioOrdenServicio)
            .OnDelete(DeleteBehavior.Restrict);

        builder
           .HasOne<Unidad>()
           .WithMany()
           .HasForeignKey(o => o.IdVehiculo)
           .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(o => o.IdOrdenServicio)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .HasIndex(o => o.IdOrdenServicio)
            .IsUnique();

        builder
            .Property(o => o.FechaOrdenSolicitud)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder
            .Property(o => o.StatusOrdenServicio)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(o => o.ObervacionOrdenServicio)
            .HasMaxLength(500)
            .IsRequired();


    }
}