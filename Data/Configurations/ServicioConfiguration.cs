


using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models;

namespace TallerMecanico.Data.Configurations;

public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Servicio> builder)
    {
        builder
            .HasKey(s => s.PkServicio);

        builder
            .HasOne<OrdenServicio>()
            .WithMany()
            .HasForeignKey(s => s.FkOrden);
    }
}