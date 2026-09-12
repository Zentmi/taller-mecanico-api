


using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerMecanico.Models;

namespace TallerMecanico.Data.Configurations;

public class UnidadConfiguration : IEntityTypeConfiguration<Unidad>
{
    public void Configure(EntityTypeBuilder<Unidad> builder)
    {
        builder
            .HasKey(u => u.IdVehiculo);

        builder
            .Property(u => u.MarcaVehiculo)
            .HasMaxLength(15)
            .IsRequired();

        builder
            .Property(u => u.ModeloVehiculo)
            .HasMaxLength(15)
            .IsRequired();

        builder
            .Property(u => u.ColorVehiculo)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .ToTable("Unidades", table => { table.HasCheckConstraint("CK_Unidad_Anio", "[AnioVehiculo] >= 1900 AND [AnioVehiculo] <= 2100"); });

        builder
            .Property(u => u.PlacasVehiculo)
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(u => u.FechaRegistroVehiculo)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder
            .Property(u => u.CombustibleVehiculo)
            .HasConversion<string>()
            .IsRequired();

        builder
            .Property(u => u.Activo)
            .HasDefaultValue(true)
            .IsRequired();

        builder
            .HasQueryFilter(u => u.Activo);
    }
}