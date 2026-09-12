


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TallerMecanico.Models;

namespace TallerMecanico.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder
            .HasKey(u => u.IdUsuario);


        builder
            .Property(u => u.PrimerNombreUsuario)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(u => u.PrimerApellidoUsuario)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(u => u.RolUsuario)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(U => U.PasswordHash)
            .IsRequired();

        builder
            .Property(u => u.Activo)
            .HasDefaultValue(true)
            .IsRequired();

        builder
            .HasQueryFilter(u => u.Activo);
    }

}


