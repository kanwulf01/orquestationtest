using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configuration
{
    public class PermisoConfiguration: IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            builder.ToTable("Permiso");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.NombreEmpleado)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.ApellidoEmpleado)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.TipoPermisoId)
                .HasColumnName("TipoPermiso")
                .IsRequired();

            builder.HasOne(p => p.TipoPermiso)
                .WithMany(p => p.Permisos)
                .HasForeignKey(p => p.TipoPermisoId)
                .IsRequired();

     

            builder.Property(p => p.FechaPermiso)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


        }
    }
}
