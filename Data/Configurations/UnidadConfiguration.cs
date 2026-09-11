


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
    }
}