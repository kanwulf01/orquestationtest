using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{

    public class TipoPermisoConfiguration : IEntityTypeConfiguration<TipoPermiso>
    {
        public void Configure(EntityTypeBuilder<TipoPermiso> builder)
        {
            builder.ToTable("TipoPermiso");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Descripcion)
                .HasMaxLength(500)
                .IsRequired();



        }
    }
}
