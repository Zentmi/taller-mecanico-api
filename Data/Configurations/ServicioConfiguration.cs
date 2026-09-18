


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerMecanico.Models;

namespace TallerMecanico.Data.Configurations;

public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
{
    public void Configure(EntityTypeBuilder<Servicio> builder)
    {
        builder
            .HasKey(s => s.PkServicio);

        builder
            .HasOne(s => s.OrdenServicio)
            .WithMany()
            .HasForeignKey(s => s.PkOrden)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(s => s.IdServicio)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .HasIndex(s => s.IdServicio)
            .IsUnique();

        builder
            .Property(s => s.TipoServicio)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(s => s.DetalleServicio)
            .HasMaxLength(1000)
            .IsRequired();

        builder
            .Property(s => s.CostoServicio)
            .HasPrecision(18, 2)
            .IsRequired();

        builder
            .Property(s => s.StatusServicio)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder
            .ToTable("Servicios", table =>
            table.HasCheckConstraint(
            "CK_Servicio_Costo",
            "[CostoServicio] >= 0"));
    }
}